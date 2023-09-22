using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol
{


    public class WorkspaceFolder
    {
        public Uri uri;
        public string name;
    }

    public class InitArgs : IJson
    {
        public int? processId;
        public (string name,string? version)? clientInfo;
        public string? locale;
        public JsonNode? initializationOptions;

        public void ReadFrom(JsonNode? node)
        {
            Debug.Assert(node != null);
            processId = node["processId"]?.GetValue<int>();
            var clientInfo_node = node["clientInfo"];
            if (clientInfo_node != null)
            {
                var name = clientInfo_node["name"]!.GetValue<string>();
                var version = clientInfo_node["version"]?.GetValue<string>();
                clientInfo = (name,version);
            }
            locale = node["locale"]?.GetValue<string>();
        }

        public JsonNode? ToJsonNode()
        {
            throw new NotImplementedException();
        }
    }

    public class InitResult : IJson
    {
        public (string name, string? version)? serverInfo;

        public void ReadFrom(JsonNode? node)
        {
            throw new NotImplementedException();
        }

        public JsonNode? ToJsonNode()
        {
            throw new NotImplementedException();
        }
    }

    public class InitRpc : JsonRpcBase<InitArgs, InitResult>
    {
        public override string m_method => MyConst.Method.Init;

        public override void OnRequest()
        {
            MyServerMgr.Instance.Init(this);
        }

        protected override void OnSuccess()
        {
            // only from client
            throw new NotImplementedException();
        }
    }
}
