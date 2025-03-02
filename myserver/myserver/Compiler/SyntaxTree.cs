using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Compiler;

public abstract class SyntaxTree
{
    public List<Token> err_tokens = [];
    public string err_msg = "";
    public Dictionary<Token, string> err_tk_map = [];
    public void AddErrToken(Token token){
        err_tokens.Add(token);
    }

    public void AddErrMsg(string msg){
        if (err_msg == "")
            err_msg = msg;
        else
            err_msg += "\n" + msg;
    }

    public void AddErrMsgToToken(Token token, string msg){
        if (err_tk_map.ContainsKey(token)){
            err_tk_map[token] += "\n" + msg;
        }
        else{
            err_tk_map[token] = msg;
        }
    }

    public virtual bool HasDirectErr => err_msg != "" && err_tk_map.Count == 0 && err_tokens.Count == 0;
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

public class LocalStatement : SyntaxTree
{
    public List<(Token name, Token Attribute)> items = [];
    public ExpressionList exp_list;
}

public class LocalFunctionStatement : SyntaxTree
{
    public Token name = Token.None;
    public FunctionBody function_body;
}

public class FunctionBody : ExpSyntaxTree
{
    public ParamList param_list;
    public bool has_self = false;// 是否有语法糖增加的self
    public BlockTree block;
}

public class ParamList: SyntaxTree
{
    public List<Token> name_list = [];
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

public class GotoStatement : SyntaxTree
{
    public Token label = Token.None;
}


public class InvalidStatement : SyntaxTree
{

}

public class InvalidExp : ExpSyntaxTree
{
    public override bool HasDirectErr => true;
}

public class RepeatStatement: SyntaxTree
{
    public BlockTree block;
    public ExpSyntaxTree exp;
}

public class WhileStatement : SyntaxTree
{
    public ExpSyntaxTree exp;
    public BlockTree block;
}

public class DoStatement : SyntaxTree
{
    public BlockTree block;
}

public class LabelStatement : SyntaxTree
{
    public Token label;
}

public class IfStatement : SyntaxTree
{

    public ExpSyntaxTree exp;
    public BlockTree then_branch;
    public List<(ExpSyntaxTree exp, BlockTree? branch)> elseif_list = null;
    public BlockTree else_branch = null;
}

public class ForNumStatement : SyntaxTree
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
    public ExpressionList exp_list;
    public BlockTree block;
}

public class FunctionStatement : SyntaxTree
{
    public List<Token> names;
    public Token name_after_colon = Token.None;// XXX:Name 的 Name
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
    /// <summary>
    /// token is TokenType.NAME, but use as str
    /// </summary>
    public bool NameUsedAsStr = false;
}

public class BinaryExpression : ExpSyntaxTree
{
    public ExpSyntaxTree left;
    public ExpSyntaxTree right;
    public Token op;
}

public class UnaryExpression : ExpSyntaxTree
{
    public ExpSyntaxTree exp;
    public Token op;
}

public class ArrayDefine : ExpSyntaxTree
{
    public List<ExpSyntaxTree> fields = [];
}

public class TableDefine : ExpSyntaxTree
{
    public List<TableField> fields = [];
}

public class TableField : ExpSyntaxTree
{
    /// <summary>
    /// 数组的索引。lua 里有效的索引 > 0
    /// </summary>
    public int ArrayIdx = 0;
    public ExpSyntaxTree index = null;
    public ExpSyntaxTree value;
}

public class TableAccess : ExpSyntaxTree
{
    public bool has_q;
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