using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol.LanguageFeatues;

public class ReferenceContext
{
    /// <summary>
    /// Include the declaration of the current symbol.
    /// </summary>
    public bool includeDeclaration { get; set; }
}

public class FindReferenceParams : PosAndTokenParams
{
    public ReferenceContext context = new ReferenceContext();
    public override void ReadFrom(JsonNode node)
    {
        base.ReadFrom(node);
        context = node["context"]!.ConvertTo<ReferenceContext>();
    }

    public override JsonNode ToJsonNode()
    {
        var data = base.ToJsonNode().AsObject();
        data.AddKeyValue("context", context);
        return data;
    }
}

public class FindReferenceResult : IJson
{
    public List<Location> locations = new List<Location>();
    public void ReadFrom(JsonNode node)
    {
        locations = node.ConvertTo<List<Location>>();
    }

    public JsonNode ToJsonNode()
    {
        return locations.ToJsonNode();
    }
}

public class RpcFindReference : JsonRpcBase<FindReferenceParams, FindReferenceResult>
{
    public override string m_method => "textDocument/references";

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
