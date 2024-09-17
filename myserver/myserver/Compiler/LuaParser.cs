using System;
using System.Collections.Generic;
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
                case (int)TokenType.REPEAT:
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

    private IfStatement ParseIfStatement()
    {
        NextToken();// if or elseif
        var statement = new IfStatement();
        var exp = ParseExp();
        var true_branch = ParseBlock();
        var false_branch = ParseFalseBranchStatement();

        statement.exp = exp;
        statement.true_branch = true_branch;
        statement.false_branch = false_branch;
        return statement;
    }

    SyntaxTree? ParseFalseBranchStatement()
    {
        if (LookAhead().Match(Keyword.ELSEIF)){
            return ParseIfStatement();
        }
        else if (LookAhead().Match(Keyword.ELSE))
        {
            NextToken();
            var block = ParseBlock();
            CheckAndNext(Keyword.END);
            return block;
        }
        else{
            // expect 'end' for 'if'
            CheckAndNext(Keyword.END);
            return null;
        }
    }

    BlockTree ParseDoStatement()
    {
        NextToken();// Skip 'do'
        var statement = ParseBlock();
        CheckAndNext(Keyword.END);
        return statement;
    }

    WhileStatement ParseWhileStatement()
    {
        NextToken();// skip 'while'
        var statement = new WhileStatement();
        var exp = ParseExp();
        CheckAndNext(Keyword.DO);
        var block = ParseBlock();
        CheckAndNext(Keyword.END);

        statement.exp = exp;
        statement.block = block;
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
        
    }

    Token? CheckAndNext(Keyword keyword)
    {
        var ahead = LookAhead();
        if (ahead.Match(keyword))
        {
            return NextToken();
        }
        ThrowParseException($"expect {keyword.ToString().ToLower()}");
        return null;
    }

    ExpSyntaxTree ParseExp()
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
        _lex.Init(content);
        return ParseModule();
    }

    private Token LookAhead(){
        return _lex.LookAhead();
    }

    private Token LookAhead2(){
        return _lex.LookAhead2();
    }

    private Token NextToken(){
        return _lex.NextToken();
    }

    private void ThrowParseException(string message){
        throw new ParseException(message);
    }

    private LuaLex _lex = new LuaLex();

}