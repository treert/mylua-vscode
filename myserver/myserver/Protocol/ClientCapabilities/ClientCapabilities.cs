using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class ClientCapabilities
{
    /**
     * Workspace specific client capabilities.
     */
    public WorkspaceClientCapabilities? workspace { get; set; }

    /// <summary>
    /// Text document specific client capabilities.
    /// </summary>
    public TextDocumentClientCapabilities? TextDocument { get; set; }

    /// <summary>
    /// Window specific client capabilities.
    /// </summary>
    public WindowClientCapabilities? Window { get; set; }

    /// <summary>
    /// General client capabilities.
    /// </summary>
    public GeneralClientCapabilities? General { get; set; }

    /// <summary>
    /// Experimental client capabilities.
    /// </summary>
    public IDictionary<string, JToken> Experimental { get; set; } = new Dictionary<string, JToken>();
}

/**
 * Text document specific client capabilities.
 */
public class TextDocumentClientCapabilities {
    /**
     * Defines which synchronization capabilities the client supports.
     */
    public TextDocumentSyncClientCapabilities? synchronization?: ;
    /**
     * Capabilities specific to the `textDocument/completion` request.
     */
    public CompletionClientCapabilities? completion?: ;
    /**
     * Capabilities specific to the `textDocument/hover` request.
     */
    public HoverClientCapabilities? hover?: ;
    /**
     * Capabilities specific to the `textDocument/signatureHelp` request.
     */
    public SignatureHelpClientCapabilities? signatureHelp?: ;
    /**
     * Capabilities specific to the `textDocument/declaration` request.
     *
     * @since 3.14.0
     */
    public DeclarationClientCapabilities? declaration?: ;
    /**
     * Capabilities specific to the `textDocument/definition` request.
     */
    public DefinitionClientCapabilities? definition?: ;
    /**
     * Capabilities specific to the `textDocument/typeDefinition` request.
     *
     * @since 3.6.0
     */
    public TypeDefinitionClientCapabilities? typeDefinition?: ;
    /**
     * Capabilities specific to the `textDocument/implementation` request.
     *
     * @since 3.6.0
     */
    public ImplementationClientCapabilities? implementation?: ;
    /**
     * Capabilities specific to the `textDocument/references` request.
     */
    public ReferenceClientCapabilities? references { get; set;}
    /**
     * Capabilities specific to the `textDocument/documentHighlight` request.
     */
    public DocumentHighlightClientCapabilities? documentHighlight { get; set;}
    /**
     * Capabilities specific to the `textDocument/documentSymbol` request.
     */
    public DocumentSymbolClientCapabilities? documentSymbol { get; set;}
    /**
     * Capabilities specific to the `textDocument/codeAction` request.
     */
    public CodeActionClientCapabilities? codeAction { get; set;}
    /**
     * Capabilities specific to the `textDocument/codeLens` request.
     */
    public CodeLensClientCapabilities? codeLens { get; set;}
    /**
     * Capabilities specific to the `textDocument/documentLink` request.
     */
    public DocumentLinkClientCapabilities? documentLink { get; set;}
    /**
     * Capabilities specific to the `textDocument/documentColor` and the
     * `textDocument/colorPresentation` request.
     *
     * @since 3.6.0
     */
    public DocumentColorClientCapabilities? colorProvider { get; set;}
    /**
     * Capabilities specific to the `textDocument/formatting` request.
     */
    public DocumentFormattingClientCapabilities? formatting { get; set;}
    /**
     * Capabilities specific to the `textDocument/rangeFormatting` request.
     */
    public DocumentRangeFormattingClientCapabilities? rangeFormatting { get; set;}
    /**
     * Capabilities specific to the `textDocument/onTypeFormatting` request.
     */
    public DocumentOnTypeFormattingClientCapabilities? onTypeFormatting { get; set; }
    /**
     * Capabilities specific to the `textDocument/rename` request.
     */
    public RenameClientCapabilities? rename { get; set; }
    /**
     * Capabilities specific to the `textDocument/foldingRange` request.
     *
     * @since 3.10.0
     */
    public FoldingRangeClientCapabilities? foldingRange { get; set; }
    /**
     * Capabilities specific to the `textDocument/selectionRange` request.
     *
     * @since 3.15.0
     */
    public SelectionRangeClientCapabilities? selectionRange { get; set; }
    /**
     * Capabilities specific to the `textDocument/publishDiagnostics` notification.
     */
    public PublishDiagnosticsClientCapabilities? publishDiagnostics { get; set; }
    /**
     * Capabilities specific to the various call hierarchy requests.
     *
     * @since 3.16.0
     */
    public CallHierarchyClientCapabilities? callHierarchy { get; set; }
    /**
     * Capabilities specific to the various semantic token request.
     *
     * @since 3.16.0
    
    semanticTokens?: SemanticTokensClientCapabilities;
    /**
     * Capabilities specific to the `textDocument/linkedEditingRange` request.
     *
     * @since 3.16.0
     */
    public LinkedEditingRangeClientCapabilities? linkedEditingRange { get; set; }
    /**
     * Client capabilities specific to the `textDocument/moniker` request.
     *
     * @since 3.16.0
     */
    public MonikerClientCapabilities? moniker { get; set; }
    /**
     * Capabilities specific to the various type hierarchy requests.
     *
     * @since 3.17.0
     */
    public TypeHierarchyClientCapabilities? typeHierarchy { get; set; }
    /**
     * Capabilities specific to the `textDocument/inlineValue` request.
     *
     * @since 3.17.0
     */
    public InlineValueClientCapabilities? inlineValue { get; set; }
    /**
     * Capabilities specific to the `textDocument/inlayHint` request.
     *
     * @since 3.17.0
     */
    public InlayHintClientCapabilities? inlayHint { get; set; }
    /**
     * Capabilities specific to the diagnostic pull model.
     *
     * @since 3.17.0
     */
    public DiagnosticClientCapabilities? diagnostic { get; set; }
}

/**
 * Workspace specific client capabilities.
 */
public class WorkspaceClientCapabilities
{
    /**
     * The client supports applying batch edits
     * to the workspace by supporting the request
     * 'workspace/applyEdit'
     */
    public bool? applyEdit { get; set; }

    /**
     * Capabilities specific to `WorkspaceEdit`s
     */
    public WorkspaceEditClientCapabilities? workspaceEdit { get; set; }

    /**
     * Capabilities specific to the `workspace/didChangeConfiguration`
     * notification.
     */
    public DidChangeConfigurationClientCapabilities? didChangeConfiguration { get; set; }

    /**
     * Capabilities specific to the `workspace/didChangeWatchedFiles`
     * notification.
     */
    public DidChangeWatchedFilesClientCapabilities? didChangeWatchedFiles { get; set; }

    /**
     * Capabilities specific to the `workspace/symbol` request.
     */
    public WorkspaceSymbolClientCapabilities? symbol { get; set; }

    /**
     * Capabilities specific to the `workspace/executeCommand` request.
     */
    public ExecuteCommandClientCapabilities? executeCommand { get; set; }

    /**
     * The client has support for workspace folders.
     *
     * @since 3.6.0
     */
    public bool? workspaceFolders { get; set; }

    /**
     * The client supports `workspace/configuration` requests.
     *
     * @since 3.6.0
     */
    public bool? configuration { get; set; }

    /**
     * Capabilities specific to the semantic token requests scoped to the
     * workspace.
     *
     * @since 3.16.0
     */
    public SemanticTokensWorkspaceClientCapabilities? semanticTokens { get; set; }

    /**
     * Capabilities specific to the code lens requests scoped to the
     * workspace.
     *
     * @since 3.16.0
     */
    public CodeLensWorkspaceClientCapabilities? codeLens { get; set; }


    /**
     * The client has support for file requests/notifications.
     *
     * @since 3.16.0
     */
    public FileOperationClientCapabilities? fileOperations { get; set; }

    /**
     * Client workspace capabilities specific to inline values.
     *
     * @since 3.17.0
     */
    public InlineValueWorkspaceClientCapabilities? inlineValue { get; set; }

    /**
     * Client workspace capabilities specific to inlay hints.
     *
     * @since 3.17.0
     */
    public InlayHintWorkspaceClientCapabilities? inlayHint { get; set; }

    /**
     * Client workspace capabilities specific to diagnostics.
     *
     * @since 3.17.0.
     */
    public DiagnosticWorkspaceClientCapabilities? diagnostics { get; set; }
}

/**
 * Capabilities relating to events from file operations by the user in the client.
 *
 * These events do not come from the file system, they come from user operations
 * like renaming a file in the UI.
 *
 * @since 3.16.0
 */
public class FileOperationClientCapabilities
{
    /**
         * Whether the client supports dynamic registration for file
         * requests/notifications.
         */
    public bool? dynamicRegistration { get; set; }

    /**
         * The client has support for sending didCreateFiles notifications.
         */
    public bool? didCreate { get; set; }

    /**
         * The client has support for sending willCreateFiles requests.
         */
    public bool? willCreate { get; set; }

    /**
         * The client has support for sending didRenameFiles notifications.
         */
    public bool? didRename { get; set; }

    /**
         * The client has support for sending willRenameFiles requests.
         */
    public bool? willRename { get; set; }

    /**
         * The client has support for sending didDeleteFiles notifications.
         */
    public bool? didDelete { get; set; }

    /**
         * The client has support for sending willDeleteFiles requests.
         */
    public bool? willDelete { get; set; }
}