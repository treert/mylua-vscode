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

public class BreakStatement : SyntaxTree
{

}

public class ContinueStatement : SyntaxTree
{

}

public class WhileStatement : SyntaxTree
{
    public ExpSyntaxTree exp;
    public BlockTree block;
}

public class IfStatement : SyntaxTree
{
    public ExpSyntaxTree exp;
    public BlockTree true_branch;
    public SyntaxTree false_branch;
}

public class ForStatement : SyntaxTree
{
    public Token name;
    public ExpSyntaxTree exp_init;
    public ExpSyntaxTree exp_limit;
    public ExpSyntaxTree exp_step;
    public BlockTree block;
}

public class ForInStatement : SyntaxTree
{
    public List<Token> names = new List<Token>();
    public ExpSyntaxTree exp;
    public BlockTree block;
}

public class FunctionStatement : SyntaxTree
{
    public List<Token> names;
    public FunctionBody func_body;
}

public class AssignStatement : SyntaxTree
{
    public List<ExpSyntaxTree> var_list = new List<ExpSyntaxTree>();
    public ExpressionList exp_list;
}

public class Terminator : ExpSyntaxTree
{
    public Token token;
}

public class BinaryExpression : SyntaxTree
{
    public ExpSyntaxTree left;
    public ExpSyntaxTree right;
    public Token op;
}

public class UnaryExpression : SyntaxTree
{
    public ExpSyntaxTree exp;
    public Token op;
}

public class TableDefine : ExpSyntaxTree
{
    public List<TableField> fields = new List<TableField>();
}

public class TableField : ExpSyntaxTree
{
    public ExpSyntaxTree index = null;
    public ExpSyntaxTree value;
}

public class TableAccess : ExpSyntaxTree
{
    public ExpSyntaxTree table;
    public ExpSyntaxTree index;
}

public class FuncCall : ExpSyntaxTree
{
    public ExpSyntaxTree caller;
    public ArgsList args;
}

public class ArgsList : SyntaxTree
{
    public class KW
    {
        public Token k;// name
        public ExpSyntaxTree w;
    }
    public List<ExpSyntaxTree> arg_list = new List<ExpSyntaxTree>();
    public List<KW> kw_list = new List<KW>();
    public List<ExpSyntaxTree> extra_args = new List<ExpSyntaxTree>();
}

public class ExpressionList : ExpSyntaxTree
{
    public List<ExpSyntaxTree> exp_list = new List<ExpSyntaxTree>();
}

public class DollarString : ExpSyntaxTree
{
    public class DollarItem{
        public Token? name;
        public Token? str;
        public ExpSyntaxTree? exp;
    }
}

#endregion