using NLog.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Drawing;

namespace MyServer.Protocol.BaseStruct;
/// <summary>
/// todo@later
/// </summary>
public class WorkspaceEdit
{
    public Dictionary<Uri, TextEdit[]>? changes { get; set; }
    /// <summary>
    /// Depending on the client capability
    /// `workspace.workspaceEdit.resourceOperations` document changes are either
    /// an array of `TextDocumentEdit`s to express changes to n different text
    /// documents where each text document edit addresses a specific version of
    /// a text document.Or it can contain above `TextDocumentEdit`s mixed with
    /// create, rename and delete file / folder operations.
    /// <br/>
    /// Whether a client supports versioned document edits is expressed via
    /// `workspace.workspaceEdit.documentChanges` client capability.
    /// <br/>
    /// If a client neither supports `documentChanges` nor
    /// `workspace.workspaceEdit.resourceOperations` then only plain `TextEdit`s
    /// using the `changes` property are supported.
    /// </summary>
    public TextDocEdit[]? documentChanges { get; set; }
    /// <summary>
    /// A map of change annotations that can be referenced in `AnnotatedTextEdit`s
    /// or create, rename and delete file / folder operations.
    /// <br/>
    /// Whether clients honor this property depends on the client capability `workspace.changeAnnotationSupport`.
    /// </summary>
    public Dictionary<string, ChangeAnnotation> changeAnnotations { get; set; }
}
