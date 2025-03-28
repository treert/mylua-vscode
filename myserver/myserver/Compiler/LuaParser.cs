using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

    BlockTree ParseBlock(int tab_size_limit, int exclude_line_idx)
    {
        using(new MyParseLimitTabGuard(this, tab_size_limit, exclude_line_idx))
        {
            return ParseBlock();
        }
    }

    // 解析一个区块，不过缩进限制大于最近的Token。
    BlockTree ParseBlockLimitByLastToken(){
        return ParseBlock(LastToken.TabSize + 1, LastToken.RowIdx);
    }

    BlockTree ParseBlock(Token token){
        return ParseBlock(token.TabSize + 1, token.RowIdx);
    }

    /// <summary>
    /// 往后读到一个 keyword 为止。中间遇到的全部当成错误
    /// </summary>
    /// <param name="root_tree"></param>
    void GoToNextKeyword(SyntaxTree root_tree)
    {
        int line_idx_limit = LastToken.RowIdx;
        using(new MyParseLimitLineGuard(this, line_idx_limit)){
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
        using(new MyParseLimitLineGuard(this, line_idx_limit)){
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

    MyParseLimitTabGuard _NewLimitTabGurad(Token token, int plus = 0)
    {
        return new MyParseLimitTabGuard(this, token.TabSize + plus, token.RowIdx);
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
        using(_NewLimitTabGurad(tok_if)){
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
        using(_NewLimitTabGurad(tk_do))
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
        using (_NewLimitTabGurad(tk_while)){
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
        using (_NewLimitTabGurad(for_tk)){
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
        Debug.Assert(name.Match(TokenType.NAME) && LastToken.Match('='));

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

    /// <summary>
    /// [Name {',' Name}]
    /// </summary>
    /// <returns></returns>
    List<Token> ParseNameList()
    {
        var list = new List<Token>();
        if (LookAhead().Match(TokenType.NAME)){
            list.Add(NextToken());
            while (LookAhead().Match(',') && LookAhead2().Match(TokenType.NAME)){
                NextToken();// skip ,
                list.Add(NextToken());
            }
        }
        return list;
    }

    FunctionStatement ParseFunctionStatement()
    {
        var fn_tk = NextToken();// skip 'function'
        using (_NewLimitTabGurad(fn_tk)){
            var statement = new FunctionStatement();
            // [Name {'.' Name}]
            if (LookAhead().Match(TokenType.NAME)){
                statement.names.Add(NextToken());
                while (LookAhead().Match(',') && LookAhead2().Match(TokenType.NAME)){
                    NextToken();// skip .
                    statement.names.Add(NextToken());
                }
            }
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
        using var _ = _NewLimitTabGurad(fn_tk);
        FunctionBody statment = new FunctionBody();
        statment.is_dollar_func = fn_tk.Match('$');
        if (statment.is_dollar_func){
            if (LookAhead().Match('(')) {
                statment.param_list = ParseParamList();
            }
            if (CheckAndNext('{')) {
                statment.block = ParseBlock(LastToken);
                ExpectAndNext(statment, '}');
            }
            else {
                statment.AddErrMsg("$function miss { funcbody }");
            }
        }
        else {
            if (LookAhead().Match('(')) {
                statment.param_list = ParseParamList();
            }
            else {
                statment.AddErrMsg("miss '(' to start params");
            }
            statment.block = ParseBlock(fn_tk);
            ExpectAndNextKeyword(statment, TokenType.END);
        }
        return statment;
    }

    ParamList ParseParamList()
    {
        NextToken();// skip '('
        using var _ = _NewLimitTabGurad(LastToken);
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
        using var _ = _NewLimitTabGurad(loc_tk);
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
            if (CheckAndNext('=')){
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
        using var _ = _NewLimitTabGurad(LastToken);
        statement.block = ParseBlockLimitByLastToken();
        if(ExpectAndNextKeyword(statement, TokenType.UNTIL)){
            statement.exp = ParseExp();
        }
        return statement;
    }

    /// <summary>
    /// 如果 return null, 表示 Block 结束。
    /// </summary>
    /// <returns></returns>
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

    FunctionBody ParseFunctionExp()
    {
        var fn_tok = NextToken();
        return ParseFunctionBody(fn_tok);
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
        Debug.Assert(LookAhead().Match(TokenType.NAME) && LookAhead2().Match('='));
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
        using(_NewLimitTabGurad(start_tok, 0)){
            using(_NewLimitTabGurad(start_tok, 1)) {
                TableField last_field = null;
                int array_idx = 0;
                while(LookAhead().Match('}') == false) {
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
        var start_tok = NextToken();// skip [
        var array = new ArrayDefine();
        // 考虑缩进
        using(_NewLimitTabGurad(start_tok, 0)){
            using(_NewLimitTabGurad(start_tok, 1)){
                while(IsMainExp()){
                    var exp = ParseExp();
                    array.fields.Add(exp);
                    if (CheckAndNext(',') == false) break;
                }
            }
            if (ExpectAndNext(array, ']') == false){
                array.AddErrMsgToToken(start_tok, "miss corresponding '}'");
            }
        }
        return array;
    }

    bool IsMainExp(Token token = null) {
        int token_type = token != null ? token.type :LookAhead().type;
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

    /// <summary>
    /// 二元运算符的优先级，和lua一样。不知为啥c++的比较运算优先级高于位运算。
    /// > http://www.lua.org/manual/5.4/manual.html#3.4.8
    /// > https://en.cppreference.com/w/cpp/language/operator_precedence
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    int GetOpPriority(Token t)
    {
        switch (t.type)
        {
            case (int)TokenType.QQUESTION: return 1;// ??
            case (int)TokenType.OR: return 10;
            case (int)TokenType.AND: return 20;
            case (int)TokenType.NE:
            case (int)TokenType.EQ: // return 25;// 想了想，和lua保持一致吧
            case '>':
            case '<':
            case (int)TokenType.GE:
            case (int)TokenType.LE: return 30;
            case (int)TokenType.DOTS: return 31;
            case '|': return 31;
            case '~': return 32;
            case '&': return 33;
            case (int)TokenType.SHIFT_LEFT:
            case (int)TokenType.SHIFT_RIGHT: return 40;
            case (int)TokenType.CONCAT: return 50;// lua 把字符串连接的优先级放这儿，有什么特别考虑吗？感觉不合适呀
            case '+':
            case '-': return 80;
            case '*':
            case '/':
            case (int)TokenType.DIVIDE:
            case '%': return 90;
            case '^': return 100;
            
            default: return 0;// 无效的运算符也是0
        }
    }
    bool IsRightAssociation(Token t)
    {
        return t.type == (int)'^' || t.type == (int)TokenType.QQUESTION || t.type == (int)TokenType.DOTS;
    }

    ExpSyntaxTree ParseExp(int left_priority = 0)
    {
        var exp = ParseMainExp();
        while (exp.HasDirectErr == false){
            // 针对二目算符优先文法的算法
            int right_priority = GetOpPriority(LookAhead());// 如果错误的Token。会自然的退出
            if (left_priority < right_priority || (left_priority == right_priority && IsRightAssociation(LookAhead())))
            {
                // C++的函数参数执行顺序没有明确定义，方便起见，不在函数参数里搞出两个有依赖的函数调用，方便往C++里迁移
                var op = NextToken();
                exp = new BinaryExpression{
                    left = exp,
                    right = ParseExp(right_priority),
                    op = op,
                };
            }
            else
            {
                break;
            }

        }
        return exp;
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

    /// <summary>
    /// 由于按行解析的。多行字符串被拆分成了很多段。
    /// </summary>
    /// <returns></returns>
    ExpSyntaxTree ParseNormalStringExp()
    {
        var start_tok = NextToken();
        Debug.Assert(start_tok.Match(TokenType.STRING));
        var exp = new NormalStringExp();
        exp.segs.Add(start_tok);
        while (LastToken.IsStarted && !LastToken.IsEnded) {
            if (LookAhead().Match(TokenType.STRING)){
                exp.segs.Add(NextToken());
            }
            else {
                Debug.Assert(LookAhead().IsNone);
                break;
            }
        }
        if (LastToken.IsEnded == false){
            exp.AddErrMsgToToken(start_tok, $"miss {start_tok.StringLimitChar} to end string");
        }
        else if (LastToken.HasError){
            // 可有可无
            // exp.AddErrMsgToToken(LastToken, LastToken.ErrMsg);
        }
        return exp;
    }

    ExpSyntaxTree ParseDollarStringExp()
    {
        Debug.Assert(LastToken.Match('$') && LastToken.IsStarted);
        var dollar_tok = LastToken;
        var dollar_str = new DollarStringExp();
        if (LastToken.IsEnded == false){
            // 一行一行的读
            int line_num = LastToken.RowIdx;
            for(;;){
                {
                    using var _ = new MyParseLimitLineGuard(this, line_num);
                    while(LookAhead().IsNone == false){
                        if (LookAhead().TestStrFlag(TokenStrFlag.Dollar)){
                            dollar_str.AddErrToken(NextToken());
                            continue;
                        }
                        if (LookAhead().Match('{')){
                            // ${} mode
                            var seg = new DollarStringSegExp();
                            var exp_start = NextToken();
                            seg.exp = ParseExp();
                            if (CheckAndNext('}') == false){
                                seg.AddErrMsgToToken(exp_start, "miss corresponding '}'");
                            }
                            dollar_str.segs.Add(seg);
                        }
                        else if(LookAhead().Match(TokenType.STRING)){
                            var seg = new DollarStringSegExp();
                            seg.str_or_name_seg = NextToken();
                            dollar_str.segs.Add(seg);
                            if (LastToken.IsEnded){
                                goto finish_read;// 正确结束解析
                            }
                            if (LastToken.IsStarted){
                                Debug.Assert(LookAhead().IsNone);
                                break;// 下一行
                            }
                        }
                        else if(LookAhead().Match(TokenType.NAME)){
                            var seg = new DollarStringSegExp();
                            seg.str_or_name_seg = NextToken();
                            dollar_str.segs.Add(seg);
                        }
                        else{
                            Debug.Assert(false);
                        }
                    }
                }
                // 下一行
                if (LastToken.IsStarted && LookAhead().IsNone == false){
                    line_num ++;
                    continue;
                }
                else{
                    // 有什么意外发生了，$string 没有正确结束
                    dollar_str.AddErrMsgToToken(dollar_tok, "$string miss end.");
                }
            }
            finish_read:;
        }
        else {
            // $" 结尾的。
            dollar_str.AddErrMsg("empty $string unfinished");
        }
        return dollar_str;
    }

    ExpSyntaxTree ParseDollarExpr()
    {
        var dollar_tok = NextToken();// skip $
        if (dollar_tok.IsStarted) {
            // $string
            return ParseDollarStringExp();
        }
        else if (LookAhead().Match('(','{')){
            // $function
            return ParseFunctionBody(LastToken);
        }
        else {
            var exp = new InvalidExp();
            return exp;
        }
    }

    /// <summary>
    /// 需要注意：返回 InvalidExp 时。不会调用 NextToken, 上层逻辑需要注意不要出现死循环
    /// </summary>
    /// <returns></returns>
    ExpSyntaxTree ParseMainExp()
    {
        ExpSyntaxTree exp;
        switch(LookAhead().type)
        {
            case (int)TokenType.NIL:
            case (int)TokenType.FALSE:
            case (int)TokenType.TRUE:
            case (int)TokenType.NUMBER:
            case (int)TokenType.DOTS:
                exp = new Terminator{token = NextToken()};
                break;
            case (int)TokenType.STRING:
                exp = ParseNormalStringExp();
                break;
            case (int)TokenType.FUNCTION:
                exp = ParseFunctionExp();
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
                // NextToken();// 有可能是 ')' 这种情况下不能吃掉。最后想一想。统一不吃掉
                exp = new InvalidExp();
                break;
        }
        return exp;
    }

    ExpressionList ParseExpList()
    {
        var exp = new ExpressionList();
        if (IsMainExp()) {
            exp.exp_list.Add(ParseExp());
            while (CheckAndNext(',')) {
                if (IsMainExp()){
                    exp.exp_list.Add(ParseExp());
                }
                else {
                    exp.AddErrMsgToToken(LastToken, "Expect <exp> after ',' in <exp_list>");
                    break;
                }
            }
        }
        else {
            exp.AddErrMsg("Expect <exp_list>");
        }
        return exp;
    }

    FunctionBody ParseModule(){
        FunctionBody statment = new FunctionBody();
        statment.param_list = new ParamList{is_vararg = true};
        statment.block = ParseBlock();
        return statment;
    }

    // 解析文件
    public SyntaxTree Parse(MyFile myfile){
        _inner_next_tok = myfile.GetFirstToken();

        m_ahead_tok = null;
        m_ahead2_tok = null;
        m_tab_size_limit = 0;
        m_line_idx_limit = -1;
        return ParseModule();
    }

    private Token LookAhead(){
        if (m_ahead_tok == null){
            m_ahead_tok = _ReadNextToken();
        }
        return _Convert2RealToken(m_ahead_tok);
    }

    private Token LookAhead2(){
        LookAhead();
        if (m_ahead2_tok == null){
            m_ahead2_tok = _ReadNextToken();
        }
        return _Convert2RealToken(m_ahead2_tok);
    }

    Token LastToken => m_last_tok;

    // 所有的读取都要经过这个，会设置 m_last_tok, 并且会执行 _Convert2RealToken 逻辑
    Token NextToken(){
        m_last_tok = LookAhead();
        // _Convert2RealToken 可能截断，不实际读取。需要判断下。真的发生读取，才推进
        if (m_last_tok == m_ahead_tok) {
            m_ahead_tok = m_ahead2_tok;
            m_ahead2_tok = null;
        }
        return m_last_tok;
    }

    // 如果Token不满足当前限制，返回 None。否则返回自己
    Token _Convert2RealToken(Token token)
    {
        if (m_line_idx_limit >= 0){
            // 只认当前行的
            if (token.RowIdx != m_line_idx_limit) return Token.None;
        }
        else if(m_tab_size_limit > 0){
            if (token.RowIdx == _tab_limit_exclue_line_idx) return token;// 豁免
            // 字符串和注释存在换行的情况。不做任何限制
            // 强力限制。
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
        _inner_next_tok = tok.NextToken;// None will return None
        return tok;
    }

    Token _inner_next_tok = Token.None;// 内部用这个token往后读

    class MyParseLimitLineGuard : IDisposable
    {
        public MyParseLimitLineGuard(LuaParser parser, int line_idx_limit){
            Parser = parser;
            pre_line_idx_limit = Parser.m_line_idx_limit;
            Parser.m_line_idx_limit = line_idx_limit;
        }

        public void Dispose(){
            Parser.m_line_idx_limit = pre_line_idx_limit;
        }
        
        LuaParser Parser;
        int pre_line_idx_limit;
    }

    class MyParseLimitTabGuard : IDisposable
    {
        public MyParseLimitTabGuard(LuaParser parser, int tab_size_limit, int tab_limit_exclue_line_idx){
            Parser = parser;
            pre_tab_limit_exclue_line_idx = tab_limit_exclue_line_idx;
            pre_tab_size_limit = Parser.m_tab_size_limit;
            Parser.m_tab_size_limit = tab_size_limit;
            Parser._tab_limit_exclue_line_idx = tab_limit_exclue_line_idx;
        }

        public void Dispose(){
            Parser.m_tab_size_limit = pre_tab_size_limit;
            Parser._tab_limit_exclue_line_idx = pre_tab_limit_exclue_line_idx;
        }
        
        LuaParser Parser;
        int pre_tab_size_limit;
        int pre_tab_limit_exclue_line_idx;
    }

    int m_line_idx_limit = -1;// 限制只读取特定行的token。优先级更高
    int m_tab_size_limit = 0;// 限制token需要满足缩进规则
    int _tab_limit_exclue_line_idx = -1;

    Token m_last_tok = Token.None;// 当前的token，也是最近读到的token
    Token? m_ahead_tok = null;
    Token? m_ahead2_tok = null;
}
