using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            public bool? didCreate { get; set; }
            public bool? willCreate { get; set; }
            public bool? didRename { get; set; }
            public bool? willRename { get; set; }
            public bool? didDelete { get; set; }
            public bool? willDelete { get; set;}
        }
        /// <summary>
        /// The client has support for file requests/notifications.
        /// </summary>
        public _FileOperations? fileOperations { get; set; }

    }
}

