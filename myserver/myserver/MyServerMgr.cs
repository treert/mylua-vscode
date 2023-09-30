using MyServer.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        public void Init(InitRpc rpc)
        {
            //m_status = Status.Inited;
            rpc.m_err = new ResponseError();
            rpc.m_err.code = ErrorCodes.InternalError;
            rpc.m_err.message = "debug init error";
            rpc.SendResponse();
        }

        public LspTrace LspTrace { get; set; } = LspTrace.Off;

        public void ShutDown()
        {
            m_status = Status.ShutDowned;
        }

        public void ExitApp()
        {
            if(m_status == Status.ShutDowned)
            {
                Environment.Exit(0);
            }
            else
            {
                Environment.Exit(1);
            }
        }
    }
}
