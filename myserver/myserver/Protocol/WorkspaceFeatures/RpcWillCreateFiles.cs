
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class FileCreate
{
    /// <summary>
    /// A file:// URI for the location of the file/folder being created.
    /// </summary>
    public string uri { get; set; }
}

public class CreateFilesParams
{
    public FileCreate[] files { get; set; }
}

[MyProto(Direction = ProtoDirection.ToServer)]
public class RpcWillCreateFiles : JsonRpcBase<CreateFilesParams, WorkspaceEdit>
{
    public override string m_method => "workspace/willCreateFiles";

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

