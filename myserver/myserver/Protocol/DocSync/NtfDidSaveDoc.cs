
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
    /// Optional the content when saved. Depends on the includeText value when the save notification was requested.<br/>
    /// 理论上不需要这个，也许可以用来校验下内存里的数据时对的。【mylua-lsp选择不关心这个】
    /// </summary>
    public string? text { get; set; }
}

/// <summary>
/// 通知文本被保存了。内存里的数据理论上是没有变化的。所以也是只读的。
/// </summary>
[MyProto(Direction = ProtoDirection.ToServer)]
public class NtfDidSaveDoc : JsonNtfBase<DidSaveTextDocumentParams>
{
    public override string m_method => "textDocument/didSave";

    public override void OnNotify()
    {
        // do nothing
    }
}
