using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyServer.Protocol;

/// <summary>
/// A pattern kind describing if a glob pattern matches a file a folder or both.
/// </summary>
[JsonConverter(typeof(MyJsonEnumConverter))]
public enum FileOperationPatternKind
{
    /// <summary>
    /// The pattern matches a file only.
    /// </summary>
    File,
    /// <summary>
    /// The pattern matches a folder only.
    /// </summary>
    Folder,
}

/// <summary>
/// Matching options for the file operation pattern.
/// </summary>
public class FileOperationPatternOptions
{
    /// <summary>
    /// The pattern should be matched ignoring casing.
    /// </summary>
    public bool? ignoreCase {  get; set; }
}

/// <summary>
/// A pattern to describe in which file operation requests or notifications the server is interested in.
/// </summary>
public class FileOperationPattern
{
    /// <summary>
    /// The glob pattern to match.Glob patterns can have the following syntax:<br/>
    /// - `*` to match one or more characters in a path segment<br/>
    /// - `?` to match on one character in a path segment<br/>
    /// - `**` to match any number of path segments, including none<br/>
    /// - `{}` to group sub patterns into an OR expression. (e.g. `**​/*.{ts,js}`
    ///   matches all TypeScript and JavaScript files)<br/>
    /// - `[]` to declare a range of characters to match in a path segment
    ///   (e.g., `example.[0-9]` to match on `example.0`, `example.1`, …)<br/>
    /// - `[!...]` to negate a range of characters to match in a path segment
    ///   (e.g., `example.[!0-9]` to match on `example.a`, `example.b`, but
    ///   not `example.0`)
    /// </summary>
    public string glob { get; set; }
    /// <summary>
    /// Whether to match files or folders with this pattern.
    /// <br/>
    /// Matches both if undefined.
    /// </summary>
    public FileOperationPatternKind? matches { get; set; }
    /// <summary>
    /// Additional options used during matching.
    /// </summary>
    public FileOperationPatternOptions? options { get; set; }
}

/// <summary>
/// A filter to describe in which file operation requests or notifications the server is interested in.
/// </summary>
public class FileOperationFilter
{
    /// <summary>
    /// A Uri like `file` or `untitled`.
    /// </summary>
    public string? scheme { get; set; }
    /// <summary>
    /// The actual file operation pattern.
    /// </summary>
    public FileOperationPattern pattern { get; set; }
}

/// <summary>
/// The options to register for file operations.
/// </summary>
public class FileOperationRegistrationOptions
{
    /// <summary>
    /// The actual filters.
    /// </summary>
    public FileOperationFilter[] filters { get; set; }
}
