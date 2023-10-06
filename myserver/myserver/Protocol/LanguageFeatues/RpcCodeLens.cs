
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyServer.Protocol.LanguageFeatues;

/// <summary>
/// A code lens represents a command that should be shown along with
/// source text, like the number of references, a way to run tests, etc.
/// 
/// A code lens is _unresolved_ when no command is associated to it.For
/// performance reasons the creation of a code lens and resolving should be done
/// in two stages.
/// </summary>
public class CodeLens
{
    /// <summary>
    /// The range in which this code lens is valid. Should only span a single line.
    /// </summary>
    public Range range {  get; set; }
    /// <summary>
    /// The command this code lens represents.
    /// </summary>
    public Command? command { get; set; }
    /// <summary>
    /// A data entry field that is preserved on a code lens item between
    /// a code lens and a code lens resolve request.
    /// </summary>
    public JsonNode? data { get; set; }
}

public class RpcCodeLens : JsonRpcBase<DocIdAndTokenParams, List<CodeLens>>
{
    public override string m_method => "textDocument/codeLens";

    public override void OnCanceled()
    {
        // todo
    }

    public override void OnRequest()
    {
        // todo
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}

public class RpcCodeLensResolve : JsonRpcBase<CodeLens, CodeLens>
{
    public override string m_method => "codeLens/resolve";

    public override void OnCanceled()
    {
        // todo
    }

    public override void OnRequest()
    {
        // todo
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}

public class RpcCodeLensRefresh : JsonRpcBase<Dummy, Dummy>
{
    public override string m_method => "workspace/codeLens/refresh";

    public override void OnCanceled()
    {
        throw new NotImplementedException();
    }

    public override void OnRequest()
    {
        throw new NotImplementedException();
    }

    protected override void OnSuccess()
    {
        // todo
    }
}


