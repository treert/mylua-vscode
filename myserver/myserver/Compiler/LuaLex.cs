using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Compiler;

public enum Keyword
{
    AND = TokenType.NAME + 1,// 搞不懂了，一方面不支持enum隐式转换成int，一方面又允许这种语法
    BREAK,
    CONTINUE,// break and continue use exception to implement
    DO,
    ELSE,
    ELSEIF,
    FALSE,
    FOR,
    FUNCTION,
    IF,
    IN,
    LOCAL,
    NIL,
    NOT,
    OR,
    REPEAT,
    RETURN,
    TRUE,
    WHILE,
}

public enum TokenType
{
    EOF = 256,// 不设置成0了，源码里不能有0

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

    NUMBER,
    STRING,

    Illegal,// 各类非法输入。

    // Name，Must place at last
    NAME = 0xFFFF,
}

public class Token
{
    public int RealTokenType
    {
        get
        {
            if (err_msg == null) return (int)TokenType.Illegal;
            return type;
        }
    }
    public int type;
    public long num_int;
    public double num_double;
    public string str;

    public MyString.Range my_range;

    public string err_msg = null;// 如果不为空，说明有错误


    public Token(TokenType tokenType)
    {
        type = (int)tokenType;
    }

    public Token(char c)
    {
        type = (int)c;
    }

    public void SetMyRange(MyString.Range range)
    {
        my_range = range;
    }

    public void MarkError(string msg)
    {
        err_msg = msg;
    }

    public void SetStr(string str)
    {
        this.str = str;
    }

    public Token(string str_, bool is_string) {
        str = str_;
        if(is_string)
        {
            type = (int)TokenType.STRING;
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

    static Dictionary<string, Keyword> keywords = new Dictionary<string, Keyword>();
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
}

public class LexError
{
    public Protocol.Position pos;
    public string msg;
}

public class LuaLex
{
    public List<LexError> m_errors;
    public Token NowToken { get;private set; } = new Token(TokenType.EOF);

    public bool IsInDollarString => dollar_char != '\0' && dollar_open_cnt == 0;

    public Token GetNextToken()
    {
        NowToken = _ReadNextToken();
        return NowToken;
    }

    public void Init(MyString.Range content)
    {
        _content = content;
        _cur_idx = -1;
        _NextChar();// ready for read token

        dollar_char = '\0';
        dollar_open_cnt = 0;
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

    MyString.Range _content;

    char dollar_char;
    int dollar_open_cnt;

    char _cur_char;
    int _cur_idx;

    char _NextChar()
    {
        _cur_idx++;
        if (_cur_idx < _content.Length)
        {
            _cur_char = _content[_cur_idx];
            return _cur_char;
        }
        else
        {
            _cur_idx = _content.Length;
            _cur_char = '\0';
            return '\0';
        }
    }

    bool _CheckThenSkip(char ch)
    {
        bool ok = _cur_char == ch;
        if (ok) { _NextChar(); }
        return ok;
    }

    Token _ReadNameOrKeyword()
    {
        Debug.Assert(IsWordLetter(_cur_char));
        _buf.Clear();
        do
        {
            _buf.Append(_cur_char);
            _NextChar();
        } while (IsWordLetterOrNumber(_cur_char));
        var token = new Token(_buf.ToString(), false);
        return token;
    }

    // Eat Cur Char Then Try Convert To int
    uint _TryEatOneHex()
    {
        char c = _cur_char;
        _NextChar();
        if (Char.IsAsciiHexDigit(c))
        {
            return LexUtil.HexToInt(c);
        }
        return 255;
    }

    // \xdd
    char _ReadHexEsc()
    {
        _NextChar();// skip x
        uint x1 = _TryEatOneHex();
        uint x2 = _TryEatOneHex();
        return '1';

    }

    // \u{aabbccdd}
    char _ReadUnicodeEsc()
    {
        return '1';
    }

    void _ReadStringAndAddToBuffer(char del1, char del2)
    {
        while(_cur_char != del1 && _cur_char != del2)
        {
            switch(_cur_char)
            {
                case '\0':
                    ErrorHappen("unfinished string. reach EOF.");
                    return;
                case '\n':
                    ErrorHappen("unfinished string. reach end of line.");
                    return;
                case '\\':
                    {
                        char c = '\0'; /* final character to be saved */
                        _NextChar();
                        switch (_cur_char)
                        {
                            case 'a': c = '\a'; goto read_save;
                            case 'b': c = '\b'; goto read_save;
                            case 'f': c = '\f'; goto read_save;
                            case 'n': c = '\n'; goto read_save;
                            case 'r': c = '\r'; goto read_save;
                            case 't': c = '\t'; goto read_save;
                            case 'v': c = '\v'; goto read_save;
                            case 'x': _ReadHexEsc(); goto no_save;
                            case 'u': _ReadUnicodeEsc(); goto no_save;
                            case '\n': c = '\n'; goto save;
                            case '\\': case '\"': case '\'':
                                c = _cur_char; goto save;
                            case '\0': goto no_save; /* will raise an error next loop */
                        }
                    read_save:
                        _NextChar();
                    save:
                        _buf.Append(c);
                    no_save:
                        break;
                    }
                default:
                    _buf.Append(_cur_char);
                    _NextChar();
                    break;
            }

        }
    }





    Token _ReadNextToken()
    {
        if (IsInDollarString)
        {
            return _ReadInDollarString();
        }
        
        while (_cur_char != '\0')
        {
            switch(_cur_char)
            {
                case '\r':
                    Debug.Fail("should not happend!");
                    _NextChar();
                    break;
                case '\n': case ' ': case '\f': case '\t': case '\v':
                    _NextChar();// ignore space
                    break;
                case '$':{
                    var tok = _NewTokenForCurChar();
                    _NextChar();
                    if (_cur_char == '"' || _cur_char == '\'')
                    {
                        if (dollar_char != '\0')
                        {
                            tok.MarkError("do not support inner <$string>");
                            // 就不吃掉了。下一个token当成普通字符串
                        }
                        else
                        {
                            dollar_char = _cur_char;
                            Debug.Assert(dollar_open_cnt == 0);
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
                    if (_CheckThenSkip('=')) return _NewTokenByType(TokenType.EQ, _cur_idx-2, 2);
                    else return _NewToken4OneChar('=', _cur_idx - 1);
                case '<':
                    _NextChar();
                    if (_CheckThenSkip('=')) return _NewTokenByType(TokenType.LE, _cur_idx-2, 2);
                    else if (_CheckThenSkip('<')) return _NewTokenByType(TokenType.SHIFT_LEFT, _cur_idx-2, 2);
                    else return _NewToken4OneChar('<', _cur_idx - 1);
                case '>':
                    _NextChar();
                    if (_CheckThenSkip('=')) return _NewTokenByType(TokenType.GE, _cur_idx-2, 2);
                    else if (_CheckThenSkip('>')) return _NewTokenByType(TokenType.SHIFT_RIGHT, _cur_idx -2, 2);
                    else return _NewToken4OneChar('>', _cur_idx - 1);
                case '/':
                    _NextChar();
                    if (_CheckThenSkip('/')) return  _NewTokenByType(TokenType.DIVIDE, _cur_idx-2, 2);
                    else return _NewToken4OneChar('/', _cur_idx - 1);
                case '~':
                    _NextChar();
                    if (_CheckThenSkip('=')) return _NewTokenByType(TokenType.NE, _cur_idx-2, 2);
                    else return _NewToken4OneChar('~', _cur_idx - 1);
                case '?':
                    _NextChar();
                    if (_CheckThenSkip('?')) return _NewTokenByType(TokenType.QQUESTION, _cur_idx - 2, 2);
                    else return _NewToken4OneChar('?', _cur_idx - 1);
                case ':':
                    _NextChar();
                    if (_CheckThenSkip(':')) return _NewTokenByType(TokenType.DBCOLON, _cur_idx - 2, 2);
                    else return _NewToken4OneChar(':', _cur_idx - 1);
                case '\'': case '"':
                    return _ReadString();
                case '.': /* '.', '..', '...', or number */
                    _NextChar();
                    if (_CheckThenSkip('.')){
                        if (_CheckThenSkip('.')) return _NewTokenByType(TokenType.DOTS, _cur_idx - 3, 3);
                        else return _NewTokenByType(TokenType.CONCAT, _cur_idx - 2, 2);
                    }
                    else if (char.IsDigit(_cur_char)) return _ReadNumber(true);
                    else return _NewToken4OneChar('.', _cur_idx - 1);
                default: {
                    if (char.IsDigit(_cur_char)) return _ReadNumber();
                    else if (IsWordLetter(_cur_char)) _ReadNameOrKeyword();
                    else {/* single-char tokens ('+', '*', '%', '{', '}', ...) */
                        var tok = _NewTokenForCurChar(); // 可能是错误的，由 parser 来 Mark
                        if (dollar_char != '\0') {
                            Debug.Assert(dollar_open_cnt > 0);
                            if ('{' == _cur_char) ++dollar_open_cnt;
                            else if('}' == _cur_char) --dollar_open_cnt;
                        }
                        _NextChar();
                        return tok;
                    }
                    break;
                }
            }
        }
        return _NewTokenByType(TokenType.EOF, _cur_idx, 0);
    }

    Token _NewTokenForCurChar()
    {
        var tok = new Token(_cur_char);
        tok.SetMyRange(_content.SubRange(_cur_idx, 1));
        return tok;
    }

    Token _NewToken4OneChar(char ch, int idx)
    {
        var tok = new Token(ch);
        tok.SetMyRange(_content.SubRange(idx, 1));
        return tok;
    }

    Token _NewTokenByType(TokenType type, int idx, int length)
    {
        var tok = new Token(type);
        tok.SetMyRange(_content.SubRange(idx, length));
        return tok;
    }

    Token _NewToken4String(string msg, int idx, int length) {
        var tok = _NewTokenByType(TokenType.STRING, idx, length);
        tok.SetStr(msg);
        return tok;
    }

    Token _ReadInDollarString()
    {
        if (_cur_char == '$') {// enter $ mode
            _NextChar();
            if(_cur_char == '{')
            {
                dollar_open_cnt = 1;
                _NextChar();
                return _NewToken4OneChar('{', _cur_idx - 1);
            }
            else if (IsWordLetter(_cur_char))
            {
                return _ReadNameOrKeyword();
            }
            else if (_cur_char == '$') 
            {
                return // todo@xx
            }
            else
            {
                return _NewIllegalToken(_cur_idx, 1, "expect $,<alpha>,{ after '$' in <$string>");
            }
        }
        while(_cur_char != dollar_char) {
            
        }
        return null;
    }

    Token _ReadLongComment()
    {
        return null;
    }

    Token _ReadLineComment()
    {
        return null;
    }

    Token _ReadRawString()
    {
        return null;
    }

    Token _ReadString(){
        return null;
    }

    Token _ReadNumber(bool is_start_with_dot = false) {
        return null;
    }

    Token _NewIllegalToken(int idx, int length, string msg)
    {
        var token = _NewTokenByType(TokenType.Illegal, idx, length);
        token.MarkError(msg);
        return token;
    }

    Protocol.Position _NowPos()
    {
        return _content.RawString.GetPosByOffset(_cur_idx);
    }

    void ErrorHappen(string msg)
    {
        var err = new LexError
        {
            pos = _NowPos(),
            msg = msg,
        };
        m_errors.Add(err);
    }
}
