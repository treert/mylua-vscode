using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class CommandParams
{
    public string command { get; set; }
    public JsonArray args { get; set; } = [];
}

[MyProto(Direction = ProtoDirection.ToClient)]
public class NtfCommand : JsonNtfBase<CommandParams>
{
    public override string m_method => "$/command";

    public override void OnNotify()
    {
        throw new NotImplementedException();
    }

    public static void SendCommond(string command, params object[] args)
    {
        var ntf = new NtfCommand();
        ntf.Args.command = command;
        for (int i = 0; i < args.Length; i++)
        {
            ntf.Args.args.Add(args[i]?.ToJsonNode());
        }
        ntf.SendNotify();
    }
}
