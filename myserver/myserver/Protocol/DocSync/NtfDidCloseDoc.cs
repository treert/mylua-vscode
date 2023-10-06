
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class DidCloseDocParams
{
    public TextDocId textDocument { get; set; }
}

/// <summary>
/// 官方文档有些奇怪的描述：
/// Receiving a close notification doesn’t mean that the document was open in an editor before.
/// A close notification requires a previous open notification to be sent.
/// </summary>
public class NtfDidCloseDoc : JsonNtfBase<DidCloseDocParams>
{
    public override string m_method => "textDocument/didClose";

    public override void OnNotify()
    {
        // todo
    }
}
