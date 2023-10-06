using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyServer.Protocol;
/// <summary>
/// TextEdit | AnnotatedTextEdit
/// </summary>
public class TextEdit
{
    /// <summary>
    /// To insert text into a document create a range where start === end.
    /// </summary>
    public Range range { get; set; }

    /// <summary>
    /// 替换的文本。delete 操作使用空串。
    /// </summary>
    public string newText { get; set; }

    /// <summary>
    /// A special text edit with an additional change annotation.<br/>
    /// @since 3.16.0. This is guarded by the client capability `workspace.workspaceEdit.changeAnnotationSupport`
    /// </summary>
    public string? annotationId { get; set; }
}

public class TextDocEdit
{
    public OptionalVersionedTextDocId textDocument { get; set; }
    public TextEdit[] edits { get; set; }
}

/// <summary>
/// Additional information that describes document changes.
/// </summary>
public class ChangeAnnotation
{
    /// <summary>
    /// A human-readable string describing the actual change.
    /// The string is rendered prominent in the user interface.
    /// </summary>
    public string label { get; set; }
    /// <summary>
    /// A flag which indicates that user confirmation is needed before applying the change.
    /// </summary>
    public bool? needsConfirmation { get; set; }
    /// <summary>
    /// A human-readable string which is rendered less prominent in the user interface.
    /// </summary>
    public string? description { get; set; }
}
