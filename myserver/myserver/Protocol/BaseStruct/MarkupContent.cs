using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyServer.Protocol;

[JsonConverter(typeof(MyJsonEnumConverter))]
public enum MarkupKind
{
    PlainText,
    Markdown,
}

/// <summary><code>
/// A `MarkupContent` literal represents a string value which content is
/// interpreted base on its kind flag.Currently the protocol supports
/// `plaintext` and `markdown` as markup kinds.
/// 
/// If the kind is `markdown` then the value can contain fenced code blocks like
/// in GitHub issues.
/// 
/// Here is an example how such a string can be constructed using
/// JavaScript / TypeScript:
/// ```typescript
/// let markdown: MarkdownContent = {
/// kind: MarkupKind.Markdown,
/// value: [
/// 		'# Header',
/// 		'Some text',
/// 		'```typescript',
/// 		'someCode();',
/// 		'```'
/// 	].join('\n')
/// };
/// ``
/// 
/// Please Note* that clients might sanitize the return markdown. A client could
/// decide to remove HTML from the markdown to avoid script execution.
/// </code></summary>
public class MarkupContent
{
    public MarkupKind kind { get; set; }
    public string value { get; set; }
}
