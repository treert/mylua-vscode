using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

[MyProto(Direction = ProtoDirection.ToServer, IsReadOnly = false)]
public class NtfDidCreateFiles : JsonNtfBase<CreateFilesParams>
{
    public override string m_method => "workspace/didCreateFiles";

    public override void OnNotify()
    {
        // todo
    }
}
