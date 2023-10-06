using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class RpcGotoTypeDefinition : JsonRpcBase<PosAndTokenParams, GotoDeclareResult>
{
    public override string m_method => "textDocument/typeDefinition";

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
