using MyServer.JsonRpc;
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
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Range? range { get; set; }
}

public class DidChangeDocParams : IJson
{
    public VersionedTextDocId doc_vid;
    public List<TextDocContentChangeEvent> changes;

    public void ReadFrom(JsonNode node)
    {
        JsonNode node_doc_id = node["textDocument"]!;
        doc_vid = JsonSerializer.Deserialize<VersionedTextDocId>(node_doc_id)!;
        JsonArray arr = node["contentChanges"]!.AsArray();

        changes = new List<TextDocContentChangeEvent>();
        foreach (JsonNode? item in arr)
        {
            var change = JsonSerializer.Deserialize<TextDocContentChangeEvent>(item)!;
            changes.Add(change);
        }
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

