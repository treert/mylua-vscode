using MyServer.Protocol.BaseStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

/// <summary>
/// How a completion was triggered
/// </summary>
public enum CompletionTriggerKind
{
    /// <summary>
    /// Completion was triggered by typing an identifier (24x7 code
    /// complete), manual invocation (e.g Ctrl+Space) or via API.
    /// </summary>
    Invoked = 1,
    /// <summary>
    /// Completion was triggered by a trigger character specified by
    /// the `triggerCharacters` properties of the `CompletionRegistrationOptions`.
    /// </summary>
    TriggerCharacter = 2,
    /// <summary>
    /// Completion was re-triggered as the current completion list is incomplete.
    /// </summary>
    TriggerForIncompleteCompletions = 3,
}

public class CompletionContext
{
    public CompletionTriggerKind triggerKind {  get; set; }
    /// <summary>
    /// The trigger character (a single character) that has trigger code complete.
    /// Is undefined if `triggerKind !== CompletionTriggerKind.TriggerCharacter`
    /// </summary>
    public string? triggerCharacter { get; set; }
}

public class CompletionParams
{
    public TextDocId textDocument { get; set; }
    public Position position { get; set; }
    /// <summary>
    /// This is only available if the client specifies to send this using
    /// the client capability `completion.contextSupport === true`
    /// </summary>
    public CompletionContext? context { get; set; }
}

public class CompletionList
{
    /// <summary>
    /// This list is not complete. Further typing should result in recomputing this list.
    /// <br/>
    /// Recomputed lists have all their items replaced (not appended) in the incomplete completion sessions.
    /// </summary>
    public bool isIncomplete { get; set; }


}

/// <summary>
/// Defines whether the insert text in a completion item should be interpreted as
/// plain text or a snippet.
/// </summary>
public enum InsertTextFormat
{
    /// <summary>
    /// The primary text to be inserted is treated as a plain string.
    /// </summary>
    PlainText = 1,
    /// <summary>
    /// The primary text to be inserted is treated as a snippet.
    /// <br/>
    /// A snippet can define tab stops and placeholders with `$1`, `$2`
    /// and `${3:foo}`. `$0` defines the final tab stop, it defaults to
    /// the end of the snippet.Placeholders with equal identifiers are linked,
    /// that is typing in one will update others too.
    /// </summary>
    Snippet = 2,
}

public enum CompletionItemTag
{
    Deprecated = 1,
}

/// <summary>
/// todo
/// </summary>
public class InsertReplaceEdit
{
    public string newText { get; set; }

    public Range? range { get; set; }

    public Range? insert {  get; set; }
    public Range? replace { get; set; }
}

public enum InsertTextMode
{
    AsIs = 1,
    AdjustIndentation = 2,
}

public class CompletionItemLabelDetails
{
    public string? detail { get; set; }
    public string? description { get; set; }
}

public enum CompletionItemKind
{
    Text = 1,
    Method = 2,
    Function = 3,
    Constructor = 4,
    Field = 5,
    Variable = 6,
    Class = 7,
    Interface = 8,
    Module = 9,
    Property = 10,
    Unit = 11,
    Value = 12,
    Enum = 13,
    Keyword = 14,
    Snippet = 15,
    Color = 16,
    File = 17,
    Reference = 18,
    Folder = 19,
    EnumMember = 20,
    Constant = 21,
    Struct = 22,
    Event = 23,
    Operator = 24,
    TypeParameter = 25,
}

public class CompletionItem
{
    public string label { get; set; }
    public CompletionItemLabelDetails? labelDetails { get; set; }
    public CompletionItemKind? kind { get; set; }
    public CompletionItemTag[]? tags { get; set; }
    public string? detail { get; set; }
    public MarkupContent? documentation { get; set; }
    public bool? preselect { get; set; }
    public string? sortText { get; set; }
    public string? filterText { get; set; }
    public string? insertText { get; set; }
    public InsertTextFormat? insertTextFormat { get; set; }
    public InsertTextMode? insertTextMode { get; set; }
    /// <summary>
    /// todo
    /// </summary>
    public InsertReplaceEdit? textEdit { get; set; }
    public string? textEditText { get; set; }
    public TextEdit[]? additionalTextEdits { get; set; }
    public string[]? commitCharacters { get; set; }
    public Command? command { get; set; }
    public JsonNode? data { get; set; }
}

[MyProto(Direction = ProtoDirection.ToServer)]
public class RpcCompletion : JsonRpcBase<CompletionParams, List<CompletionItem>>
{
    public override string m_method => "textDocument/completion";

    public override void OnRequest()
    {
        throw new NotImplementedException();
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}

[MyProto(Direction = ProtoDirection.ToServer)]
public class RpcCompletionItemResolve : JsonRpcBase<CompletionItem, CompletionItem>
{
    public override string m_method => "completionItem/resolve";

    public override void OnRequest()
    {
        throw new NotImplementedException();
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}

