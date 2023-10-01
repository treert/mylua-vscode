using myserver;
using MyServer.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer
{
    public class MyServerMgr
    {
        public static MyServerMgr Instance { get; private set; } = new MyServerMgr();

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
            rpc.m_err = new ResponseError();
            rpc.m_err.code = ErrorCodes.InternalError;
            var err_data = new JsonObject()
            {
                { "retry",true },
            };
            rpc.m_err.data = err_data;
            rpc.m_err.message = "debug init error";
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
