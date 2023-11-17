using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;


[MyProto(Direction = ProtoDirection.ToClient)]
public class NtfLogMessage : JsonNtfBase<ShowMessageParams>
{
    public override string m_method => "window/logMessage";

    public override void OnNotify()
    {

    }
}
