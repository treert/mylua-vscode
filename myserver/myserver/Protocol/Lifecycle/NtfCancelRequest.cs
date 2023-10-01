using MyServer.JsonRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol
{
    public class CancelParams : IJson
    {
        public MyId id;
        public void ReadFrom(JsonNode node)
        {
            id = node["id"]!;
        }

        public JsonNode ToJsonNode()
        {
            JsonObject data = new JsonObject();
            data.Add("id", id.ToJsonNode());
            return data;
        }
    }
    public class NtfCancelRequest : JsonNtfBase<CancelParams>
    {
        public override string m_method => "$/cancelRequest";

        public override void OnNotify()
        {
            JsonRpcMgr.Instance.CancelRequest(m_args.id);
        }
    }

}
