using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol
{
    public class NtfCancelRequest : JsonNotifyBase<MyId>
    {
        public override string m_method => "$/cancelRequest";

        public override void OnNotify()
        {
            JsonRpcMgr.Instance.CancelRequest(m_args!);
        }
    }

}
