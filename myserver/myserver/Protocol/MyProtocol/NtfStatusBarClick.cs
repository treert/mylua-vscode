using MyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

[MyProto(Direction = ProtoDirection.ToServer)]
public class NtfStatusBarClick : JsonNtfBase
{
    public override string m_method => "$/status/click";

    public override void OnNotify()
    {
        StatusBarMgr.Instance.OnClick();
    }
}
