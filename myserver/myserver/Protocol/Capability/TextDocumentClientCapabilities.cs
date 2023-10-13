using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;


public class TextDocumentClientCapabilities
{
    public TextDocumentSyncClientCapabilities? synchronization { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/completion` request.
    /// </summary>
    public CompletionClientCapabilities? completion { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/hover` request.
    /// </summary>
    public HoverClientCapabilities? hover { get; set; }
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
    /// <summary>
    /// Capabilities specific to the `textDocument/codeAction` request.
    /// </summary>
    public CodeActionClientCapabilities? codeAction { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/codeLens` request.
    /// </summary>
    public CodeLensClientCapabilities? codeLens { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/documentLink` request.
    /// </summary>
    public DocumentLinkClientCapabilities? documentLink { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/documentColor` and the
    /// `textDocument/colorPresentation` request.
    /// <br/>
    /// @since 3.6.0
    /// </summary>
    public DocumentColorClientCapabilities? colorProvider { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/formatting` request.
    /// </summary>
    public DocumentFormattingClientCapabilities? formatting { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/rangeFormatting` request.
    /// </summary>
    public DocumentRangeFormattingClientCapabilities? rangeFormatting { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/onTypeFormatting` request.
    /// </summary>
    public DocumentOnTypeFormattingClientCapabilities? onTypeFormatting { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/rename` request.
    /// </summary>
    public RenameClientCapabilities? rename { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/publishDiagnostics` notification.
    /// </summary>
    public PublishDiagnosticsClientCapabilities? publishDiagnostics { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/foldingRange` request.
    /// <br/>
    /// @since 3.10.0
    /// </summary>
    public FoldingRangeClientCapabilities? foldingRange { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/selectionRange` request.
    /// <br/>
    /// @since 3.15.0
    /// </summary>
    public SelectionRangeClientCapabilities? selectionRange { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/linkedEditingRange` request.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public LinkedEditingRangeClientCapabilities? linkedEditingRange { get; set; }
    /// <summary>
    /// Capabilities specific to the various call hierarchy requests.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public CallHierarchyClientCapabilities? callHierarchy { get; set; }
    /// <summary>
    /// Capabilities specific to the various semantic token requests.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public SemanticTokensClientCapabilities? semanticTokens { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/moniker` request.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public MonikerClientCapabilities? moniker { get; set; }
    /// <summary>
    /// Capabilities specific to the various type hierarchy requests.
    /// <br/>
    /// @since 3.17.0
    /// </summary>
    public TypeHierarchyClientCapabilities? typeHierarchy { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/inlineValue` request.
    /// <br/>
    /// @since 3.17.0
    /// </summary>
    public InlineValueClientCapabilities? inlineValue { get; set; }
    /// <summary>
    /// Capabilities specific to the `textDocument/inlayHint` request.
    /// <br/>
    /// @since 3.17.0
    /// </summary>
    public InlayHintClientCapabilities? inlayHint { get; set; }

}
