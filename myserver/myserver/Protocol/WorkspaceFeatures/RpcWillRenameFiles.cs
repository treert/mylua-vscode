using NLog.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class FileRename
{
    public string oldUri { get; set; }
    public string newUri { get; set; }
}

public class RenameFilesParams
{
    /// <summary>
    /// An array of all files/folders renamed in this operation. When a folder
    /// is renamed, only the folder will be included, and not its children.
    /// </summary>
    public FileRename[] files { get; set; }
}

[MyProto(Direction = ProtoDirection.ToServer)]
public class RpcWillRenameFiles : JsonRpcBase<RenameFilesParams, WorkspaceEdit>
{
    public override string m_method => "workspace/willRenameFiles";

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
