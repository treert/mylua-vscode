using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static NLog.LayoutRenderers.Wrappers.ReplaceLayoutRendererWrapper;
using static System.Net.Mime.MediaTypeNames;

namespace MyServer.Protocol;

public class CompletionItemCapabilities
{
    /// <summary>
    /// Client supports snippets as insert text.
    /// <br/>
    /// A snippet can define tab stops and placeholders with `$1`, `$2`
    /// and `${ 3:foo}`. `$0` defines the final tab stop, it defaults to
    /// the end of the snippet.Placeholders with equal identifiers are
    /// linked, that is typing in one will update others too.
    /// </summary>
    public bool? snippetSupport { get; set; }
    /// <summary>
    /// Client supports commit characters on a completion item.
    /// </summary>
    public bool? commitCharactersSupport { get; set; }
    /// <summary>
    /// Client supports the follow content formats for the documentation
    /// property. The order describes the preferred format of the client.
    /// </summary>
    public MarkupKind[]? documentationFormat { get; set; }
    /// <summary>
    /// Client supports the deprecated property on a completion item.
    /// </summary>
    public bool? deprecatedSupport { get; set; }
    /// <summary>
    /// Client supports the preselect property on a completion item.
    /// </summary>
    public bool? preselectSupport { get; set; }
    public class TagSupport
    {
        /// <summary>
        /// The tags supported by the client.
        /// </summary>
        public CompletionItemTag[] valueSet { get; set; }
    }
    /// <summary>
    /// Client supports the tag property on a completion item.
    /// Clients supporting tags have to handle unknown tags gracefully.
    /// Clients especially need to preserve unknown tags when sending a completion
    /// item back to the server in a resolve call.
    /// <br/>
    /// @since 3.15.0
    /// </summary>
    public TagSupport? tagSupport { get; set; }
    /// <summary>
    /// Client supports insert replace edit to control different behavior if
    /// a completion item is inserted in the text or should replace text.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public bool? insertReplaceSupport { get; set; }
    public class ResolveSupport
    {
        /// <summary>
        /// The properties that a client can resolve lazily.
        /// </summary>
        public string[] properties { get; set; }
    }
    /// <summary>
    /// Indicates which properties a client can resolve lazily on a
    /// completion item.Before version 3.16.0 only the predefined properties
    /// `documentation` and `detail` could be resolved lazily.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public ResolveSupport? resolveSupport { get; set;}
    public class InsertTextModeSupport
    {
        public InsertTextMode[] valueSet { get; set; }
    }
    /// <summary>
    /// The client supports the `insertTextMode` property on
    /// a completion item to override the whitespace handling mode
    /// as defined by the client(see `insertTextMode`).
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public InsertTextModeSupport? insertTextModeSupport { get; set; }
    /// <summary>
    /// The client has support for completion item label
    /// details(see also `CompletionItemLabelDetails`).
    /// <br/>
    /// @since 3.17.0
    /// </summary>
    public bool? labelDetailsSupport { get; set; }
}

public class CompletionClientCapabilities
{
    /// <summary>
    /// Whether completion supports dynamic registration.
    /// </summary>
    public bool? dynamicRegistration { get; set; }
    /// <summary>
    /// The client supports the following `CompletionItem` specific capabilities.
    /// </summary>
    public CompletionItemCapabilities? completionItem { get; set; }
    public class _CompletionItemKind
    {
        /// <summary>
        /// The completion item kind values the client supports.When this
        /// property exists the client also guarantees that it will
        /// handle values outside its set gracefully and falls back
        /// to a default value when unknown.
        /// <br/>
        /// If this property is not present the client only supports
        /// the completion items kinds from `Text` to `Reference` as defined in
        /// the initial version of the protocol.
        /// </summary>
        public CompletionItemKind[] valueSet { get; set; }
    }
    public _CompletionItemKind? completionItemKind { get; set; }
    /// <summary>
    /// The client supports to send additional context information for a
    /// `textDocument/completion` request.
    /// </summary>
    public bool? contextSupport { get; set; }
    /// <summary>
    /// The client's default when the completion item doesn't provide a
    /// `insertTextMode` property.
    /// <br/>
    /// @since 3.17.0
    /// </summary>
    public InsertTextMode? insertTextMode { get; set; }
    public class _CompletionList
    {
        /// <summary>
        /// The client supports the following itemDefaults on
        /// a completion list.
        /// <br/>
        /// The value lists the supported property names of the
        /// `CompletionList.itemDefaults` object. If omitted
        /// no properties are supported.
        /// <br/>
        /// @since 3.17.0
        /// </summary>
        public string[]? itemDefaults { get; set; }
    }
}
