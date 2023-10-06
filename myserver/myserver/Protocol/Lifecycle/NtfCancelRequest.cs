
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol
{
    public class CancelParams
    {
        public MyId id { get; set; }
    }
    public class NtfCancelRequest : JsonNtfBase<CancelParams>
    {
        public override string m_method => "$/cancelRequest";

        public override void OnNotify()
        {
            JsonRpcMgr.Instance.CancelRequest(Args.id);
        }
    }

}
