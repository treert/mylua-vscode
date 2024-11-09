using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Compiler;

/*
vscode multilineTokenSupport is false.
所以也不支持跨行的token。对于触发换行的 string 和 comment, 会拆分成多段。
*/

public enum Keyword
{
    AND = TokenType.NAME + 1,// 搞不懂了，一方面不支持enum隐式转换成int，一方面又允许这种语法
    BREAK,
    CONTINUE,// mylua 特殊支持
    DO,
    ELSE,
    ELSEIF,
    END,
    FALSE,
    FOR,
    FUNCTION,
    GOTO,
    IF,
    IN,
    LOCAL,
    NIL,
    NOT,
    OR,
    REPEAT,
    RETURN,
    TRUE,
    THEN,
    WHILE,
}

public enum TokenType
{
    // 空token，用于标记：行尾，文件尾，无效token
    None = 256,// 不设置成0了，源码里不能有0

    DIVIDE, // // 整除
    CONCAT,// .. string concat
    DOTS,// ...
    EQ,// ==
    GE,// >=
    LE,// <=
    NE,// ~=
    SHIFT_LEFT,// <<
    SHIFT_RIGHT,// >>

    QQUESTION,// ??

    DBCOLON, // ::

    NUMBER,// int64 or double
    STRING,// string 可以支持多行的

    Commnet,// 注释
    Illegal,// 各类非法输入。

    // Name，Must place at last
    NAME = 0xFFFF,
}

/*
补充 TokenType.Commnet 和 TokenType.String 信息
解析的状态
1. OneLine      普通单行
2. Started      多行的起点。
3. InMiddle     中间段 逻辑层面的
4. Ended        结束了

字符串类型
1. " or '
2. $ + (" or ')
3. [=*[ string ]=*]         需要额外记录 numof(=)
4. --[=*[ comment ]=*]      需要额外记录 numof(=)
5. 字符串里的转义字符

*/

// 补充 TokenType.Commnet 和 TokenType.String 信息
// 还需要额外字段记录 numof(=)，值是 numof(=)+2 or 0
public enum TokenStrFlag
{
    Normal             = 0,
    Started             = 1<<0,// 两大类情况：1. $string 开头的 $ 符号；2. 几种字符串的换行, 这个时候影响下一个 line 的解析
    Ended               = 1<<2,
    DoubleQuote         = 1<<4,
    SingleQuote         = 1<<5,
    Dollar              = 1<<6,// 纯标记
    RawString           = 1<<7,
    RawComment          = 1<<8,
    ZipMode             = 1<<9,// for \z, 同时也标记 sub_tok
    DollarDoubleQuote   = Dollar | DoubleQuote,
    DollarSingleQuote   = Dollar | SingleQuote,
    OnlyStrMask = SingleQuote | DoubleQuote | Dollar | RawString | RawComment | ZipMode,// 用于获取 str flag
}

public class Token
{
    // 结束标记。属于是无效 token
    public static readonly Token EndOfLine = new Token(TokenType.None);
    static Dictionary<string, Keyword> keywords = [];
    static Token()
    {
        var arr = Enum.GetValues(typeof(Keyword));
        for (int i = 0; i < arr.Length; i++)
        {
            var obj = arr.GetValue(i)!;
            Keyword key = (Keyword)obj;
            keywords.Add(key.ToString().ToLower(), key);
        }
    }

    public Token NextToken {
        get {
            if (src_line != null) {
                int idx = tok_idx + 1;
                if (src_line.Tokens.Count > idx){
                    return src_line.Tokens[idx];
                }
                else{
                    // 找一个有效的
                    MyLine line = src_line.NextLine;
                    while(line != null && line.Tokens.Count > 0) line = line.NextLine;
                    if (line is not null) return line.Tokens[0];
                }
            }
            return Token.EndOfLine;
        }
    }

    public Token PreToken{
        get{
            if (src_line != null) {
                int idx = tok_idx - 1;
                if (idx >= 0){
                    return src_line.Tokens[idx];
                }
                else{
                    // 找一个有效的
                    MyLine line = src_line.PreLine;
                    while(line != null && line.Tokens.Count > 0) line = line.PreLine;
                    if (line is not null) return line.LastToken;
                }
            }
            return Token.EndOfLine;
        }
    }

    // 获取行号。如果是EndOfLine，返回 -1
    public int RowIdx {
        get{
            if (src_line != null) return src_line.RowIdx;
            return -1;
        }
    }

    // 获取缩进数量。如果是 EndOfLine , return 0
    public int TabSize{
        get{
            if (src_line != null) return src_line.TabSize;
            return 0;
        }
    }

    public int type;
    public long num_int;
    public double num_double;
    public string str;

    public string ErrMsg => err_msg;

    // just for debug
    public override string ToString() {
        if (IsNone) return "<None>";
        // if (str != null) { return str;}
        return src_line.Content.Substring(start_idx, end_idx - start_idx);
    }

    public TokenStrFlag m_str_flag = TokenStrFlag.Normal;
    public void AddStrFlag(TokenStrFlag flag){
        m_str_flag |= flag;
    }
    public void RemoveStrFlag(TokenStrFlag flag){
        m_str_flag &= ~flag;
    }
    public void SetStrFlag(TokenStrFlag flag){
        m_str_flag = flag;
    }

    public bool TestStrFlag(TokenStrFlag flag){
        return (m_str_flag & flag) != 0;
    }
    public int m_equal_sep_num = 0;

    public bool IsStarted {
        get { return TestStrFlag(TokenStrFlag.Started);}
        set {
            if(value) AddStrFlag(TokenStrFlag.Started);
            else RemoveStrFlag(TokenStrFlag.Started);
        }
    }

    public bool IsEnded{
        get { return TestStrFlag(TokenStrFlag.Ended);}
        set {
            if(value) AddStrFlag(TokenStrFlag.Ended);
            else RemoveStrFlag(TokenStrFlag.Ended);
        }
    }

    public bool IsNone => Match(TokenType.None);// 其实可以直接用 == EndOfLine

    public bool IsComment => Match(TokenType.Commnet);

    public MyLine src_line = null;// EndOfLine 时，这个是null
    public int start_idx;
    public int end_idx;// end idx is exclusive
    public int tok_idx;// tokens 数组索引

    public string err_msg = string.Empty;// 如果不为空，说明有错误
    public List<Token> m_sub_toks = null;


    public Token(TokenType tokenType)
    {
        type = (int)tokenType;
    }

    public Token(char c)
    {
        type = (int)c;
    }

    public bool Match(char char_)
    {
        return type == (int)char_;
    }

    public bool Match(char ch1, char ch2)
    {
        return type == (int)ch1 || type == (int)ch2;
    }

    public bool Match(char ch1, char ch2, char ch3)
    {
        return type == (int)ch1 || type == (int)ch2 || type == (int)ch3;
    }

    public bool Match(TokenType type_)
    {
        return type == (int)type_;
    }

    public bool Match(Keyword key)
    {
        return type == (int)key;
    }

    public bool IsKeyword()
    {
        return type >= (int) Keyword.AND;
    }

    public void SetRange(int start_idx, int end_idx)
    {
        this.start_idx = start_idx;
        this.end_idx = end_idx;
    }

    public void MarkError(string msg)
    {
        err_msg = msg;
    }

    public void MarkToName(){
        type = (int)TokenType.NAME;
    }

    public void SetStr(string str)
    {
        this.str = str;
    }

    public void AddSubToken(Token tok){
        if (m_sub_toks == null) m_sub_toks = [];
        m_sub_toks.Add(tok);
    }

    public Token(string str_, bool force_name) {
        str = str_;
        if(force_name)
        {
            type = (int)TokenType.NAME;
        }
        else
        {
            if(keywords.TryGetValue(str_, out Keyword key))
            {
                type = (int)key;
            }
            else
            {
                type= (int)TokenType.NAME;
            }
        }
    }


}

public class LexError
{
    public Protocol.Position pos;
    public string msg;
}

/*
以单行为单位来解析。
状态描述：
1. 普通模式
2. " or ' 字符串中
3. $ + (" or ') 字符串中
    - 内嵌{ } 模式，不过不允许括号跨行。这个模式中允许 " or ' 字符串 raw string or commnet，由于不能跨行，须得一次性读完。
4. [=*[ string ]=*] 中，开始解析时
5. --[=*[ comment ]=*] 中，开始解析时
*/
public class LuaLex
{
    public bool IsInDollarMode => (m_cur_parse_flag&TokenStrFlag.Dollar) != 0;
    public bool IsInZipMode => (m_cur_parse_flag&TokenStrFlag.ZipMode) != 0;

    char DollarChar {
        get {
            if (m_cur_parse_flag == TokenStrFlag.DollarSingleQuote) return '\'';
            else if (m_cur_parse_flag == TokenStrFlag.DollarDoubleQuote) return '"';
            else return '\0';
        }
        set {
            if (value == '"') m_cur_parse_flag = TokenStrFlag.DollarDoubleQuote;
            else if (value == '\'') m_cur_parse_flag = TokenStrFlag.DollarSingleQuote;
            else m_cur_parse_flag = TokenStrFlag.Normal;
            Debug.Assert(m_dollar_open_cnt == 0);
        }
    }

    public bool IsAtEnd => _cur_char == '\n';

    TokenStrFlag m_cur_parse_flag = TokenStrFlag.Normal;
    int m_equal_sep_num = 0;
    int m_dollar_open_cnt;
    void ResetParseFlag(){
        m_equal_sep_num = 0;
        m_cur_parse_flag = TokenStrFlag.Normal;
        m_dollar_open_cnt = 0;
    }

    public bool ParseOneLine(MyLine line, MyLine pre_line = null, bool only_if_flag_changed = false){
        m_dollar_open_cnt = 0;
        m_equal_sep_num = 0;
        m_cur_parse_flag = TokenStrFlag.Normal;
        {
            if (pre_line?.Tokens.LastOrDefault() is Token tk){
                if (tk.IsStarted){
                    m_equal_sep_num = tk.m_equal_sep_num;
                    m_cur_parse_flag = tk.m_str_flag & TokenStrFlag.OnlyStrMask;
                    Debug.Assert(m_cur_parse_flag != TokenStrFlag.Normal);
                }
            }
        }

        if (only_if_flag_changed){
            if (m_equal_sep_num == line.m_equal_sep_num && m_cur_parse_flag == line.m_parse_flag){
                return false;// do nothing
            }
        }
        // 开始解析
        line.m_parse_flag = m_cur_parse_flag;
        line.m_equal_sep_num = m_equal_sep_num;
        line.ClearTokens();
        _line = line;
        _cur_idx = -1;
        _NextChar();// ready for read token

        Token pre_tok = Token.EndOfLine;
        Token last_tok = Token.EndOfLine;
        for(;;){
            var cur_tok = _ReadNextToken();
            if (cur_tok.IsNone) break;
            line.AddToken(cur_tok);
            last_tok = cur_tok;
            if (cur_tok.IsKeyword()){
                if (pre_tok.Match('.') || pre_tok.Match(TokenType.DBCOLON) || pre_tok.Match(Keyword.GOTO)){
                    cur_tok.MarkToName();
                }
            }
            else if(cur_tok.Match('=') && pre_tok.IsKeyword()){
                // 这儿讨巧了。有副作用，但是不会影响正常的流程。
                pre_tok.MarkToName();
            }
        }
        // 没读到任何token，需要特殊处理下多行字符串
        if (last_tok.IsNone){
            if (m_cur_parse_flag == TokenStrFlag.RawComment || m_cur_parse_flag == TokenStrFlag.RawString){
                // 增加中间段的空行
                var tk = _NewToken4String("", 0, 0);
                tk.IsStarted = true;
                tk.AddStrFlag(m_cur_parse_flag);
                tk.m_equal_sep_num = m_equal_sep_num;
                line.AddToken(tk);
            }
            else if (m_cur_parse_flag != TokenStrFlag.Normal){
                if (IsInZipMode){
                    // zip 模式的中间行
                    var tk = _NewToken4String("", 0, 0);// 其实区域无所谓
                    tk.IsStarted = true;
                    tk.AddStrFlag(m_cur_parse_flag);
                    line.AddToken(tk);
                }
                else{
                    // 强制结束掉
                    var tk = _NewToken4String("", 0, 0);
                    tk.IsEnded = true;
                    tk.MarkError("unexpect end with empty line");
                    line.AddToken(tk);
                }
            }
        }
        else if (IsInDollarMode){
            // $string 未结束
            if (!last_tok.IsStarted){
                // 未正常结束
                last_tok.IsEnded = true;// 强制结束
                last_tok.MarkError(m_dollar_open_cnt == 0 ? "$string need End or NewLine" : "$string {} mode must match in one line");
            }
            else{
                if (last_tok.Match('$')){
                    // 一种极限情况 $"\n
                    last_tok.IsEnded = true;// 强制结束自己
                    last_tok.MarkError("unfinished empty $string");
                }
            }
        }
        else if(m_cur_parse_flag != TokenStrFlag.Normal){
            if (!last_tok.IsStarted){
                // 没有正常结束呢
                last_tok.IsEnded = true;// 强制借宿
                if (last_tok.ErrMsg != ""){
                    last_tok.MarkError("string need End or NewLine");
                }
            }
        }
        return true;
    }

    static bool IsWordLetter(char c)
    {
        return c == '_' || char.IsAsciiLetter(c);
    }

    static bool IsWordLetterOrNumber(char c)
    {
        return c == '_' || char.IsAsciiLetterOrDigit(c);
    }

    StringBuilder _buf = new StringBuilder();


    MyLine _line;

    char _cur_char;
    int _cur_idx;

    char _NextChar()
    {
        _cur_idx++;
        if (_cur_idx < _line.Length)
        {
            _cur_char = _line[_cur_idx];
            return _cur_char;
        }
        else
        {
            _cur_idx = _line.Length;
            _cur_char = '\n';
            return '\n';
        }
    }

    bool _CheckThenSkip(char ch)
    {
        bool ok = _cur_char == ch;
        if (ok) { _NextChar(); }
        return ok;
    }

    Token _ReadNameOrKeyword(bool force_name = false)
    {
        Debug.Assert(IsWordLetter(_cur_char));
        int start_idx = _cur_idx;
        _buf.Clear();
        do
        {
            _buf.Append(_cur_char);
            _NextChar();
        } while (IsWordLetterOrNumber(_cur_char));
        var token = new Token(_buf.ToString(), force_name);
        token.SetRange(start_idx, _cur_idx);
        return token;
    }

    
    // \ddd
    Token _ReadDigitalEsc(){
        int i = 0;
        int r = 0;
        for (i = 0; i < 3 && char.IsAsciiDigit(_cur_char); i++) {
            r = r*10 + _cur_char - '0';
            _NextChar();
        }
        if (i == 0){
            return _NewIllegalToken(_cur_idx - 1, 1, "expect digital after '\\' in format '\\ddd'");
        }
        else if (r <= 0xff){
            char c = (char)r;
            return _NewTokenInStr(c.ToString(), _cur_idx - 1 - i, 1+i);
        }
        else{
            return _NewIllegalToken(_cur_idx - 1-i, 1+i, "too large in format '\\ddd'");
        }
    }

    // \xdd
    Token _ReadHexEsc()
    {
        _NextChar();// skip x
        int x = 0;
        int valid_num = 0;
        if(char.IsAsciiHexDigit(_cur_char)){
            x = LexUtil.HexToInt(_cur_char);
            valid_num ++;
            _NextChar();
            if(char.IsAsciiHexDigit(_cur_char)){
                x = (x<<4) + LexUtil.HexToInt(_cur_char);
                valid_num ++;
                _NextChar();
            }
        }
        if(valid_num == 2){
            char cc = (char)x;
            return _NewTokenInStr(cc.ToString(), _cur_idx-4, 4);
        }
        return _NewIllegalToken(_cur_idx - 2 - valid_num, 2 + valid_num, "invalid \\xdd");
    }

    // \u{aabbccdd}
    Token _ReadUnicodeEsc()
    {
        _NextChar();// skip u
        if (_CheckThenSkip('{')){
            int valid_num = 0;
            int x = 0;
            bool utf8_ok = true;
            while(char.IsAsciiHexDigit(_cur_char)){
                utf8_ok &= (x <= (0x7FFFFFFFu >> 4));
                x = (x<<4) + LexUtil.HexToInt(_cur_char);
                valid_num ++;
                _NextChar();
            }
            if(_CheckThenSkip('}')){
                if(utf8_ok && valid_num > 0){
                    try{
                        string cc = char.ConvertFromUtf32(x);
                        return _NewTokenInStr(cc, _cur_idx - 4 - valid_num, 4+valid_num);
                    }
                    catch{
                        return _NewIllegalToken( _cur_idx - 4 - valid_num, 4+valid_num, "invalid utf8 code");
                    }
                }
                else{
                    return _NewIllegalToken( _cur_idx - 4 - valid_num, 4+valid_num, "invalid utf string");
                }
            }
            else{
                return _NewIllegalToken(_cur_idx-3-valid_num, 3+valid_num, "expect valid utf str '\\u{XXXX}'");
            }
        }
        else{
            return _NewIllegalToken(_cur_idx-2, 2, "expect '{' after '\\u'");
        }
    }

    Token _ReadMiddleToken(){
        if (m_cur_parse_flag == TokenStrFlag.SingleQuote || m_cur_parse_flag == TokenStrFlag.DoubleQuote){
            var tok = _ReadString(m_cur_parse_flag == TokenStrFlag.SingleQuote ? '\'' : '"');
            return tok;
        }
        if (m_cur_parse_flag == TokenStrFlag.RawString){
            var tok = _ReadRawString(true);
            return tok;
        }
        if (m_cur_parse_flag == TokenStrFlag.RawComment){
            var tok = _ReadLongComment(true);
            return tok;
        }
        Debug.Assert(false, "should not happend");
        return null;
    }

    Token _ReadNextToken(){
        var tk = __ReadNextToken();
        tk.src_line = _line;
        return tk;
    }

    Token __ReadNextToken()
    {
        if (IsInZipMode){
            // 忽略掉开头的空白
            Debug.Assert(_cur_idx == 0);
            while(!IsAtEnd && char.IsWhiteSpace(_cur_char)){
                _NextChar();
            }
        }
        if (IsAtEnd) {
            var tok = Token.EndOfLine;
            return tok;
        }
        // 可以退出 Zip模式了
        m_cur_parse_flag &= ~TokenStrFlag.ZipMode;

        if (m_dollar_open_cnt == 0 && m_cur_parse_flag != TokenStrFlag.Normal){
            if (IsInDollarMode) {
                var tok = _ReadInDollarString();
                if (tok.IsEnded){
                    ResetParseFlag();// 正常结束
                }
                else if (IsAtEnd && !tok.IsStarted){
                    // 意外结束了。词法分析后续会处理这个情况。flag 残留给后续使用。
                }
                return tok;
            }
            else {
                // 一些字符串换行模式。
                var tok = _ReadMiddleToken();
                if (!tok.IsStarted){
                    // 正常结束
                    tok.IsEnded = true;
                    ResetParseFlag();
                }
                return tok;
            }
        }

        {
            var tok = _ReadNormalToken();
            return tok;
        }

    }

    Token _ReadNormalToken()
    {
        while (_cur_char != '\n')
        {
            switch(_cur_char)
            {
                case '\r':
                    Debug.Fail("should not happend!");
                    _NextChar();
                    break;
                case ' ': case '\f': case '\t': case '\v':
                    _NextChar();// ignore space
                    break;
                case '$':{
                    var tok = _NewTokenForCurChar();
                    _NextChar();
                    if (_cur_char == '"' || _cur_char == '\'')
                    {
                        if (IsInDollarMode)
                        {
                            tok.MarkError("do not support inner <$string>");
                            // 就不吃掉了。下一个token当成普通字符串
                        }
                        else
                        {
                            tok.IsStarted = true;
                            DollarChar = _cur_char;// 进入 $string mode
                            _NextChar();
                        }
                    }
                    return tok;
                }
                case '-':
                    _NextChar();
                    if (_cur_char != '-')
                    {
                        return _NewToken4OneChar('-', _cur_idx - 1);
                    }
                    // is a comment
                    _NextChar();
                    if (_cur_char == '[') // block comment
                    {
                        return _ReadLongComment();
                    }
                    else // line comment
                    {
                        return _ReadLineComment();
                    }
                case '[':
                    _NextChar();
                    if (_cur_char == '=' || _cur_char == '[')
                    {
                        return _ReadRawString();
                    }
                    return _NewToken4OneChar('[', _cur_idx - 1);
                case '=':
                    _NextChar();
                    if (_CheckThenSkip('=')) return _NewToken(TokenType.EQ, _cur_idx-2, _cur_idx);
                    else return _NewToken4OneChar('=', _cur_idx - 1);
                case '<':
                    _NextChar();
                    if (_CheckThenSkip('=')) return _NewToken(TokenType.LE, _cur_idx-2, _cur_idx);
                    else if (_CheckThenSkip('<')) return _NewToken(TokenType.SHIFT_LEFT, _cur_idx-2, _cur_idx);
                    else return _NewToken4OneChar('<', _cur_idx - 1);
                case '>':
                    _NextChar();
                    if (_CheckThenSkip('=')) return _NewToken(TokenType.GE, _cur_idx-2, _cur_idx);
                    else if (_CheckThenSkip('>')) return _NewToken(TokenType.SHIFT_RIGHT, _cur_idx -2, _cur_idx);
                    else return _NewToken4OneChar('>', _cur_idx - 1);
                case '/':
                    _NextChar();
                    if (_CheckThenSkip('/')) return  _NewToken(TokenType.DIVIDE, _cur_idx-2, _cur_idx);
                    else return _NewToken4OneChar('/', _cur_idx - 1);
                case '~':
                    _NextChar();
                    if (_CheckThenSkip('=')) return _NewToken(TokenType.NE, _cur_idx-2, _cur_idx);
                    else return _NewToken4OneChar('~', _cur_idx - 1);
                case '?':
                    _NextChar();
                    if (_CheckThenSkip('?')) return _NewToken(TokenType.QQUESTION, _cur_idx - 2, _cur_idx);
                    else return _NewToken4OneChar('?', _cur_idx - 1);
                case ':':
                    _NextChar();
                    if (_CheckThenSkip(':')) return _NewToken(TokenType.DBCOLON, _cur_idx - 2, _cur_idx);
                    else return _NewToken4OneChar(':', _cur_idx - 1);
                case '\'': case '"':
                    char del = _cur_char;
                    _NextChar();
                    return _ReadString(del);
                case '.': /* '.', '..', '...', or number */
                    _NextChar();
                    if (_CheckThenSkip('.')){
                        if (_CheckThenSkip('.')) return _NewToken(TokenType.DOTS, _cur_idx - 3, _cur_idx);
                        else return _NewToken(TokenType.CONCAT, _cur_idx - 2, _cur_idx);
                    }
                    else if (char.IsDigit(_cur_char)) return _ReadNumber(true);
                    else return _NewToken4OneChar('.', _cur_idx - 1);
                default: {
                    if (char.IsDigit(_cur_char)) return _ReadNumber();
                    else if (IsWordLetter(_cur_char)) return _ReadNameOrKeyword();
                    else if(char.IsAscii(_cur_char)) {/* single-char tokens ('+', '*', '%', '{', '}', ...) */
                        var tok = _NewTokenForCurChar(); // 可能是错误的，由 parser 来 Mark
                        if (DollarChar != '\0') {
                            Debug.Assert(m_dollar_open_cnt > 0);
                            if ('{' == _cur_char) ++m_dollar_open_cnt;
                            else if('}' == _cur_char) --m_dollar_open_cnt;
                        }
                        _NextChar();
                        return tok;
                    }
                    else { // 非法字符
                        var tok = _NewIllegalToken(_cur_idx, 1, "illegal char");
                        _NextChar();
                        return tok;
                    }
                    break;
                }
            }
        }
        return Token.EndOfLine;
    }

    Token _NewTokenForCurChar()
    {
        var tok = new Token(_cur_char);
        tok.SetRange(_cur_idx, _cur_idx+1);
        return tok;
    }

    Token _NewToken4OneChar(char ch, int idx)
    {
        var tok = new Token(ch);
        tok.SetRange(idx, idx+1);
        return tok;
    }

    Token _NewToken(TokenType type, int start, int end)
    {
        var tok = new Token(type);
        tok.SetRange(start, end);
        return tok;
    }

    // 字符串内部的转义字符。
    Token _NewTokenInStr(string msg, int idx, int length){
        var tok = _NewToken(TokenType.STRING, idx, idx + length);
        tok.SetStr(msg);
        return tok;
    }

    Token _NewToken4String(string msg, int idx, int length) {
        var tok = _NewToken(TokenType.STRING, idx, idx+length);
        tok.SetStr(msg);
        return tok;
    }

    Token _NewIllegalToken(int idx, int length, string msg)
    {
        var token = _NewToken(TokenType.Illegal, idx, idx + length);
        token.MarkError(msg);
        return token;
    }

    // 读取字符串Token，可能标记 Started
    Token _ReadLineStringUtil(char del1, char del2){
        var tok = new Token(TokenType.STRING);
        if (del1 == del2){
            tok.AddStrFlag(del1 == '"' ? TokenStrFlag.DoubleQuote : TokenStrFlag.SingleQuote);
        }
        else{
            Debug.Assert(del2 == '$');
            tok.AddStrFlag(del1 == '"' ? TokenStrFlag.DollarDoubleQuote : TokenStrFlag.DollarSingleQuote);
        }
        int start_idx = _cur_idx;
        while (_cur_char != del1 && _cur_char != del2) {
            switch(_cur_char){
                case '\n':
                    tok.MarkError("unfinished string");
                    goto finish_read;
                case '\\': {
                    Token? sub_tok;
                    char c;
                    _NextChar();
                    switch(_cur_char){
                        case 'a': c = '\a'; goto save_one_char;
                        case 'b': c = '\b'; goto save_one_char;
                        case 'f': c = '\f'; goto save_one_char;
                        case 'n': c = '\n'; goto save_one_char;
                        case 'r': c = '\r'; goto save_one_char;
                        case 't': c = '\t'; goto save_one_char;
                        case 'v': c = '\v'; goto save_one_char;
                        case '\\': case '\'': case '\"':
                            c = _cur_char; goto save_one_char;
                        case 'x': sub_tok = _ReadHexEsc(); goto save_tok;
                        case 'u': sub_tok = _ReadUnicodeEsc(); goto save_tok;
                        case '\n': { // 行末 \ ，触发换行
                            sub_tok = _NewTokenInStr("\n", _cur_idx-2, 1);
                            tok.AddSubToken(sub_tok);
                            tok.IsStarted = true;// 换行，触发多行逻辑
                            goto finish_read;
                        }
                        case 'z':{ /* zap following span of spaces，支持换行的 */
                            _NextChar();
                            int zap_num = 2;
                            while(!IsAtEnd && char.IsWhiteSpace(_cur_char)){
                                _NextChar();
                                zap_num++;
                            }
                            sub_tok = _NewTokenInStr("", _cur_idx - zap_num, zap_num);
                            sub_tok.AddStrFlag(TokenStrFlag.ZipMode);
                            if (IsAtEnd) {
                                tok.AddSubToken(sub_tok);
                                tok.IsStarted = true;// 换行，触发多行逻辑
                                tok.AddStrFlag(TokenStrFlag.ZipMode);
                                goto finish_read;
                            }
                            else{
                                goto save_tok;// 继续
                            }
                        }
                        default:{
                            sub_tok = _ReadDigitalEsc();
                            goto save_tok;
                        }
                    }
                    save_one_char:
                        _NextChar();
                        sub_tok = _NewTokenInStr(c.ToString(), _cur_idx-2, 2);
                    save_tok:
                        tok.AddSubToken(sub_tok);
                    break;
                }
                default:
                    _NextChar();
                    break;
            }
        }
        finish_read:
        tok.SetRange(start_idx, _cur_idx);
        return tok;
    }

    // 读取 $string 里的片段。可能设置 IsEnded
    Token _ReadInDollarString()
    {
        char limit_char = DollarChar;
        Debug.Assert(limit_char != '\0' && m_dollar_open_cnt == 0);

        if (_cur_char == '$') {// enter $ mode
            _NextChar();
            if(_cur_char == '{')
            {
                m_dollar_open_cnt = 1;
                _NextChar();
                return _NewToken4OneChar('{', _cur_idx - 1);
            }
            else if (IsWordLetter(_cur_char))
            {
                return _ReadNameOrKeyword(true);
            }
            else if (_cur_char == '$') 
            {
                _NextChar();
                var tk = _NewToken4String("$", _cur_idx - 2, 2);
                return tk;
            }
            else if (_cur_char == limit_char){
                // 允许 $ 结尾 , 结束 $string
                _NextChar();
                var tk = _NewToken4String("$", _cur_idx - 2, 1);
                tk.IsEnded = true;
                return tk;
            }
            else
            {
                // 只吃掉前面的 $
                return _NewIllegalToken(_cur_idx-1, 1, "expect $,<alpha>,{ after '$' in <$string>");
            }
        }
        
        var tok = _ReadLineStringUtil(limit_char, '$');
        if (_cur_char == limit_char) {
            _NextChar();
            tok.IsEnded = true;// 正常结束了
            return tok;
        }
        return tok;
    }

    int _SkipAndCountEqualSign(char flag_ch = '=')
    {
        if (flag_ch == '=') {
            flag_ch = _cur_char;
            _NextChar();
        }
        Debug.Assert(flag_ch == '[' || flag_ch == ']');

        int count = 0;
        while(_cur_char == '='){
            _NextChar();
            count++;
        }
        if (_cur_char == flag_ch){
            return count + 2;
        }
        return 0;// 有问题
    }

    // 尝试读取整段多行字符串。如果一行没有结束，返回false，否则返回true
    bool _TryReadLongStringToEnd(int sep){
        Debug.Assert(_cur_char == '[' && sep >= 2);
        for(;;){
            _NextChar();
            if(_cur_char == ']'){
                if(_SkipAndCountEqualSign() == sep){
                    _NextChar();// eat it
                    return true;// success end
                }
            }
            else if (_cur_char == '\n') {
                return false;
            }
        }
    }

    // --[=*[ comment ]=*]
    Token _ReadLongComment(bool is_middle = false)
    {
        int sep,start_idx;
        if (is_middle){
            sep = m_equal_sep_num;
            start_idx = 0;
            Debug.Assert(sep >= 2 && _cur_idx == 0);
        }
        else{
            Debug.Assert(_cur_char == '[');
            start_idx = _cur_idx - 2;
            sep = _SkipAndCountEqualSign();
        }
        if (sep >= 2){
            // 多行注释
            bool ended = _TryReadLongStringToEnd(sep);
            var tok = new Token(TokenType.Commnet);
            tok.SetRange(start_idx, _cur_idx);
            tok.AddStrFlag(TokenStrFlag.RawComment);
            tok.m_equal_sep_num = sep;
            if (!ended){
                Debug.Assert(IsAtEnd);
                if(IsInDollarMode){
                    // $string 里不支持多行字符串
                    tok.MarkError("raw comment not support multline in $string");
                }
                else{
                    tok.IsStarted = true;
                }
            }
            return tok;
        }
        else{
            // 单行注释
            return _ReadLineComment(start_idx);
        }
    }

    // -- comment
    Token _ReadLineComment(int start_idx = -1)
    {
        if (start_idx < 0){
            start_idx = _cur_idx - 2;
        }
        // 直接到行尾
        var tok = new Token(TokenType.Commnet);
        _cur_idx = _line.Length;// 直接读到最后
        tok.SetRange(start_idx, _line.Length);
        return tok;
    }

    // [=*[ xxx ]=*]
    Token _ReadRawString(bool is_middle = false)
    {
        int sep,start_idx;
        if (is_middle){
            sep = m_equal_sep_num;
            start_idx = 0;
            Debug.Assert(sep >= 2 && _cur_idx == 0);
        }
        else{
            Debug.Assert(_cur_char == '[' || _cur_char == '=');
            start_idx = _cur_idx - 1;
            Debug.Assert(_line[start_idx] == '[');
            sep = _SkipAndCountEqualSign('[');
        }
        if (sep >= 2){
            // 多行字符串
            bool ended = _TryReadLongStringToEnd(sep);
            var tok = new Token(TokenType.STRING);
            tok.AddStrFlag(TokenStrFlag.RawString);
            tok.SetRange(start_idx, _cur_idx);
            tok.m_equal_sep_num = sep;
            if (!ended){
                Debug.Assert(IsAtEnd);
                if(IsInDollarMode){
                    // $string 里不支持多行字符串
                    tok.MarkError("raw string not support multline in $string");
                }
                else{
                    tok.IsStarted = true;
                }
            }
            return tok;
        }
        // invalid format. 没办法
        {
            var tok = new Token(TokenType.Illegal);
            tok.SetRange(start_idx, _cur_idx);
            tok.MarkError("raw string start with '[=*['. miss '['?");
            return tok;
        }
    }

    Token _ReadString(char del){
        var tok = _ReadLineStringUtil(del, del);
        if (_cur_char == del){
            _NextChar();
        }
        else if(IsAtEnd) {
            if (tok.IsStarted) {
                if (IsInDollarMode){
                    tok.MarkError("string not support multline in $string");
                    tok.IsStarted = false;
                }
            }
            else{
                // 意外结束了。
                tok.MarkError("unfinished string");
            }
        }
        return tok;
    }

    // %d(%x|%.|([Ee][+-]?))* | 0[Xx](%x|%.|([Pp][+-]?))*
    // 参考lua 实现比lua还贪心
    Token _ReadNumber(bool is_start_with_dot = false) {
        Debug.Assert(char.IsAsciiDigit(_cur_char));
        int start_idx = _cur_idx;
        if (is_start_with_dot){
            start_idx -= 1;
            Debug.Assert(_line[start_idx] == '.');
        }
        do{
            _NextChar();
        }while(_cur_char == '.' || char.IsAsciiLetterLower(_cur_char));

        var tok = new Token(TokenType.NUMBER);
        tok.SetRange(start_idx, _cur_idx);
        return tok;
    }
}
