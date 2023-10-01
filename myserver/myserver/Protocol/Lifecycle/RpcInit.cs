using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol
{


    public class WorkspaceFolder : IJson
    {
        public Uri uri;
        public string name;

        public void ReadFrom(JsonNode node)
        {
            var str = node["uri"]!.GetValue<string>();
            uri = new Uri(str);
            name = node["name"]!.GetValue<string>();
        }

        public JsonNode ToJsonNode()
        {
            JsonObject data = new JsonObject();
            data.Add("name", name);
            data.Add("uri", uri.ToString());
            return data;
        }
    }

    public class InitArgs : IJson
    {
        public WorkDoneProgressParams progress = new();

        /// <summary>
        /// 客户端进程，如果有值，需要检查下：如果客户端进程已经跪了，需要退出lsp
        /// </summary>
        public int? processId;
        /// <summary>
        /// 客户端名字和版本。myserver do not care
        /// </summary>
        public (string name,string? version)? clientInfo;
        /// <summary>
        /// Uses IETF language tags as the value's syntax
        /// (See https://en.wikipedia.org/wiki/IETF_language_tag)
        /// myserver do not care
        /// </summary>
        public string? locale;
        /// <summary>
        /// 客户端自定义传过来的参数。开关功能用。
        /// </summary>
        public JsonNode? initializationOptions;

        public LspTrace trace = LspTrace.Off;

        public List<WorkspaceFolder> work_dirs = new List<WorkspaceFolder>();

        public void ReadFrom(JsonNode node)
        {
            progress.ReadFrom(node);
            processId = node["processId"]?.GetValue<int>();
            var clientInfo_node = node["clientInfo"];
            if (clientInfo_node != null)
            {
                string name = clientInfo_node["name"]!.GetValue<string>();
                string? version = clientInfo_node["version"]?.GetValue<string>();
                clientInfo = (name,version);
            }
            locale = node["locale"]?.GetValue<string>();
            initializationOptions = node["initializationOptions"];
            var node_trace = node["trace"]?.GetValue<string>();
            if (node_trace!= null)
            {
                trace = node_trace.StrToTrace();
            }
            JsonArray? node_dirs = node["workspaceFolders"]?.AsArray();
            work_dirs.Clear();
            if (node_dirs != null)
            {
                foreach(var node_dir in node_dirs)
                {
                    var dir = new WorkspaceFolder();
                    dir.ReadFrom(node_dir!);
                    work_dirs.Add(dir);
                }
            }

            // todo 能力字段后面回头搞下。
        }

        public JsonNode ToJsonNode()
        {
            throw new NotImplementedException();
        }
    }

    public class InitResult : IJson
    {
        public (string name, string? version)? serverInfo;

        public void ReadFrom(JsonNode node)
        {
            throw new NotImplementedException();
        }

        public JsonNode ToJsonNode()
        {
            JsonObject data = new JsonObject();
            if (serverInfo != null)
            {
                JsonObject tt = new JsonObject();
                tt.Add("name", serverInfo.Value.name);
                if (serverInfo.Value.version != null)
                {
                    tt.Add("version", serverInfo.Value.version);
                }
                data.Add("serverInfo", tt);
            }
            // todo server 能力
            return data;
        }
    }

    public class InitRpc : JsonRpcBase<InitArgs, InitResult>
    {
        public override string m_method => MyConst.Method.Init;

        public override void OnRequest()
        {
            MyServerMgr.Instance.StartInit(this);
        }

        public override void OnCanceled()
        {
            throw new NotImplementedException();
        }

        protected override void OnSuccess()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 客户端受到 rpc init 的结果后发这个 ntf, 通知服务器初始化完成
    /// </summary>
    public class NtfInited : JsonNtfBase<EmptyObject>
    {
        public override string m_method => "$initialized";

        public override void OnNotify()
        {
            MyServerMgr.Instance.NtfInited();
        }
    }
}
