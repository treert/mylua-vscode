
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using MyServer.Misc;

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

public class WillSaveTextDocParams
{
    public TextDocId textDocument { get; set; }
    public TextDocSaveReason reason { get; set; }
}

/*
客户端保存文件前，发送给服务器。
发送的 DocId 应该处于 Open 状态
*/
public class NtfWillSaveDoc : JsonNtfBase<WillSaveTextDocParams>
{
    public override string m_method => "textDocument/willSave";

    public override void OnNotify()
    {
        // todo
    }
}
