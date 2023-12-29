using System;
using System.Collections.Generic;
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
    FINNALY,
    FOR,
    // 像rust一样，使用fn关键字，之前想着用?的，虽然也挺好，但感觉没有fn方便。
    // 之所以不想用function，一是长，二是function是名词。
    FN,
    GLOBAL,
    USING,// like c# using(var st = FileStream(path)){ ... }
    IF,
    IN,
    LOCAL,
    NIL,
    NOT,
    OR,
    RETURN,
    TRUE,
    WHILE,
    // exception handle, not need for finally
    TRY,
    CATCH,
    // FINALLY,
    THROW,
}

public enum TokenType
{
    EOF = 256,// 不设置成0了，源码里不能有0

    DIVIDE, // // 整除
    CONCAT,// .. string concat
    EQ,// ==
    GE,// >=
    LE,// <=
    NE,// !=
    SHIFT_LEFT,// <<
    SHIFT_RIGHT,// >>

    THREE_CMP, // <>，c++里是 <=>，MyScript模版，就简单的<>好了

    // 自加运算系列
    SpecialAssignBegin,
    CONCAT_SELF,// .=
    ADD_SELF,// +=
    DEC_SELF,// -=
    MUL_SELF,// *=
    DIV_SELF,// /=
    MOD_SELF,// %=
    DIVIDE_SELF,// //=
    BIT_AND_SELF,// &=
    BIT_OR_SELF,// |=
    BIT_XOR_SELF,// ~=
    POW_SELF,// ^=
    SpecialAssignSelfEnd,
    ADD_ONE,// ++
    DEC_ONE,// --
    SpecialAssignEnd,

    NUMBER,
    STRING_BEGIN,// 方便词法解析代码编写，字符串可能被$语法打断
    STRING,

    // Name，Must place at last
    NAME = 0xFFFF,
}

public class Token
{
    public int type;
    public long num_int;
    public double num_double;
    public Token(TokenType tokenType)
    {
        type = (int)tokenType;
    }
}

public class Lex
{
    public Token NowToken { get;private set; } = new Token(TokenType.EOF);

    public Token GetNextToken()
    {
        return NowToken;
    }
}
