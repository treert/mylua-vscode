using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol.MyProtocol;
public class StatusBarReportParams
{
    public string text { get; set; }
    public string? tooltip { get; set; }
}

[MyProto(Direction = ProtoDirection.ToClient)]
public class NtfStatusBarReport : JsonNtfBase<StatusBarReportParams>
{
    public override string m_method => "$/status/report";

    public override void OnNotify()
    {
        throw new NotImplementedException();
    }
}
