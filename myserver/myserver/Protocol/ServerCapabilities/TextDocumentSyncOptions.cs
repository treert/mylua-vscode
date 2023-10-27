using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static MyServer.Protocol.SemanticTokensClientCapabilities;

namespace MyServer.Protocol;

public enum TextDocumentSyncKind
{
    /// <summary>
    /// Documents should not be synced at all.
    /// </summary>
    None = 0,
    /// <summary>
    /// Documents are synced by always sending the full content of the document.
    /// </summary>
    Full = 1,
    /// <summary>
    /// Documents are synced by sending the full content on open.
    /// After that only incremental updates to the document are sent.
    /// </summary>
    Incremental = 2,
}

public class SaveOptions
{
    /// <summary>
    /// The client is supposed to include the content on save.
    /// </summary>
    public bool includeText { get; set; } = true;
}

public class TextDocumentSyncOptions
{
    /// <summary>
    /// Open and close notifications are sent to the server. If omitted open close notification should not be sent.
    /// </summary>
    public bool openClose { get; set; } = true;
    /// <summary>
    /// Change notifications are sent to the server.If omitted it defaults to TextDocumentSyncKind.None.
    /// todo@xx 需要选择一种同步方式
    /// </summary>
    public TextDocumentSyncKind change { get; set; } = TextDocumentSyncKind.Incremental;
    /// <summary>
    /// If present will save notifications are sent to the server.
    /// If omitted the notification should not be sent.
    /// </summary>
    public bool willSave { get; set; } = true;
    /// <summary>
    /// If present will save wait until requests are sent to the server.
    /// If omitted the request should not be sent.
    /// </summary>
    public bool willSaveWaitUntil { get; set; } = true;
    /// <summary>
    /// If present save notifications are sent to the server.
    /// If omitted the notification should not be sent.
    /// </summary>
    public SaveOptions save { get; set; } = new SaveOptions();
}
