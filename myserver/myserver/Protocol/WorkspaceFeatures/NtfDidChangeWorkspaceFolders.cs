using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class WorkspaceFoldersChangeEvent
{
    public WorkspaceFolder[] added { get; set; }
    public WorkspaceFolder[] removed { get; set; }
}

public class DidChangeWorkspaceFoldersParams
{
    [JsonPropertyName("event")]
    public WorkspaceFoldersChangeEvent Event { get; set; }
}

public class NtfDidChangeWorkspaceFolders : JsonNtfBase<DidChangeWorkspaceFoldersParams>
{
    public override string m_method => "workspace/didChangeWorkspaceFolders";

    public override void OnNotify()
    {
        // todo
    }
}
