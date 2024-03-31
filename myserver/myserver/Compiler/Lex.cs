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

public class LexError
{
    public Protocol.Position pos;
    public string msg;
}

public class Lex
{
    public List<LexError> m_errors;
    public Token NowToken { get;private set; } = new Token(TokenType.EOF);

    public bool IsInDollarString => dollar_char != '\0' && dollar_open_cnt == 0;

    public Token GetNextToken()
    {
        return NowToken;
    }

    public void Init(MyString.Range content)
    {
        _content = content;
        _cur_idx = -1;
        _NextChar();

        dollar_mode = false;
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

    bool dollar_mode;
    char dollar_char;
    int dollar_open_cnt;

    char _cur_char;
    int _cur_idx;

    char _NextChar()
    {
        _cur_idx++;
        if (_cur_idx < _content.Length)
        {
            return _content[_cur_idx];
        }
        else
        {
            _cur_idx = _content.Length;
            return '\0';
        }
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
                return _ReadNameOrKeyword();
            }
            else
            {
                return _GenIllegalToken("unexpected symbol in <$string>");
            }
        }
        while(_cur_char != dollar_char) {
            
        }
        return null;
    }



    Token _GetNextToken()
    {
        if (IsInDollarString)
        {

        }
        return null;
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

    Token _GenIllegalToken(string msg)
    {
        var token = new Token(TokenType.Illegal);
        token.str = msg;
        return token;
    }
}
