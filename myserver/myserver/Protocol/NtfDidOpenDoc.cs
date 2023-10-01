using MyServer.JsonRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class DidOpenDocParams : IJson
{
    public TextDocItem textDoc;
    public void ReadFrom(JsonNode node)
    {
        textDoc = JsonSerializer.Deserialize<TextDocItem>(node)!;
    }

    public JsonNode ToJsonNode()
    {
        JsonNode? obj = JsonSerializer.SerializeToNode(textDoc);
        return obj!;
    }
}

public class NtfDidOpenDoc : JsonNtfBase<DidOpenDocParams>
{
    public override string m_method => "textDocument/didOpen";

    public override void OnNotify()
    {
        // todo
    }
}
