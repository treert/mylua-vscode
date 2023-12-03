using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class DidChangeConfigurationParams
{
    /// <summary>
    /// The actual changed settings
    /// </summary>
    public JsonNode setting { get; set; }
}

[MyProto(Direction = ProtoDirection.ToServer)]
public class NtfDidChangeConfiguration : JsonNtfBase<DidChangeConfigurationParams>
{
    public override string m_method => "workspace/didChangeConfiguration";

    public override void OnNotify()
    {
        // todo
    }
}
