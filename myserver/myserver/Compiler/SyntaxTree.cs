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

/*
注释带有类型信息。需要更好的维护。
*/
public abstract class LuaCommentBase : SyntaxTree{
    public bool IsValid{get;} 

}

#region Lua SyntaxTree



public class BlockTree : SyntaxTree
{
    public List<SyntaxTree> statements = [];
}

public class FunctionBody : SyntaxTree
{
    public ParamList param_list;
    public BlockTree block;
}

public class ParamList: SyntaxTree
{
    public List<Token> name_list = [];
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
    public SyntaxTree? false_branch;
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
    public List<Token> names = [];
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
    public List<ExpSyntaxTree> var_list = [];
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
    public List<TableField> fields = [];
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
    public List<ExpSyntaxTree> arg_list = [];
    public List<KW> kw_list = [];
    public List<ExpSyntaxTree> extra_args = [];
}

public class ExpressionList : ExpSyntaxTree
{
    public List<ExpSyntaxTree> exp_list = [];
}

public class DollarString : ExpSyntaxTree
{
    // $string 内部元素，3选一
    public class DollarItem{
        public Token? name;
        public Token? str;
        public ExpSyntaxTree? exp;
    }
    public List<DollarItem> items = [];
}

public class DollarFunction : ExpSyntaxTree
{
    public FunctionBody body;
}


#endregion