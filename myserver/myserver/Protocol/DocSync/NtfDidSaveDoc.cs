
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class DidSaveTextDocumentParams
{
    public TextDocId textDocument { get; set; }
    /// <summary>
    /// Optional the content when saved. Depends on the includeText value when the save notification was requested.
    /// </summary>
    public string? text { get; set; }
}

public class NtfDidSaveDoc : JsonNtfBase<DidSaveTextDocumentParams>
{
    public override string m_method => "textDocument/didSave";

    public override void OnNotify()
    {
        // 
    }
}
