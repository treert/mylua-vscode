
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
/// 关闭文件。这时有必要重新读取磁盘文件，因为用户可能放弃内存中的修改。<br/>
/// 官方文档有些奇怪的描述,前后矛盾：<br/>
/// Receiving a close notification doesn’t mean that the document was open in an editor before.<br/>
/// A close notification requires a previous open notification to be sent.<br/>
/// Note that a server’s ability to fulfill requests is independent of whether a text document is open or closed.
/// </summary>
[MyProto(Direction = ProtoDirection.ToServer, IsReadOnly = false)]
public class NtfDidCloseDoc : JsonNtfBase<DidCloseDocParams>
{
    public override string m_method => "textDocument/didClose";

    public override void OnNotify()
    {
        // todo
    }
}
