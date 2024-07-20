using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        if 

    }

    FunctionBody ParseModule(){
        var block = new BlockTree();
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

    private 

    private LuaLex _lex = new LuaLex();

}