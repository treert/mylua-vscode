using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

[MyProto(Direction = ProtoDirection.ToClient)]
public class NtfTelemetry : JsonNtfBase
{
    public override string m_method => "telemetry/event";

    public override void OnNotify()
    {

    }
}