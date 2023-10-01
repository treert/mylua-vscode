using MyServer.JsonRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public enum TextDocSaveReason
{
    /// <summary>
    /// Manually triggered, e.g. by the user pressing save, by starting
    /// debugging, or by an API call.
    /// </summary>
    Manual = 1,
    /// <summary>
    /// Automatic after a delay.
    /// </summary>
    AfterDelay = 2,
    /// <summary>
    /// When the editor lost focus.
    /// </summary>
    FocusOut = 3,
}

public class WillSaveTextDocParams : IJson
{
    public TextDocId doc_id;
    public TextDocSaveReason reason;

    public void ReadFrom(JsonNode node)
    {
        JsonNode node_doc_id = node["textDocument"]!;
        doc_id = JsonSerializer.Deserialize<TextDocId>(node_doc_id)!;
        JsonArray arr = node["contentChanges"]!.AsArray();

        int reason_int = node["reason"]!.GetValue<int>();
        reason =(TextDocSaveReason)(reason_int);
    }

    public JsonNode ToJsonNode()
    {
        throw new NotImplementedException();
    }
}

public class NtfWillSaveDoc : JsonNtfBase<WillSaveTextDocParams>
{
    public override string m_method => "textDocument/willSave";

    public override void OnNotify()
    {
        // todo
    }
}
