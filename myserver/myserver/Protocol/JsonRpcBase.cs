using MyServer.JsonRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol
{
    public abstract class JsonRpcBase
    {
        public MyId m_id;
        public abstract void OnRequest(JsonNode? args);
        public abstract void OnResponse(JsonNode? args);
    }

    public abstract class JsonRpcBase<TReq, TRes>  : JsonRpcBase  where TReq: IJson,new() where TRes: IJson, new()
    {
        public TReq? m_req;
        public TRes? m_res;
        /// <summary>
        /// 收到请求。需要提供结果，并看情况响应 cancel
        /// </summary>
        public abstract void OnRequest();

        /// <summary>
        /// 请求收到返回。
        /// </summary>
        public abstract void OnResponse();

        public override void OnRequest(JsonNode? args)
        {
            if (args != null)
            {
                m_req = new TReq();
                m_req.ReadFrom(args);
            }
            OnRequest();
        }

        public override void OnResponse(JsonNode? args)
        {
            throw new NotImplementedException();
        }
    }
}
