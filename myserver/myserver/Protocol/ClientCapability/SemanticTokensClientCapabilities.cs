using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static MyServer.Protocol.SemanticTokensClientCapabilities;

namespace MyServer.Protocol;
public class SemanticTokensClientCapabilities
{
    /// <summary>
    /// Whether implementation supports dynamic registration.If this is set to
    /// `true` the client supports the new `(TextDocumentRegistrationOptions &
    /// StaticRegistrationOptions)` return value for the corresponding server
    /// capability as well.
    /// </summary>
    public bool? dynamicRegistration { get; set; }
    /// <summary>
    /// Example: "requests": { "range": true, "full": { "delta": true } }
    /// 和官方文本不一样，不支持奇观的类型或
    /// </summary>
    public class _Request
    {
        /// <summary>
        /// The client will send the `textDocument/semanticTokens/range` request if the server provides a corresponding handler.
        /// </summary>
        public bool? range { get; set; }
        public class _Full
        {
            /// <summary>
            /// The client will send the `textDocument/semanticTokens/full/delta` request if the server provides a corresponding handler.
            /// </summary>
            public bool delta { get; set; }
        }
        /// <summary>
        /// The client will send the `textDocument/semanticTokens/full` request if the server provides a corresponding handler.
        /// </summary>
        public _Full? full { get; set; }
    }
    /// <summary>
    /// Which requests the client supports and might send to the server
    /// depending on the server's capability. Please note that clients might not
    /// show semantic tokens or degrade some of the user experience if a range
    /// or full request is advertised by the client but not provided by the
    /// server.If for example the client capability `requests.full` and
    /// `request.range` are both set to true but the server only provides a
    /// range provider the client might not render a minimap correctly or might
    /// even decide to not show any semantic tokens at all.
    /// </summary>
    public _Request requests { get; set; }
    /// <summary>
    /// The token types that the client supports.
    /// </summary>
    public string[] tokenTypes { get; set; }
    /// <summary>
    /// The token modifiers that the client supports.
    /// </summary>
    public string[] tokenModifiers { get; set; }
    /// <summary>
    /// The formats the clients supports.
    /// </summary>
    public TokenFormat[] formats { get; set; }
    /// <summary>
    /// Whether the client supports tokens that can overlap each other.【vscode 1.84.2 还不支持】
    /// </summary>
    public bool overlappingTokenSupport { get; set; } = false;
    /// <summary>
    /// Whether the client supports tokens that can span multiple lines.【vscode 1.84.2 还不支持】
    /// </summary>
    public bool multilineTokenSupport { get; set; } = false;
    /// <summary>
    /// Whether the client allows the server to actively cancel a
    /// semantic token request, e.g.supports returning
    /// ErrorCodes.ServerCancelled.If a server does the client
    /// needs to retrigger the request.
    /// <br/>
    /// @since 3.17.0
    /// </summary>
    public bool? serverCancelSupport { get; set; }
    /// <summary>
    /// Whether the client uses semantic tokens to augment existing syntax tokens.
    /// If set to `true` client side created syntax tokens and semantic tokens are both used for colorization.
    /// If set to `false` the client only uses the returned semantic tokens for colorization.
    /// <br/>
    /// If the value is `undefined` then the client behavior is not specified.
    /// <br/>
    /// @since 3.17.0
    /// </summary>
    public bool? augmentsSyntaxTokens { get; set; }
}
