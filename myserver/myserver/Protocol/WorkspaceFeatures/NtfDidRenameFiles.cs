using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

[MyProto(Direction = ProtoDirection.ToServer)]
public class NtfDidRenameFiles : JsonNtfBase<RenameFilesParams>
{
    public override string m_method => "workspace/didRenameFiles";

    public override void OnNotify()
    {
        // todo
    }
}
