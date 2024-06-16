
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyServer.Protocol
{


    public class WorkspaceFolder
    {
        public Uri uri { get; set; }
        public string name { get; set; }
    }

    public class ClientInfo
    {
        /// <summary>
        /// The name of the client as defined by the client.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// The client's version as defined by the client.
        /// </summary>
        public string? version { get; set; }
    }

    public class ServerInfo
    {
        /// <summary>
        /// The name of the server as defined by the server.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// The client's version as defined by the server.
        /// </summary>
        public string? version { get; set; }
    }

    public class InitArgs
    {
        /// <summary>
        /// 客户端进程，如果有值，需要检查下：如果客户端进程已经跪了，需要退出lsp
        /// </summary>
        public int? processId { get; set; }
        /// <summary>
        /// Information about the client.
        /// myserver do not care
        /// </summary>
        public ClientInfo? clientInfo { get; set; }
        /// <summary>
        /// Uses IETF language tags as the value's syntax
        /// (See https://en.wikipedia.org/wiki/IETF_language_tag)
        /// myserver do not care
        /// </summary>
        public string? locale { get; set; }
        /// <summary>
        /// 客户端自定义传过来的参数。开关功能用。
        /// </summary>
        public JsonNode? initializationOptions { get; set; }
        /// <summary>
        /// The capabilities provided by the client (editor or tool)
        /// </summary>
        public ClientCapabilities capabilities { get; set; }

        public LspTrace? trace { get; set; }

        public List<WorkspaceFolder> workspaceFolders { get; set; } = [];
    }

    public class InitResult
    {
        /// <summary>
        /// The capabilities the language server provides.
        /// </summary>
        public ServerCapabilities capabilities { get; set; }
        /// <summary>
        /// Information about the server.
        /// </summary>
        public ServerInfo serverInfo { get; set; }
    }

    [MyProto(Direction = ProtoDirection.ToServer)]
    public class InitRpc : JsonRpcBase<InitArgs, InitResult>
    {
        public override string m_method => MyConst.Method.Init;

        public override void OnRequest()
        {
            MyServerMgr.Instance.StartInit(this);
        }

        protected override void OnSuccess()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 客户端受到 rpc init 的结果后发这个 ntf, 通知服务器初始化完成
    /// </summary>
    [MyProto(Direction = ProtoDirection.ToServer)]
    public class NtfInited : JsonNtfBase<Dummy>
    {
        public override string m_method => "initialized";

        public override void OnNotify()
        {
            MyServerMgr.Instance.NtfInited();
        }
    }
}
