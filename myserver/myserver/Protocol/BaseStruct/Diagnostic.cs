using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

/// <summary>
/// Represents a diagnostic, such as a compiler error or warning.
/// Diagnostic objects are only valid in the scope of a resource.
/// </summary>
public class Diagnostic
{
    public Range range { get; set; }
    /// <summary>
    /// If omitted it is up to the client to interpret diagnostics as error, warning, info or hint.
    /// </summary>
    public DiagnosticSeverity? severity { get; set; }
    /// <summary>
    /// The diagnostic's code, which might appear in the user interface.
    /// </summary>
    public int? code { get; set; }
    /// <summary>
    /// An optional property to describe the error code.
    /// </summary>
    public CodeDescription? codeDescription { get; set; }
    /// <summary>
    /// A human-readable string describing the source of this diagnostic.
    /// e.g. 'typescript' or 'super lint'.
    /// </summary>
    public string? source { get; set; }
    public string message { get; set; }
    public DiagnosticTag[]? tags { get; set; }
    /// <summary>
    /// An array of related diagnostic information, e.g. when symbol-names within
    /// a scope collide all definitions can be marked via this property.
    /// </summary>
    public DiagnosticRelatedInformation[]? relatedInformation { get; set; }
    /// <summary>
    /// A data entry field that is preserved between a
    /// `textDocument/publishDiagnostics` notification and
    /// `textDocument/codeAction` request.
    /// </summary>
    public JsonNode? data { get; set; }
}

public enum DiagnosticSeverity
{
    Error = 1,
    Warning = 2,
    Information = 3,
    Hint = 4,
}

public enum DiagnosticTag
{
    /// <summary>
    /// Unused or unnecessary code.
    /// <br/>
    /// Clients are allowed to render diagnostics with this tag faded out
    /// instead of having an error squiggle.
    /// </summary>
    Unnecessary = 1,
    /// <summary>
    /// Deprecated or obsolete code.
    /// <br/>
    /// Clients are allowed to rendered diagnostics with this tag strike through.
    /// </summary>
    Deprecated = 2,
}

/// <summary>
/// Represents a related message and source code location for a diagnostic.
/// This should be used to point to code locations that cause or are related to
/// a diagnostics, e.g when duplicating a symbol in a scope.
/// </summary>
public class DiagnosticRelatedInformation
{
    public Location location { get; set; }
    public string message { get; set; }
}

/// <summary>
/// Structure to capture a description for an error code.
/// </summary>
public class CodeDescription
{
    /// <summary>
    /// An URI to open with more information about the diagnostic error.
    /// </summary>
    public Uri href { get; set; }
}

