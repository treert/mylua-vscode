using MyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

[MyProto(Direction = ProtoDirection.ToServer)]
public class NtfStatusBarRefresh : JsonNtfBase
{
    public override string m_method => "$/status/refresh";

    public override void OnNotify()
    {
        StatusBarMgr.Instance.Show();
    }
}
