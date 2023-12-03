using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

[MyProto(Direction = ProtoDirection.ToClient)]
public class RpcWorkspaceFolders : JsonRpcBase<Dummy, List<WorkspaceFolder>>
{
    public override string m_method => "workspace/workspaceFolders";

    public override void OnRequest()
    {
        throw new NotImplementedException();
    }

    protected override void OnSuccess()
    {
        // todo
    }
}
