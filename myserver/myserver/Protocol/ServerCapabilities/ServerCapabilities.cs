using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
/// <summary>
/// 服务器提供的权限。这儿的结构定义可以变通来着
/// </summary>
public class ServerCapabilities
{
    /// <summary>
    /// The position encoding the server picked from the encodings offered
    /// by the client via the client capability `general.positionEncodings`.
    /// <br/>
    /// If the client didn't provide any position encodings the only valid
    /// value that a server can return is 'utf-16'.
    /// <br/>
    /// If omitted it defaults to 'utf-16'.
    /// <br/>
    /// @since 3.17.0
    /// </summary>
    public PositionEncodingKind positionEncoding { get; set; } = PositionEncodingKind.UTF16;
    /// <summary>
    /// Defines how text documents are synced.
    /// </summary>
    public TextDocumentSyncOptions textDocumentSync { get; set; } = new TextDocumentSyncOptions();
    /// <summary>
    /// Defines how notebook documents are synced.
    /// </summary>
    public NotebookDocumentSyncOptions? notebookDocumentSync { get; set; }
    /// <summary>
    /// The server provides completion support.
    /// </summary>
    public CompletionOptions? completionProvider { get; set; }
    /// <summary>
    /// The server provides hover support.
    /// </summary>
    public HoverOptions? hoverProvider { get; set; }
    public 

}
