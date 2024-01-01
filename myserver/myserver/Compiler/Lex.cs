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
    FINNALY,
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
    EQ,// ==
    GE,// >=
    LE,// <=
    NE,// ~=
    SHIFT_LEFT,// <<
    SHIFT_RIGHT,// >>

    NUMBER,
    STRING,

    Illegal,// 各类非法输入。

    // Name，Must place at last
    NAME = 0xFFFF,
}

public class Token
{
    public int type;
    public long num_int;
    public double num_double;
    public string str;
    public Token(TokenType tokenType)
    {
        type = (int)tokenType;
    }

    public Token(char c)
    {
        type = (int)c;
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

public class Lex
{
    public Token NowToken { get;private set; } = new Token(TokenType.EOF);

    public bool IsInDollarString => dollar_char != '\0' && dollar_open_cnt == 0;

    public Token GetNextToken()
    {
        return NowToken;
    }

    public void Init(string[] rows)
    {
        _rows = rows;
        _line = 0;
        _column = 0;
        _cur_char = '\0';
        _cur_row = null;

        dollar_mode = false;
        dollar_char = '\0';
        dollar_open_cnt = 0;


        // set first char
        if (_rows.Length > 0 )
        {
            _cur_row = _rows[_line];
            _column = -1;// trick
            _NextChar();
        }
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

    string[] _rows;
    string? _cur_row;

    bool dollar_mode;
    char dollar_char;
    int dollar_open_cnt;

    char _cur_char;
    int _line;
    int _column;
    char _NextChar()
    {
        if(_cur_row != null)
        {
            _column++;
            if (_column < _cur_row.Length)
            {
                _cur_char = _cur_row[_column];
            }
            else if (_column == _cur_row.Length)
            {
                // 换行符算当前行的最后一个
                _cur_char = '\n';
            }
            else // 换行
            {
                _line++;
                _column = 0;
                if (_line < _rows.Length)
                {
                    _cur_row = _rows[_line];
                    _cur_char = (_cur_row.Length > 0) ? _cur_row[0] : '\n';
                }
                else
                {
                    _cur_row = null;// 结束了。
                    _cur_char = '\0';
                }
            }

            return _cur_char;
        }
        else
        {
            // 已经到未见尾了
            _cur_char = '\0';
            return _cur_char;
        }
    }

    Token _ReaadNameOrKeyword()
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

    Token _ReadInDollarString()
    {
        if (dollar_mode)
        {
            dollar_mode = false;// close dollar mode
            if(_cur_char == '{')
            {
                dollar_open_cnt = 1;
                _NextChar();
                return new Token('{');
            }
            else if (IsWordLetter(_cur_char))
            {
                return _ReaadNameOrKeyword();
            }
            else
            {
                return _GenIllegalToken("unexpected symbol in <$string>");
            }
        }
        while(_cur_char != dollar_char) {
            
        }
    }



    Token _GetNextToken()
    {
        if (IsInDollarString)
        {

        }
        return null;
    }

    Token _GenIllegalToken(string msg)
    {
        var token = new Token(TokenType.Illegal);
        token.str = msg;
        return token;
    }
}
