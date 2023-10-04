using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyServer.Protocol.LanguageFeatues;

public class CallHierarchyPrepareParams : IWorkDoneProgress, IJson
{
    public ProgressToken? workDoneToken { get; set; }
    public TextDocPosition TextDocPosition { get; set; }

    public virtual void ReadFrom(JsonNode node)
    {
        TextDocPosition = node.ConvertTo<TextDocPosition>();
        workDoneToken = node["workDoneToken"];
    }

    public virtual JsonNode ToJsonNode()
    {
        JsonObject data = TextDocPosition.ToJsonNode().AsObject();
        data.TryAddKeyValue("workDoneToken", workDoneToken);
        return data;
    }
}

public class CallHierarchyItem
{
    public string name { get; set; }
    public SymbolKind kind { get; set; }
    /// <summary>
    /// 现在只有一个 deprecated
    /// </summary>
    public SymbolTag[]? tags { get; set; }
    public string? detail { get; set; }
    public Uri uri { get; set; }
    /// <summary>
    /// The range enclosing this symbol not including leading/trailing whitespace
    /// but everything else, e.g. comments and code.
    /// </summary>
    public Range range { get; set; }
    /// <summary>
    /// The range that should be selected and revealed when this symbol is being
    /// picked, e.g.the name of a function. Must be contained by the
    /// [`range`](#CallHierarchyItem.range).
    /// </summary>
    public Range selectionRange { get; set; }
    public JsonNode? data { get; set; }
}

public class CallHierarchyPrepareResult : IJson
{
    public List<CallHierarchyItem> items = new();
    public void ReadFrom(JsonNode node)
    {
        items = node.ConvertTo<List<CallHierarchyItem>>();
    }

    public JsonNode ToJsonNode()
    {
        return items.ToJsonNode();
    }
}

public class RpcPrepareCallHierarchy : JsonRpcBase<CallHierarchyPrepareParams, GotoDeclareResult>
{
    public override string m_method => "textDocument/prepareCallHierarchy";

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
