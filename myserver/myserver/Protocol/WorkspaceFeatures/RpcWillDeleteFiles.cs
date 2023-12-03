using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class FileDelete
{
    public string uri { get; set; }
}

public class DeleteFilesParams
{
    public FileDelete[] files { get; set; }
}

[MyProto(Direction = ProtoDirection.ToServer)]
public class RpcWillDeleteFiles : JsonRpcBase<DeleteFilesParams, WorkspaceEdit>
{
    public override string m_method => "workspace/willDeleteFiles";

    public override void OnRequest()
    {
        // do nothing
        m_res = null;
        SendResponse();
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}

