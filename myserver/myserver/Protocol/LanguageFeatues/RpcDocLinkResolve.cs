using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol.LanguageFeatues;

public class DocumentLinkParamsAndResult : IJson
{
    public DocumentLink link = new DocumentLink();
    public void ReadFrom(JsonNode node)
    {
        link = node.ConvertTo<DocumentLink>();
    }

    public JsonNode ToJsonNode()
    {
        return link.ToJsonNode();
    }
}

public class RpcDocLinkResolve : JsonRpcBase<DocumentLinkParamsAndResult, DocumentLinkParamsAndResult>
{
    public override string m_method => "documentLink/resolve";

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

