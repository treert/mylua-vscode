using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

/// <summary>
/// An item to transfer a text document from the client to the server.
/// </summary>
public class TextDocItem
{
    public Uri uri { get; set; }
    public string languageId { get; set; }
    /// <summary>
    /// it will increase after each change, including undo/redo.
    /// </summary>
    public int version { get; set; }
    /// <summary>
    /// The content of the opened text document.
    /// </summary>
    public string text { get; set; }
}

public class TextDocId
{
    public Uri uri { get; set; }
}

public class VersionedTextDocId : TextDocId
{
    public int version { get; set; }
}

public class OptionalVersionedTextDocId : TextDocId
{
    public int? version { get; set; }
}

public class TextDocPosAndId
{
    public TextDocId textDocument { get; set; }
    public Position position { get; set; }
}
