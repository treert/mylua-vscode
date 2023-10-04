using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol.LanguageFeatues;

public class DocumentLinkParams : IWorkDoneProgress, IPartialResult, IJson
{
    public ProgressToken? partialResultToken { get; set; }
    public ProgressToken? workDoneToken { get; set; }
    public TextDocId textDocument { get; set; }

    public virtual void ReadFrom(JsonNode node)
    {
        textDocument = node["textDocument"]!.ConvertTo<TextDocId>();
        partialResultToken = node["partialResultToken"];
        workDoneToken = node["workDoneToken"];
    }

    public virtual JsonNode ToJsonNode()
    {
        JsonObject data = new JsonObject();
        data.AddKeyValue("textDocument", textDocument);
        data.TryAddKeyValue("partialResultToken", partialResultToken);
        data.TryAddKeyValue("workDoneToken", workDoneToken);
        return data;
    }
}

public class DocumentLink
{
    public Range range { get; set; }
    public Uri? target { get; set; }
    public string? tooltip { get; set; }
    public JsonNode? data { get; set; }
}

public class DocumentLinkResult : IJson
{
    public List<DocumentLink> links = new List<DocumentLink>();
    public void ReadFrom(JsonNode node)
    {
        links = node.ConvertTo<List<DocumentLink>>();
    }

    public JsonNode ToJsonNode()
    {
        return links.ToJsonNode();
    }
}

public class RpcDocLink : JsonRpcBase<DocumentLinkParams, DocumentLinkResult>
{
    public override string m_method => "textDocument/documentLink";

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
