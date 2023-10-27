using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static MyServer.Protocol.SemanticTokensClientCapabilities;

namespace MyServer.Protocol;
public class CompletionOptions: WorkDoneProgressOptions
{
    /// <summary>
    /// The additional characters, beyond the defaults provided by the client(typically
    /// [a - zA - Z]), that should automatically trigger a completion request.For example
    /// `.` in JavaScript represents the beginning of an object property or method and is
    /// thus a good candidate for triggering a completion request.
    /// <br/>
    /// Most tools trigger a completion request automatically without explicitly
    /// requesting it using a keyboard shortcut(e.g.Ctrl+Space). Typically they
    /// do so when the user starts to type an identifier.For example if the user
    /// types `c` in a JavaScript file code complete will automatically pop up
    /// present `console` besides others as a completion item.Characters that
    /// make up identifiers don't need to be listed here.
    /// </summary>
    public string[]? triggerCharacters { get; set; }
    /// <summary>
    /// The list of all possible characters that commit a completion.This field
    /// can be used if clients don't support individual commit characters per
    /// completion item. See client capability
    /// `completion.completionItem.commitCharactersSupport`.
    /// <br/>
    /// If a server provides both `allCommitCharacters` and commit characters on
    /// an individual completion item the ones on the completion item win.
    /// </summary>
    public string[]? allCommitCharacters { get; set; }
    /// <summary>
    /// The server provides support to resolve additional information for a completion item.
    /// </summary>
    public bool? resolveProvider { get; set; }
    public class _CompletionItem
    {
        /// <summary>
        /// The server has support for completion item label details(see also `CompletionItemLabelDetails`)
        /// when receiving a completion item in a resolve call.
        /// </summary>
        public bool? labelDetailsSupport { get; set; }
    }
    /// <summary>
    /// The server supports the following `CompletionItem` specific capabilities.
    /// </summary>
    public _CompletionItem? completionItem { get; set; }
}
