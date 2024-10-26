using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                case (int)Keyword.IF:
                    statement = ParseIfStatement(); break;
                case (int)Keyword.DO:
                    statement = ParseDoStatement(); break;
                case (int)Keyword.WHILE:
                    statement = ParseWhileStatement(); break;
                case (int)Keyword.FOR:
                    statement = ParseForStatement(); break;
                case (int)Keyword.FUNCTION:
                    statement = ParseFunctionStatement(); break;
                case (int)Keyword.LOCAL:
                    statement = ParseLocalStatement(); break;
                case (int)Keyword.RETURN:
                    statement = ParseReturnStatement(); break;
                case (int)Keyword.BREAK:
                    statement = ParseBreakStatement(); break;
                case (int)Keyword.CONTINUE:
                    statement = ParseContinueStatement(); break;
                case (int)Keyword.GOTO:
                    statement = ParseGotoStatement(); break;
                case (int)Keyword.REPEAT:
                    statement = ParseRepeatStatement(); break;
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

    // 往后读到一个 keyword 为止。中间遇到的全部当成错误
    void GoToNextKeyword(SyntaxTree root_tree, int line_idx_limit)
    {
        using(new MyParseLimitGuard(this, m_tab_size_limit, line_idx_limit)){
            for(;;){
                var tok = LookAhead();
                if (tok.IsKeyword()) return;
                if (tok.IsEndOfLine) return;// 结束了
                root_tree.AddErrToken(NextToken());
            }
        }
    }

    MyParseLimitGuard _NewLimitGurad(Token token, int plus = 0)
    {
        return new MyParseLimitGuard(this, token.TabSize + plus);
    }

    private IfStatement ParseIfStatement()
    {
        var tok_if = NextToken();// if
        Debug.Assert(tok_if.Match(Keyword.IF));

        var statement = new IfStatement();
        Token tok_wait_end = null;
        using(_NewLimitGurad(tok_if)){
            statement.exp = ParseExp();
            GoToNextKeyword(statement, LastToken.RowIdx);
            // then
            if (CheckAndNext(Keyword.THEN)){
                tok_wait_end = LastToken;
                statement.then_branch = ParseBlockLimitByLastToken();
            }
            else{
                statement.AddErrMsgToToken(tok_if, "<if> miss <then>");
                goto the_end;
            }
            GoToNextKeyword(statement, LastToken.RowIdx);
            while(CheckAndNext(Keyword.ELSEIF)){
                var elseif_tk = LastToken;
                var exp = ParseExp();
                GoToNextKeyword(statement, LastToken.RowIdx);
                if (CheckAndNext(Keyword.THEN)){
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
            if (CheckAndNext(Keyword.ELSE)){
                tok_wait_end = LastToken;
                BlockTree block = ParseBlockLimitByLastToken();
            }
            GoToNextKeyword(statement, LastToken.RowIdx);
            the_end:
            if (!CheckAndNext(Keyword.END)){
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
            if (!CheckAndNext(Keyword.END)){
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
        Token tok_wait_end = null;
        using (_NewLimitGurad(tk_while)){
            var exp = ParseExp();
            GoToNextKeyword(statement, LastToken.RowIdx);
            if (CheckAndNext(Keyword.DO)){
                tok_wait_end = LastToken;
                statement.block = ParseBlockLimitByLastToken();
            }
            else{
                CheckAndNext(Keyword.END);// 尽量吃掉一个吧。
                statement.AddErrMsgToToken(tk_while, "miss <do>");
            }
        }
        return statement;
    }

    SyntaxTree ParseForStatement()
    {
        NextToken();// skip 'for'

        if(LookAhead().Match(TokenType.NAME)){
            ThrowParseException($"expect 'id' after 'for'");
        }
        if (LookAhead2().Match('=')){
            return ParseForNumStatement();
        }
        else
        {
            return ParseForInStatement();
        }
    }

    ForNumStatement ParseForNumStatement()
    {
        var statement = new ForNumStatement();
        var name = NextToken();
        NextToken();// skip '='

        statement.name = name;
        statement.exp_init = ParseExp();
        if(!LookAhead().Match(','))
        {
            ThrowParseException("expect ',' in for-num-statement");
        }
        NextToken();// skip ','
        statement.exp_limit = ParseExp();
        if(LookAhead().Match(','))
        {
            NextToken();
            statement.exp_step = ParseExp();
        }

        CheckAndNext(Keyword.DO);
        statement.block = ParseBlock();
        CheckAndNext(Keyword.END);
        return statement;
    }

    ForInStatement ParseForInStatement()
    {
        var statement = new ForInStatement();
        statement.names = ParseNameList();
        CheckAndNext(Keyword.IN);
        statement.exp_list = ParseExpList();
        CheckAndNext(Keyword.DO);
        statement.block = ParseBlock();
        CheckAndNext(Keyword.END);
        return statement;
    }

    List<Token> ParseNameList()
    {
        var list = new List<Token>();
        list.Add(NextToken());
        while(LookAhead().Match(','))
        {
            NextToken();
            if(LookAhead().Match(TokenType.NAME))
            {
                ThrowParseException("expect 'id' after ','");
            }
            list.Add(NextToken());
        }
        return list;
    }

    FunctionStatement ParseFunctionStatement()
    {
        NextToken();// skip 'function'
        var statement = new FunctionStatement();
        statement.names = ParseNameList();
        statement.func_body = ParseFunctionBody();
        return statement;
    }

    List<Token> ParseFunctionName()
    {
        if (LookAhead().Match(TokenType.NAME) == false){
            ThrowParseException("expect 'id' after 'function'");
        }
        var list = new List<Token>();
        list.Add(NextToken());
        while(LookAhead().Match('.'))
        {
            NextToken();
            if(LookAhead().Match(TokenType.NAME))
            {
                ThrowParseException("unexpect token in function name after '.'");
            }
            list.Add(NextToken());
        }
        return list;
    }

    FunctionBody ParseFunctionBody()
    {
        return null;
    }

    SyntaxTree ParseLocalStatement()
    {
        return null;
    }

    SyntaxTree ParseReturnStatement()
    {
        return null;
    }

    SyntaxTree ParseBreakStatement()
    {
        return null;
    }

    SyntaxTree ParseContinueStatement()
    {
        return null;
    }

    SyntaxTree ParseGotoStatement()
    {
        return null;
    }

    SyntaxTree ParseRepeatStatement()
    {
        return null;
    }

    SyntaxTree ParseOtherStatement()
    {
        return null;
    }

    bool CheckAndNext(Keyword keyword)
    {
        var ahead = LookAhead();
        if (ahead.Match(keyword))
        {
            NextToken();
            return true;
        }
        return false;
    }

    ExpSyntaxTree ParseExp(int left_priority = 0)
    {
        return null;
    }

    ExpressionList ParseExpList()
    {
        return null;
    }

    FunctionBody ParseModule(){
        return null;


    }
    public SyntaxTree Parse(MyString.Range content){
        // _lex.Init(content);
        return ParseModule();
    }

    // 解析文件局部形成语法树
    public SyntaxTree Parse(MyFile myfile){
        m_file = myfile;
        m_cur_line = myfile.m_lines.LastOrDefault();
        m_next_tok_idx = 0;
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

    // 如果Token不满足当前限制，返回 EndOfLine。否则返回自己
    Token _ReturnRealToken(Token token)
    {
        if (m_line_idx_limit >= 0){
            // 只认当前行的
            if (token.RowIdx != m_line_idx_limit) return Token.EndOfLine;
        }
        else if(m_tab_size_limit > 0){
            // 强力限制。如果遇到 字符串那种需要触发换行的。特殊处理好了
            if (token.TabSize < m_tab_size_limit) return Token.EndOfLine;
            // keyword must match tab limit
            // if (token.IsKeyword() && token.TabSize < m_tab_size_limit) return Token.EndOfLine;
        }
        return token;
    }

    // 文件内部读取下一个Token. 会把读取指针往后移动一位。
    private Token _ReadNextToken(){
        if (m_cur_line is not null){
            var tok = m_cur_line.GetToken(m_next_tok_idx);
            m_next_tok_idx ++;
            if (tok.IsEndOfLine){
                m_next_tok_idx = 1;// 必然要读一个token。所有一定是 1。
                for (;;) {
                    m_cur_line = m_file.GetLine(m_cur_line.RowIdx + 1);
                    if (m_cur_line is null) break;
                    tok = m_cur_line.GetToken(0);
                    if (tok.IsEndOfLine == false) break;
                }
            }
            return tok;
        }
        return Token.EndOfLine;
    }

    private void ThrowParseException(string message){
        throw new ParseException(message);
    }

    MyFile m_file;
    MyLine? m_cur_line;
    int m_next_tok_idx;

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
    Token m_last_tok = Token.EndOfLine;// 当前的token，也是最近读到的token
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