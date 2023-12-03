using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class ExecuteCommandParams
{
    public string command { get; set; }
    public JsonNode[]? arguments { get; set; }
}

[MyProto(Direction = ProtoDirection.ToServer)]
public class RpcExecuteCommand : JsonRpcBase<ExecuteCommandParams, object>
{
    public override string m_method => "workspace/executeCommand";

    public override void OnRequest()
    {
        // todo
        m_res = new{ tips = $"do command {ReqArgs.command}"};
        SendResponse();
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}
