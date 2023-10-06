
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class PosAndTokenParams : TextDocPosition, IWorkDoneProgress, IPartialResult
{
    public ProgressToken? partialResultToken { get; set; }
    public ProgressToken? workDoneToken { get; set; }

}

// todo
public class GotoDeclareResult
{
}

[MyProto(Direction = ProtoDirection.ToServer)]
public class RpcGotoDeclare : JsonRpcBase<PosAndTokenParams, GotoDeclareResult>
{
    public override string m_method => "textDocument/declaration";

    public override void OnCanceled()
    {
        // todo
    }

    public override void OnRequest()
    {
        // todo
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}
