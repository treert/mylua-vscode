using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static MyServer.Protocol.SemanticTokensClientCapabilities;

namespace MyServer.Protocol;

public class WorkspaceSymbolParams
{
    /// <summary>
    /// A query string to filter symbols by.<br/>
    /// Clients may send an empty string here to request all symbols.
    /// </summary>
    public string query { get; set;}
}

/// <summary>
/// A special workspace symbol that supports locations without a range
/// </summary>
public class WorkspaceSymbol
{
    public string name { get; set;}
    public SymbolKind kind { get; set; }
    /// <summary>
    /// The location of this symbol. Whether a server is allowed to
    /// return a location without a range depends on the client
    /// capability `workspace.symbol.resolveSupport`.
    /// </summary>
    public LocationOptRange location { get; set;}
    public SymbolTag[]? tags { get; set; }
    /// <summary>
    /// The name of the symbol containing this symbol. This information is for
    /// user interface purposes(e.g.to render a qualifier in the user interface
    /// if necessary). It can't be used to re-infer a hierarchy for the document
    /// symbols.
    /// </summary>
    public string? containerName { get; set; }
    /// <summary>
    /// A data entry field that is preserved on a workspace symbol between a
    /// workspace symbol request and a workspace symbol resolve request.
    /// </summary>
    public JsonNode? data { get; set; }
}

[MyProto(Direction = ProtoDirection.ToServer)]
public class RpcWorkspaceSymbol : JsonRpcBase<WorkspaceSymbolParams, List<WorkspaceSymbol>>
{
    public override string m_method => "workspace/symbol";

    public override void OnRequest()
    {
        // todo
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// 确定符号的具体位置。但是按这个协议的定义。符号岂不是在一个文件里只能有一个位置？重复定义不支持？是不是我哪里理解错了。
/// </summary>
[MyProto(Direction = ProtoDirection.ToServer)]
public class RpcWorkspaceSymbolResolve : JsonRpcBase<WorkspaceSymbol, WorkspaceSymbol>
{
    public override string m_method => "workspaceSymbol/resolve";

    public override void OnRequest()
    {
        throw new NotImplementedException();
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}

