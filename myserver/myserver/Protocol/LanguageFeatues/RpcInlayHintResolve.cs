using MyServer.Protocol.BaseStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

[MyProto(Direction = ProtoDirection.ToServer)]
public class RpcInlayHintResolve : JsonRpcBase<InlayHint, InlayHint>
{
    public override string m_method => "inlayHint/resolve";

    public override void OnRequest()
    {
        throw new NotImplementedException();
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}
