using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Compiler;

/**

*/
public class LuaParser {
    
    public SyntaxTree Parse(MyString.Range content){
        _lex.Init(content);
        _current = null;
        _look_ahead = null;
        _look_ahead2 = null;
        return ParseChunk();
    }

    private LuaLex _lex = new LuaLex();
    private Token _current;
    private Token _look_ahead;
    private Token _look_ahead2;

}