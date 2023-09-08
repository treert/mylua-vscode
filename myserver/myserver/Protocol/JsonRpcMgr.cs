using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol
{
    internal class JsonRpcMgr
    {
        public void Request(JsonNode data)
        {
            MyId id = data["id"]!.ToMyId();
            string method = data["method"]!.GetValue<string>();
            JsonNode? args = data["params"];
            data["12"] = null;
        }

        public void Notify(JsonNode data)
        {

        }

        public void OnNotify(string message)
        {

        }

        public void OnRequest()
        {

        }
    }
}
