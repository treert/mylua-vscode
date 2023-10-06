using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
/// <summary>
/// example:<br/>
/// { language: 'typescript', scheme: 'file' }<br/>
/// { language: 'json', pattern: '**/package.json' }<br/>
/// </summary>
public class DocumentFilter
{
    public string? language { get; set; }
    /// <summary>
    /// A Uri [scheme](#Uri.scheme), like `file` or `untitled`.
    /// </summary>
    public string? scheme { get; set; }
    /// <summary>
    /// A glob pattern, like `*.{ts,js
    /// <br/>
    /// Glob patterns can have the following syntax:<br/>
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
    public string? pattern { get; set; }
}
