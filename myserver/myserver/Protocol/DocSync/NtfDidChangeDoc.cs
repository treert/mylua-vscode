
using MyServer.Misc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyServer.Protocol;


public class TextDocContentChangeEvent
{
    /// <summary>
    /// The new text for the provided range.
    /// </summary>
    public string text { get; set; }
    /// <summary>
    /// The range of the document that changed.<br/>
    /// 如果是空的，代表整个文件。
    /// </summary>
    public Range? range { get; set; }
}

public class DidChangeDocParams
{
    /// <summary>
    /// The document that did change.The version number points
    /// to the version after all provided content changes have
    /// been applied.
    /// </summary>
    public VersionedTextDocId textDocument { get; set; }
    /// <summary>
    /// The actual content changes.The content changes describe single state
    /// changes to the document.So if there are two content changes c1 (at
    /// array index 0) and c2(at array index 1) for a document in state S then
    /// c1 moves the document from S to S' and c2 from S' to S''. So c1 is
    /// computed on the state S and c2 is computed on the state S'.<br/>
    /// <br/>
    /// To mirror the content of a document using change events use the following approach:<br/>
    /// - start with the same initial content<br/>
    /// - apply the 'textDocument/didChange' notifications in the order you receive them.<br/>
    /// - apply the `TextDocumentContentChangeEvent`s in a single notification in the order you receive them.<br/>
    /// </summary>
    public List<TextDocContentChangeEvent> contentChanges { get; set; }
}

[MyProto(Direction = ProtoDirection.ToServer, IsReadOnly = false)]
public class NtfDidChangeDoc : JsonNtfBase<DidChangeDocParams>
{
    public override string m_method => "textDocument/didChange";

    public override void OnNotify()
    {
        // todo
    }
}

