using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyServer.Protocol;

/// <summary>
/// Describe options to be used when registering for file system change events.
/// 这个能力只能动态注册
/// </summary>
public class DidChangeWatchedFilesRegistrationOptions
{
    public FileSystemWatcher[] watchers { get; set; }
}

/// <summary>
/// A relative pattern is a helper to construct glob patterns that are matched
/// relatively to a base URI.The common value for a `baseUri` is a workspace
/// folder root, but it can be another absolute URI as well.
/// </summary>
public class RelativePattern
{
    public string baseUri { get; set; }
    /// <summary>
    /// The glob pattern to watch relative to the base path.Glob patterns can have the following syntax:<br/>
    /// - `*` to match one or more characters in a path segment<br/>
    /// - `?` to match on one character in a path segment<br/>
    /// - `**` to match any number of path segments, including none<br/>
    /// - `{}` to group conditions(e.g. `**​/*.{ts,js}` matches all TypeScript and JavaScript files)<br/>
    /// - `[]` to declare a range of characters to match in a path segment
    ///   (e.g., `example.[0-9]` to match on `example.0`, `example.1`, …)<br/>
    /// - `[!...]` to negate a range of characters to match in a path segment
    ///   (e.g., `example.[!0-9]` to match on `example.a`, `example.b`, but not `example.0`)
    /// </summary>
    public string pattern { get; set; }
}

[Flags]
public enum WatchKind
{
    Created = 1,
    Changed = 2,
    Deleted = 4,
}

public class FileSystemWatcher
{
    /// <summary>
    /// If omitted it defaults to WatchKind.Create | WatchKind.Change | WatchKind.Delete which is 7.
    /// </summary>
    public WatchKind? kind { get; set; }
    public RelativePattern globPattern { get; set; }
}


public enum FileChangeType
{
    None = 0,
    Created = 1,
    Changed = 2,
    Deleted = 3,
}

public class FileEvent
{
    public FileChangeType type { get; set; }
    public Uri uri { get; set; }
}

public class DidChangeWatchedFilesParams
{
    public FileEvent[] changes { get; set; }
}

public class NtfDidChangeWatchedFiles : JsonNtfBase<DidChangeWatchedFilesParams>
{
    public override string m_method => "workspace/didChangeWatchedFiles";

    public static void RegisterCapability()
    {

    }

    public override void OnNotify()
    {
        // todo
    }
}
