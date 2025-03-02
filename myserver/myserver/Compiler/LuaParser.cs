using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NLog.LayoutRenderers.Wrappers;

namespace MyServer.Compiler;

/**

*/
public class LuaParser {
    
    BlockTree ParseBlock(){
        var block = new BlockTree();
        for(;;){
            SyntaxTree statement = null;
            var tk_ahead = LookAhead();
            switch(tk_ahead.type){
                case (int)';':
                    NextToken();break;
                case (int)TokenType.DBCOLON:
                    statement = ParseLabelStatement(); break;
                case (int)TokenType.BREAK:
                    statement = ParseBreakStatement(); break;
                case (int)TokenType.CONTINUE:
                    statement = ParseContinueStatement(); break;
                case (int)TokenType.GOTO:
                    statement = ParseGotoStatement(); break;

                case (int)TokenType.DO:
                    statement = ParseDoStatement(); break;

                case (int)TokenType.WHILE:
                    statement = ParseWhileStatement(); break;
                case (int)TokenType.REPEAT:
                    statement = ParseRepeatStatement(); break;

                case (int)TokenType.IF:
                    statement = ParseIfStatement(); break;

                case (int)TokenType.FOR:
                    statement = ParseForStatement(); break;

                case (int)TokenType.FUNCTION:
                    statement = ParseFunctionStatement(); break;
                case (int)TokenType.LOCAL:
                    statement = ParseLocalStatement(); break;

                case (int)TokenType.RETURN:
                    statement = ParseReturnStatement(); break;
                default:
                    statement = ParseOtherStatement();
                    break;
            }
            if (statement == null)
                break;
            block.statements.Add(statement);
        }
        return block;
    }

    BlockTree ParseBlock(int tab_size_limit, int line_idx_limit = -1)
    {
        using(new MyParseLimitGuard(this, tab_size_limit, line_idx_limit))
        {
            return ParseBlock();
        }
    }

    // 解析一个区块，不过缩进限制大于最近的Token。
    BlockTree ParseBlockLimitByLastToken(){
        return ParseBlock(LastToken.TabSize + 1);
    }

    BlockTree ParseBlock(Token token){
        return ParseBlock(token.TabSize + 1);
    }

    /// <summary>
    /// 往后读到一个 keyword 为止。中间遇到的全部当成错误
    /// </summary>
    /// <param name="root_tree"></param>
    void GoToNextKeyword(SyntaxTree root_tree)
    {
        int line_idx_limit = LastToken.RowIdx;
        using(new MyParseLimitGuard(this, m_tab_size_limit, line_idx_limit)){
            for(;;){
                var tok = LookAhead();
                if (tok.IsKeyword()) return;
                if (tok.IsNone) return;// 结束了
                root_tree.AddErrToken(NextToken());
            }
        }
    }

    /// <summary>
    /// 往后读到一个 keyword () {} [] 为止。中间遇到的全部当成错误
    /// </summary>
    /// <param name="tree"></param>
    /// <param name="ch"></param>
    void GoToNextKeywordOrBracket(SyntaxTree tree, char ch = '\0'){
        int line_idx_limit = LastToken.RowIdx;
        using(new MyParseLimitGuard(this, m_tab_size_limit, line_idx_limit)){
            for(;;){
                var tok = LookAhead();
                if (tok.IsNone) return;// 结束了
                if (tok.IsKeyword() 
                    || tok.Match(ch)
                    || tok.Match('(',')')
                    || tok.Match('{','}')
                    || tok.Match('[',']')
                    ) return;
                tree.AddErrToken(NextToken());
            }
        }
    }

    MyParseLimitGuard _NewLimitGurad(Token token, int plus = 0)
    {
        return new MyParseLimitGuard(this, token.TabSize + plus);
    }

    private SyntaxTree ParseLabelStatement(){
        var tok = NextToken();// skip ::
        if (LookAhead().Match(TokenType.NAME)){
            var statement = new LabelStatement();
            statement.label = NextToken();
            if (CheckAndNext(TokenType.DBCOLON) == false){
                statement.AddErrMsgToToken(tok, "miss corresponding ::");
            }
            return statement;
        }
        else {
            var err = new InvalidStatement();
            return err;
        }
    }

    private IfStatement ParseIfStatement()
    {
        var tok_if = NextToken();// if
        Debug.Assert(tok_if.Match(TokenType.IF));

        var statement = new IfStatement();
        Token tok_wait_end = null;
        using(_NewLimitGurad(tok_if)){
            statement.exp = ParseExp();
            GoToNextKeyword(statement);
            // then
            if (CheckAndNext(TokenType.THEN)){
                tok_wait_end = LastToken;
                statement.then_branch = ParseBlockLimitByLastToken();
            }
            else{
                statement.AddErrMsgToToken(tok_if, "<if> miss <then>");
                goto the_end;
            }
            while(CheckAndNext(TokenType.ELSEIF)){
                var elseif_tk = LastToken;
                var exp = ParseExp();
                GoToNextKeyword(statement);
                if (CheckAndNext(TokenType.THEN)){
                    tok_wait_end = LastToken;
                    var block = ParseBlockLimitByLastToken();
                    statement.elseif_list.Add((exp, block));
                }
                else{
                    statement.AddErrMsgToToken(elseif_tk, "<elseif> miss <then>");
                    statement.elseif_list.Add((exp, null));
                    goto the_end;
                }
            }
            if (CheckAndNext(TokenType.ELSE)){
                tok_wait_end = LastToken;
                BlockTree block = ParseBlockLimitByLastToken();
            }
            the_end:
            if (!CheckAndNext(TokenType.END)){
                if (tok_wait_end is not null)
                    statement.AddErrMsgToToken(tok_wait_end, "miss <end>");
            }
            return statement;
        }
    }

    DoStatement ParseDoStatement()
    {
        DoStatement statement = new DoStatement();
        NextToken();// Skip 'do'
        var tk_do = LastToken;
        using(_NewLimitGurad(tk_do))
        {
            statement.block = ParseBlockLimitByLastToken();
            if (!CheckAndNext(TokenType.END)){
                statement.AddErrMsgToToken(tk_do, "miss <end>");
            }
            return statement;
        }
    }

    WhileStatement ParseWhileStatement()
    {
        NextToken();// skip 'while'
        var statement = new WhileStatement();
        var tk_while = LastToken;
        using (_NewLimitGurad(tk_while)){
            statement.exp = ParseExp();
            GoToNextKeyword(statement);
            if (CheckAndNext(TokenType.DO)){
                statement.block = ParseBlockLimitByLastToken();
                if (!CheckAndNext(TokenType.END)){
                    statement.AddErrMsgToToken(tk_while, "miss <end>");
                }
            }
            else{
                CheckAndNext(TokenType.END);// 尽量吃掉一个吧。
                statement.AddErrMsgToToken(tk_while, "miss <do>");
            }
        }
        return statement;
    }

    SyntaxTree ParseForStatement()
    {
        NextToken();// skip 'for'
        var for_tk = LastToken;
        using (_NewLimitGurad(for_tk)){
            if (LookAhead().Match(TokenType.NAME) && LookAhead2().Match('=')){
                return ParseForNumStatement();
            }
            else{
                return ParseForInStatement();
            }
        }
    }

    ForNumStatement ParseForNumStatement()
    {
        var for_tk = LastToken;
        var statement = new ForNumStatement();
        var name = NextToken();// read Name
        NextToken();// skip '='

        statement.name = name;
        statement.exp_init = ParseExp();
        if(LookAhead().Match(','))
        {
            statement.exp_limit = ParseExp();
            if(LookAhead().Match(','))
            {
                NextToken();
                statement.exp_step = ParseExp();
            }
        }
        else {
            statement.exp_limit = new InvalidExp();
            statement.exp_limit.AddErrMsg("expect ',' in for-num-statement");
        }
        GoToNextKeyword(statement);
        if (CheckAndNext(TokenType.DO)){
            statement.block = ParseBlockLimitByLastToken();
            if (!CheckAndNext(TokenType.END)){
                statement.AddErrMsgToToken(for_tk, "miss <end>");
            }
        }
        else{
            CheckAndNext(TokenType.END); // try eat one end
            statement.AddErrMsgToToken(for_tk, "miss <end>");
        }
        return statement;
    }

    ForInStatement ParseForInStatement()
    {
        var statement = new ForInStatement();
        statement.names = ParseNameList();
        if (ExpectAndNextKeyword(statement, TokenType.IN)){
            statement.exp_list = ParseExpList();
            GoToNextKeyword(statement);
        }
        // 即便没找到 in 也继续找 do
        if (ExpectAndNextKeyword(statement, TokenType.DO)){
            statement.block = ParseBlockLimitByLastToken();
        }
        ExpectAndNextKeyword(statement, TokenType.END);
        return statement;
    }

    // 不会报错。但是如果返回的是空数组。外部应该当成是有错的。
    List<Token> ParseNameList()
    {
        var list = new List<Token>();
        if (LookAhead().Match(TokenType.NAME)){
            list.Add(NextToken());
            while (LookAhead().Match(',') && LookAhead2().Match(TokenType.NAME)){
                NextToken();
                list.Add(NextToken());
            }
        }
        return list;
    }

    FunctionStatement ParseFunctionStatement()
    {
        var fn_tk = NextToken();// skip 'function'
        using (_NewLimitGurad(fn_tk)){
            var statement = new FunctionStatement();
            statement.names = ParseNameList();
            if (statement.names.Count == 0){
                statement.AddErrMsg("function miss name");
            }
            if (CheckAndNext(':')){
                if (LookAhead().Match(TokenType.NAME))
                    statement.name_after_colon = NextToken();
                else
                    statement.AddErrMsgToToken(LastToken, "expect <name> after ':'");
            }
            statement.func_body = ParseFunctionBody(fn_tk);
            statement.func_body.has_self = statement.name_after_colon.IsNone == false;
            return statement;
        }
    }

    FunctionBody ParseFunctionBody(Token fn_tk)
    {
        FunctionBody statment = new FunctionBody();
        if (CheckAndNext('(')) {
            statment.param_list = ParseParamList();
        }
        else{
            statment.AddErrMsg("miss '(' to start params");
        }
        statment.block = ParseBlock(fn_tk);
        ExpectAndNextKeyword(statment, TokenType.END);
        return statment;
    }

    ParamList ParseParamList()
    {
        NextToken();// skip '('
        Debug.Assert(LastToken.Match('('));
        ParamList ls = new ParamList();
        ls.name_list = ParseNameList();
        if (CheckAndNext(',')){
            if (ls.name_list.Count == 0){
                ls.AddErrMsgToToken(LastToken, "unexpect ','");
            }
            if (LookAhead().Match(TokenType.DOTS)){
                NextToken();
                ls.is_vararg = true;
            }
        }
        else if (LookAhead().Match(TokenType.DOTS)){
            NextToken();
            ls.is_vararg = true;
            if (ls.name_list.Count > 0){
                ls.AddErrMsgToToken(LastToken, "expect ',' before '...'");
            }
        }
        GoToNextKeywordOrBracket(ls, ')');
        return ls;
    }


    SyntaxTree ParseLocalStatement()
    {
        var loc_tk = NextToken();
        using var _ = _NewLimitGurad(loc_tk);
        if (LookAhead().Match(TokenType.FUNCTION)){
            var fn_tk = NextToken();
            var statement = new LocalFunctionStatement();
            if (LookAhead().Match(TokenType.NAME)){
                statement.name = NextToken();
            }
            statement.function_body = ParseFunctionBody(fn_tk);
            return statement;
        }
        else{
            var statement = new LocalStatement();
            while (LookAhead().Match(TokenType.NAME)){
                var name = NextToken();
                Token attr = Token.None;
                if (CheckAndNext('<')){
                    var left_tk = LastToken;
                    if (CheckAndNext(TokenType.NAME)){
                        attr = LastToken;
                    }
                    else{
                        statement.AddErrMsgToToken(name, "invalid attribute");
                    }
                    if (!CheckAndNext('>')){
                        statement.AddErrMsgToToken(left_tk, "miss '>'");
                    }
                }
                statement.items.Add( (name, attr) );
                if (!CheckAndNext(',')) break;
            }
            if (LookAhead().Match('=')){
                statement.exp_list = ParseExpList();
            }
            return statement;
        }
    }

    SyntaxTree ParseReturnStatement()
    {
        NextToken();
        var statement = new ReturnStatement();
        if (IsMainExp()){
            statement.exp_list = ParseExpList();
        }
        return statement;
    }

    SyntaxTree ParseBreakStatement()
    {
        NextToken();
        return new BreakStatement();
    }

    SyntaxTree ParseContinueStatement()
    {
        NextToken();
        return new ContinueStatement();
    }

    SyntaxTree ParseGotoStatement()
    {
        NextToken();
        var statement = new GotoStatement();
        if (CheckAndNext(TokenType.NAME)){
            statement.label = LastToken;
        }
        else{
            statement.AddErrMsgToToken(LastToken, "expect <Label> after <goto>");
        }
        return statement;
    }

    SyntaxTree ParseRepeatStatement()
    {
        NextToken();
        var statement = new RepeatStatement();
        using var _ = _NewLimitGurad(LastToken);
        statement.block = ParseBlockLimitByLastToken();
        if(ExpectAndNextKeyword(statement, TokenType.UNTIL)){
            statement.exp = ParseExp();
        }
        return statement;
    }

    SyntaxTree ParseOtherStatement()
    {
        return null;
    }

    /// <summary>
    /// 除了 keyword 全部过滤掉，读到 keyword 为之
    /// </summary>
    /// <param name="syntax"></param>
    /// <param name="keyword"></param>
    /// <returns></returns>
    bool ExpectAndNextKeyword(SyntaxTree syntax, TokenType keyword){
        GoToNextKeyword(syntax);
        if (!CheckAndNext(keyword))
            syntax.AddErrMsg($"miss <{keyword.ToString().ToLower()}>");
        return false;
    }

    /// <summary>
    /// 除了 keyword () {} [] 全部过滤掉，读到 ch 为之
    /// </summary>
    /// <param name="syntax"></param>
    /// <param name="ch"></param>
    /// <returns></returns>
    bool ExpectAndNext(SyntaxTree syntax, char ch){
        // 除了 keyword () {} [] 全部过滤掉
        GoToNextKeywordOrBracket(syntax, ch);
        if (!CheckAndNext(ch)){
            syntax.AddErrMsg($"miss '{ch}'");
            return false;
        }
        return true;
    }

    bool CheckAndNext(char ch){
        var ahead = LookAhead();
        if (ahead.Match(ch))
        {
            NextToken();
            return true;
        }
        return false;
    }

    bool CheckAndNext(TokenType tokenType){
        var ahead = LookAhead();
        if (ahead.Match(tokenType))
        {
            NextToken();
            return true;
        }
        return false;
    }

    FunctionBody ParseFunctionDef()
    {
        return null;
    }

    TableField ParseTableIndexField()
    {
        var tok = NextToken();// skip [
        var field = new TableField();
        field.index = ParseExp();
        if (CheckAndNext(']') == false){
            field.AddErrMsgToToken(tok, "miss corresponding ']'");
            // 虽然有错误。但是继续解析下去
        }
        if (CheckAndNext('=') == false){
            field.AddErrMsg("expect '=' <exp>");
            return field;// 没必要继续了
        }
        field.value = ParseExp();
        return field;
    }
    TableField ParseTableNameField()
    {
        Debug.Assert(LookAhead().Match(TokenType.NAME) && LookAhead().Match('='));
        var field = new TableField();
        field.index = new Terminator{token = NextToken(), NameUsedAsStr = true};
        NextToken();// skip '='
        field.value = ParseExp();
        return field;
    }
    TableField ParseTableArrayField()
    {
        var field = new TableField();
        field.index = null;// default is null
        field.value = ParseExp();
        return field;
    }

    TableDefine ParseTableConstructor()
    {
        var start_tok = NextToken();// skip {
        var table = new TableDefine();
        // 考虑缩进
        using(_NewLimitGurad(start_tok, 0)){
            using(_NewLimitGurad(start_tok, 1)){
                TableField last_field = null;
                int array_idx = 0;
                while(LookAhead().Match('}')){
                    if (LookAhead().Match('[')){
                        last_field = ParseTableIndexField();
                    }
                    else if (LookAhead().Match(TokenType.NAME) && LookAhead2().Match('=')){
                        last_field = ParseTableNameField();
                    }
                    else{
                        last_field = ParseTableArrayField();
                        last_field.ArrayIdx = ++array_idx;
                    }

                    table.fields.Add(last_field);

                    if (LookAhead().Match(',', ';')){
                        NextToken();
                        continue;
                    }
                    break;// 可以直接退出了
                }
            }
            if (ExpectAndNext(table, '}') == false){
                table.AddErrMsgToToken(start_tok, "miss corresponding '}'");
            }
        }
        return table;
    }

    ArrayDefine ParseArrayConstructor()
    {
        return null;
    }

    bool IsMainExp() {
        int token_type = LookAhead().type;
        return
            token_type == (int)TokenType.NIL ||
            token_type == (int)TokenType.FALSE ||
            token_type == (int)TokenType.TRUE ||
            token_type == (int)TokenType.NUMBER ||
            token_type == (int)TokenType.STRING ||
            token_type == (int)TokenType.DOTS ||
            token_type == (int)TokenType.FUNCTION ||
            token_type == (int)TokenType.NAME ||
            token_type == (int)'(' ||
            token_type == (int)'{' ||
            token_type == (int)'[' ||
            token_type == (int)'-' ||
            token_type == (int)'~' ||
            token_type == (int)'#' ||
            token_type == (int)'$' ||
            token_type == (int)TokenType.NOT;
    }

    ExpSyntaxTree ParseExp(int left_priority = 0)
    {
        return null;
    }

    TableAccess ParseTableAccessor(ExpSyntaxTree table)
    {
        NextToken();// skip '[' or '.'
        Debug.Assert(LastToken.Match('[', '.'));

        var index_access = new TableAccess();
        index_access.table = table;
        if(LastToken.Match('['))
        {
            var tok = LastToken;
            index_access.index = ParseExp();
            if (CheckAndNext(']') == false){
                index_access.AddErrMsgToToken(tok, "miss match ']'");
            }
        }
        else
        {
            if (CheckAndNext(TokenType.NAME)){
                index_access.index = new Terminator{token = LastToken};
            }
            else{
                index_access.AddErrMsgToToken(LastToken, "expect 'id' after '.'");
            }
        }
        return index_access;
    }

    FuncCall ParseFunctionCall(ExpSyntaxTree caller)
    {
        Debug.Assert(IsArgsCanStart());
        var func_call = new FuncCall();
        func_call.caller = caller;
        func_call.args = ParseArgs();
        return func_call;
    }

    bool IsArgsCanStart(){
        var ahead = LookAhead();
        return ahead.Match('$','(', '{') && ahead.Match(TokenType.STRING);
    }

    ArgsList ParseArgs()
    {
        return null;
    }

    /// <summary>
    /// prefixexp ::= table_index | functioncall | '(' exp ')'
    /// </summary>
    /// <returns></returns>
    ExpSyntaxTree ParsePrefixExp()
    {
        ExpSyntaxTree exp = null;
        if (LookAhead().Match(TokenType.NAME))
        {
            exp = new Terminator{token = NextToken()};
        }
        else if (LookAhead().Match('('))
        {
            var tok = NextToken();// skip (
            exp = ParseExp();
            if (CheckAndNext(')') == false){
                exp.AddErrMsgToToken(tok, "miss corresponding ')'");
            }
        }
        else {
            // 有错误。
            exp = new InvalidExp();
            return exp;// 直接退出吧
        }

        // table index or func call
        while(exp.HasDirectErr == false)
        {
            bool has_q = CheckAndNext('?');
            if (LookAhead().Match('[', '.')){
                var table_access = ParseTableAccessor(exp);
                table_access.has_q = has_q;
                exp = table_access;
            }
            else if(LookAhead().Match(':'))
            {
                NextToken();// skip :
                var table_access = new TableAccess();
                table_access.table = exp;
                exp = table_access;
                if (LookAhead().Match(TokenType.NAME))
                {
                    table_access.index = new Terminator{token = NextToken()};
                    // 后面应该就是函数参数了
                    if (IsArgsCanStart()){
                        exp = ParseFunctionCall(exp);
                    }
                }
                else{
                    table_access.AddErrMsgToToken(LastToken, "expect <Name> after ':'");
                    break;// 有错误，退出
                }
            }
            else if (IsArgsCanStart())
            {
                exp = ParseFunctionCall(exp);
            }
            else
            {
                break;
            }
        }
        return exp;
    }

    ExpSyntaxTree ParseDollarExpr()
    {
        return null;
    }

    ExpSyntaxTree ParseMainExp()
    {
        ExpSyntaxTree exp;
        switch(LookAhead().type)
        {
            case (int)TokenType.NIL:
            case (int)TokenType.FALSE:
            case (int)TokenType.TRUE:
            case (int)TokenType.NUMBER:
            case (int)TokenType.STRING:
            case (int)TokenType.DOTS:
                exp = new Terminator{token = NextToken()};
                break;
            case (int)TokenType.FUNCTION:
                exp = ParseFunctionDef();
                break;
            case '$':
                exp = ParseDollarExpr();
                break;
            case (int)TokenType.NAME:
            case (int)'(':
                exp = ParsePrefixExp();
                break;
            case (int)'{':
                exp = ParseTableConstructor();
                break;
            case (int)'[':
                exp = ParseArrayConstructor();
                break;
            // unop exp priority is 90 less then ^
            case (int)'-':
            case (int)'~':
            case (int)'#':
            case (int)TokenType.NOT:
                var unexp = new UnaryExpression();
                unexp.op = NextToken();
                unexp.exp = ParseExp(90);
                exp = unexp;
                break;
            default:
                NextToken();
                exp = new InvalidExp();
                break;
        }
        return exp;
    }

    ExpressionList ParseExpList()
    {
        return null;
    }

    FunctionBody ParseModule(){
        return null;
    }

    // 解析文件局部形成语法树
    public SyntaxTree Parse(MyFile myfile){
        _inner_next_tok = myfile.GetFirstToken();

        m_ahead_tok = null;
        m_ahead2_tok = null;
        m_tab_size_limit = 0;
        m_line_idx_limit = -1;
        return null;
    }

    private Token LookAhead(){
        if (m_ahead_tok == null){
            m_ahead_tok = _ReadNextToken();
        }
        return _ReturnRealToken(m_ahead_tok);
    }

    private Token LookAhead2(){
        LookAhead();
        if (m_ahead2_tok == null){
            m_ahead2_tok = _ReadNextToken();
        }
        return _ReturnRealToken(m_ahead2_tok);
    }

    Token LastToken => m_last_tok;

    // 所有的读取都要经过这个，会设置 m_last_tok, 并且会执行 _ReturnRealToken 逻辑
    Token NextToken(){
        if (m_ahead_tok != null){
            var tok = m_ahead_tok;
            m_ahead_tok = m_ahead2_tok;
            m_ahead2_tok = null;
            m_last_tok = _ReturnRealToken(tok);
        }
        else{
            m_last_tok = _ReturnRealToken(_ReadNextToken());
        }
        return m_last_tok;
    }

    // 如果Token不满足当前限制，返回 None。否则返回自己
    Token _ReturnRealToken(Token token)
    {
        if (m_line_idx_limit >= 0){
            // 只认当前行的
            if (token.RowIdx != m_line_idx_limit) return Token.None;
        }
        else if(m_tab_size_limit > 0){
            // 强力限制。如果遇到 字符串那种需要触发换行的。特殊处理好了
            if (token.TabSize < m_tab_size_limit) return Token.None;
            // keyword must match tab limit
            // if (token.IsKeyword() && token.TabSize < m_tab_size_limit) return Token.None;
        }
        return token;
    }

    // 文件内部读取下一个Token. 会把读取指针往后移动一位。
    // 会过滤掉 comment
    private Token _ReadNextToken(){
        var tok = _inner_next_tok;
        while(tok.IsComment) tok = tok.NextToken;
        _inner_next_tok = tok.NextToken;
        return tok;
    }

    private void ThrowParseException(string message){
        throw new ParseException(message);
    }

    Token _inner_next_tok = Token.None;// 内部用这个token往后读

    class MyParseLimitGuard : IDisposable
    {
        public MyParseLimitGuard(LuaParser parser, int tab_size_limit, int line_idx_limit = -1){
            Parser = parser;
            pre_line_idx_limit = parser.m_line_idx_limit;
            pre_tab_size_limit = parser.m_tab_size_limit;
            parser.m_line_idx_limit = line_idx_limit;
            parser.m_tab_size_limit = tab_size_limit;
        }
        public void Dispose()
        {
            Parser.m_line_idx_limit = pre_line_idx_limit;
            Parser.m_tab_size_limit = pre_tab_size_limit;
        }

        LuaParser Parser;
        int pre_line_idx_limit = -1;
        int pre_tab_size_limit = 0;
    }

    int m_line_idx_limit = -1;// 限制只读取特定行的token
    int m_tab_size_limit = 0;// 限制token需要满足缩进规则
    Token m_last_tok = Token.None;// 当前的token，也是最近读到的token
    Token? m_ahead_tok = null;
    Token? m_ahead2_tok = null;
}


/*
新的想法：先用1-2个pass初步吧 Token List 分解成语法树半成品，然后在细处理每个语法树节点。

未想好的地方：
1. 代码如何组织的问题。
2. 对于在中间修改源文件的情况如何高效实现。【这个更难一点】
*/


/*
先用括号匹配的方式，初步划分出区块，就像脚手架一样。
i
*/



public class LuaTokenRange{
    public void XX(){

    }
}