using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static MyServer.Protocol.SemanticTokensClientCapabilities;
using System.Text.Json.Nodes;

namespace MyServer.Protocol;
public class ClientCapabilities
{
    public class _Workspace
    {
        /// <summary>
        /// The client supports applying batch edits to the workspace by supporting the request 'workspace/applyEdit'
        /// </summary>
        public bool? applyEdit { get; set; }
        /// <summary>
        /// Capabilities specific to `WorkspaceEdit`s
        /// </summary>
        public WorkspaceEditClientCapabilities? workspaceEdit { get; set; }
        /// <summary>
        /// Capabilities specific to the `workspace/didChangeConfiguration` notification.
        /// </summary>
        public DidChangeConfigurationClientCapabilities? didChangeConfiguration { get; set; }
        /// <summary>
        /// Capabilities specific to the `workspace/didChangeWatchedFiles` notification.
        /// </summary>
        public DidChangeWatchedFilesClientCapabilities? didChangeWatchedFiles { get; set; }
        /// <summary>
        /// Capabilities specific to the `workspace/symbol` notification.
        /// </summary>
        public WorkspaceSymbolClientCapabilities? symbol { get; set; }
        /// <summary>
        /// Capabilities specific to the `workspace/executeCommand` request.
        /// </summary>
        public ExecuteCommandClientCapabilities? executeCommand { get; set; }
        /// <summary>
        /// The client has support for workspace folders.
        /// </summary>
        public bool? workspaceFolders { get; set; }
        /// <summary>
        /// The client supports `workspace/configuration` requests.
        /// </summary>
        public bool? configuration { get; set; }
        /// <summary>
        /// Capabilities specific to the semantic token requests scoped to the workspace.
        /// </summary>
        public SemanticTokensWorkspaceClientCapabilities? semanticTokens { get; set; }
        /// <summary>
        /// Capabilities specific to the code lens requests scoped to the workspace.
        /// </summary>
        public CodeLensWorkspaceClientCapabilities? codeLens { get; set; }
        public class _FileOperations
        {
            public bool? dynamicRegistration { get; set; }
            public bool? didCreate { get; set; } = true;
            public bool? willCreate { get; set; } = true;
            public bool? didRename { get; set; } = true;
            public bool? willRename { get; set; } = true;
            public bool? didDelete { get; set; } = true;
            public bool? willDelete { get; set; } = true;
        }
        /// <summary>
        /// The client has support for file requests/notifications.
        /// </summary>
        public _FileOperations? fileOperations { get; set; }
        /// <summary>
        /// Client workspace capabilities specific to inline values.
        /// </summary>
        public InlineValueWorkspaceClientCapabilities? inlineValue { get; set; }
        /// <summary>
        /// Client workspace capabilities specific to inlay hints.
        /// </summary>
        public InlayHintWorkspaceClientCapabilities? inlayHint { get; set; }
        /// <summary>
        /// Client workspace capabilities specific to diagnostics.
        /// </summary>
        public DiagnosticWorkspaceClientCapabilities? diagnostic { get; set; }
    }
    /// <summary>
    /// Workspace specific client capabilities.
    /// </summary>
    public _Workspace? workspace { get; set; }
    /// <summary>
    /// Text document specific client capabilities.
    /// </summary>
    public TextDocumentClientCapabilities? textDocument { get; set; }
    /// <summary>
    /// Capabilities specific to the notebook document support.
    /// </summary>
    public NotebookDocumentClientCapabilities? notebookDocument { get; set; }
    public class _Window
    {
        /// <summary>
        /// It indicates whether the client supports server initiated
        /// progress using the `window/workDoneProgress/create` request.
        /// <br/>
        /// The capability also controls Whether client supports handling
        /// of progress notifications.If set servers are allowed to report a
        /// `workDoneProgress` property in the request specific server
        /// capabilities.
        /// </summary>
        public bool? workDoneProgress { get; set; }
        /// <summary>
        /// Capabilities specific to the showMessage request
        /// </summary>
        public ShowMessageRequestClientCapabilities? showMessage { get; set; }
        /// <summary>
        /// Client capabilities for the show document request.
        /// </summary>
        public ShowDocumentClientCapabilities? showDocument { get; set; }
    }
    /// <summary>
    /// Window specific client capabilities.
    /// </summary>
    public _Window? window { get; set; }
    public class _General
    {
        public class _StaleRequestSupport
        {
            /// <summary>
            /// The client will actively cancel the request.
            /// </summary>
            public bool cancel { get; set; }
            /// <summary>
            /// The list of requests for which the client will retry the request 
            /// if it receives a response with error code `ContentModified``
            /// </summary>
            public string[] retryOnContentModified { get; set; }
        }
        /// <summary>
        /// Client capability that signals how the client handles stale requests
        /// (e.g.a request for which the client will not process the response
        /// anymore since the information is outdated).
        /// <br/>
        /// @since 3.17.0
        /// </summary>
        public _StaleRequestSupport? staleRequestSupport { get; set; }
        /// <summary>
        /// Client capabilities specific to regular expressions.
        /// </summary>
        public RegularExpressionsClientCapabilities? regularExpressions { get; set; }
        /// <summary>
        /// Client capabilities specific to the client's markdown parser.
        /// </summary>
        public MarkdownClientCapabilities? markdown { get; set; }
        /// <summary>
        /// The position encodings supported by the client.Client and server
        /// have to agree on the same position encoding to ensure that offsets
        /// (e.g.character position in a line) are interpreted the same on both
        /// side.
        /// <br/>
        /// To keep the protocol backwards compatible the following applies: if
        /// the value 'utf-16' is missing from the array of position encodings
        /// servers can assume that the client supports UTF-16. UTF-16 is
        /// therefore a mandatory encoding.
        /// <br/>
        /// If omitted it defaults to['utf-16'].
        /// <br/>
        /// Implementation considerations: since the conversion from one encoding
        /// into another requires the content of the file / line the conversion
        /// is best done where the file is read which is usually on the server
        /// side.
        /// <br/>
        /// @since 3.17.0
        /// </summary>
        public PositionEncodingKind[]? positionEncodings { get; set; }
    }
    /// <summary>
    /// General client capabilities.
    /// </summary>
    public _General? general { get; set; }
    /// <summary>
    /// Experimental client capabilities.
    /// </summary>
    public JsonNode? experimental { get; set; }
}

