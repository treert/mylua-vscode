
using MyServer.Misc;
using MyServer.Protocol.BaseStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol.LanguageFeatues;

[MyProto(Direction = ProtoDirection.ToClient)]
public class RpcInlayHintRefresh : JsonRpcBase<Dummy, Dummy>
{
    public override string m_method => "workspace/inlayHint/refresh";

    public override void OnCanceled()
    {
        throw new NotImplementedException();
    }

    public override void OnRequest()
    {
        throw new NotImplementedException();
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}
