using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class DidSaveTextDocumentParams : IJson
{
    public TextDocId doc_id;
    public string? text;

    public void ReadFrom(JsonNode node)
    {
        doc_id = node["textDocument"]!.ConvertTo<TextDocId>();
        text = (string?)node["text"];
    }

    public JsonNode ToJsonNode()
    {
        throw new NotImplementedException();
    }
}

public class NtfDidSaveDoc : JsonNtfBase<DidSaveTextDocumentParams>
{
    public override string m_method => "textDocument/didSave";

    public override void OnNotify()
    {
        // todo
    }
}
