using MyServer.JsonRpc;
using MyServer.Misc;
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
        textDoc = node.ConvertTo<TextDocItem>()!;
    }

    public JsonNode ToJsonNode()
    {
        return textDoc.ToJsonNode()!;
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
