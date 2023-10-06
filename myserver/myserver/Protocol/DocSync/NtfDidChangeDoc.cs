
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyServer.Protocol;


public class TextDocContentChangeEvent
{
    public string text { get; set; }
    public Range? range { get; set; }
}

public class DidChangeDocParams
{
    public VersionedTextDocId textDocument { get; set; }
    public List<TextDocContentChangeEvent> contentChanges { get; set; }
}

public class NtfDidChangeDoc : JsonNtfBase<DidChangeDocParams>
{
    public override string m_method => "textDocument/didChange";

    public override void OnNotify()
    {
        // todo
    }
}

