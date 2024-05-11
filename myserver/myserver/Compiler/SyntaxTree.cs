using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Compiler;

public abstract class SyntaxTree
{

}

public abstract class ExpSyntaxTree : SyntaxTree
{

}

#region Lua SyntaxTree

public class BlockTree : SyntaxTree
{
    public List<SyntaxTree> statements = new List<SyntaxTree>();
}

public class FunctionBody : SyntaxTree
{
    public ParamList param_list;
    public BlockTree block;
}

public class ParamList: SyntaxTree
{
    public List<Token> name_list = new List<Token>();
    public Token kw_name = null;
    public bool is_vararg = false;
}

public class ReturnStatement : ExpSyntaxTree
{
    public ExpressionList exp_list;
}

#endregion