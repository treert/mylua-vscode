using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol.MyProtocol;

[MyProto(Direction = ProtoDirection.ToClient)]
public class NtfStatusBarShow : JsonNtfBase
{
    public override string m_method => "$/status/show";

    public override void OnNotify()
    {
        throw new NotImplementedException();
    }
}

[MyProto(Direction = ProtoDirection.ToClient)]
public class NtfStatusBarHide : JsonNtfBase
{
    public override string m_method => "$/status/hide";

    public override void OnNotify()
    {
        throw new NotImplementedException();
    }
}


