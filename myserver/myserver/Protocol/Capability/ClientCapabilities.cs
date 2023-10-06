using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class ClientCapabilities
{
    public TextDocumentSyncClientCapabilities? synchronization {  get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/completion` request.
    /// </summary>
    public CompletionClientCapabilities? completion { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/hover` request.
    /// </summary>
    public HoverClientCapabilities? hover {  get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/signatureHelp` request.
    /// </summary>
    public SignatureHelpClientCapabilities? signatureHelp { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/declaration` request.
    /// <br/>
    /// @since 3.14.0
    /// </summary>
    public DeclarationClientCapabilities? declaration { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/definition` request.
    /// </summary>
    public DefinitionClientCapabilities? definition { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/typeDefinition` request.
    /// <br/>
    /// @since 3.6.0
    /// </summary>
    public TypeDefinitionClientCapabilities? typeDefinition { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/implementation` request.
    /// <br/>
    /// @since 3.6.0
    /// </summary>
    public ImplementationClientCapabilities? implementation { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/references` request.
    /// </summary>
    public ReferenceClientCapabilities? references { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/documentHighlight` request.
    /// </summary>
    public DocumentHighlightClientCapabilities? documentHighlight { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/documentSymbol` request.
    /// </summary>
    public DocumentSymbolClientCapabilities? documentSymbol { get; set; }
}
