using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyServer.Protocol;


public class TextDocContentChangeEvent
{
    public string text { get; set; }
    public Range? range { get; set; }
}

public class DidChangeDocParams : IJson
{
    public VersionedTextDocId doc_vid;
    public List<TextDocContentChangeEvent> changes;

    public void ReadFrom(JsonNode node)
    {
        JsonNode node_doc_id = node["textDocument"]!;
        doc_vid = node_doc_id.ConvertTo<VersionedTextDocId>()!;

        JsonArray arr = node["contentChanges"]!.AsArray();
        changes = arr!.ConvertTo<List<TextDocContentChangeEvent>>();
    }

    public JsonNode ToJsonNode()
    {
        throw new NotImplementedException();
    }
}

public class NtfDidChangeDoc : JsonNtfBase<DidChangeDocParams>
{
    public override string m_method => "textDocument/didChange";

    public override void OnNotify()
    {
        // todo
    }
}

