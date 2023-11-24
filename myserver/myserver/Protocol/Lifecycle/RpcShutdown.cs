
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;


/// <summary>
/// 通知服务器关闭。rpc shutdown 返回后。客户端再使用 ntf 通知服务器退出。
/// </summary>
public class RpcShutdown : JsonRpcBase<Dummy, Dummy>
{
    public override string m_method => "shutdown";

    public override void OnRequest()
    {
        MyServerMgr.Instance.ShutDown(this);
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}

public class NtfExit : JsonNtfBase
{
    public override string m_method => "exit";

    public override void OnNotify()
    {
        MyServerMgr.Instance.ExitApp();
    }
}
