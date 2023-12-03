using myserver;
using MyServer.Misc;
using MyServer.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer
{
    /// <summary>
    /// MyLua Lsp 实例
    /// </summary>
    public class MyServerMgr
    {
        public static MyServerMgr Instance { get; private set; } = new MyServerMgr();

        /// <summary>
        /// 客户端下发的能力。服务器只读。
        /// </summary>
        public ClientCapabilities ClientCapabilities { get; set; }

        /// <summary>
        /// 服务器发送给客户端的能力。
        /// </summary>
        public ServerCapabilities ServerCapabilities { get; set; }

        public enum Status
        {
            UnInit,
            Inited,
            ShutDowned,
        }

        private Status m_status = Status.UnInit;

        public bool IsInited => m_status == Status.Inited;


        public void StartInit(InitRpc rpc)
        {
            if(rpc.ReqArgs.workspaceFolders.Count > 0) {
                var fold = rpc.ReqArgs.workspaceFolders[0];
                if (fold.uri.IsFile)
                {
                    var LocaPath = fold.uri.ToLocalFilePath();
                    var files = Directory.GetFiles(LocaPath);
                    NtfShowMessage.ShowMessage(files.ToJsonStr());
                }
            }

            // 构建服务器能力。全部用静态注册。
            ServerCapabilities = new ServerCapabilities()
            {
                textDocumentSync = new TextDocumentSyncOptions(),
                //documentSymbolProvider = new DocumentSymbolOptions(),
                //semanticTokensProvider = new SemanticTokensOptions(),
            };

            rpc.m_res = new InitResult()
            {
                serverInfo = new ServerInfo() { name = "mylua", version = "0.1.0"},
                capabilities = ServerCapabilities,
            };
            //rpc.m_err = new ResponseError();
            //rpc.m_err.code = ErrorCodes.InternalError;
            //var err_data = new JsonObject()
            //{
            //    { "retry",true },
            //};
            //rpc.m_err.data = err_data;
            //rpc.m_err.message = "debug init error";
            rpc.SendResponse();
        }

        public void NtfInited()
        {
            m_status = Status.Inited;
        }

        public LspTrace LspTrace { get; set; } = LspTrace.Off;

        public void ShutDown(RpcShutdown rpc)
        {
            m_status = Status.ShutDowned;
            rpc.SendResponse();
        }

        public void ExitApp()
        {
            MySession.Instance.Stop();
            m_status = Status.UnInit;
            JsonRpcMgr.Instance.Init();
            return;
            //if(m_status == Status.ShutDowned)
            //{
            //    Environment.Exit(0);
            //}
            //else
            //{
            //    Environment.Exit(1);
            //}
        }
    }
}
