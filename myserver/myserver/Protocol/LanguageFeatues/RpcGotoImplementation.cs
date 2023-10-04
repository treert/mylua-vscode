using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol.LanguageFeatues;

public class RpcGotoImplementation : JsonRpcBase<PosAndTokenParams, GotoDeclareResult>
{
    public override string m_method => "textDocument/implementation";

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
