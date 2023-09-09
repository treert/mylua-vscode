using MyServer.JsonRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol
{
    public abstract class JsonNotifyBase
    {
        public abstract string m_method { get; }
        public abstract void OnNotify(JsonNode? args);
        public abstract void SendNotify(JsonNode? args);
    }

    public abstract class JsonRpcBase
    {
        public abstract string m_method { get; }
        public MyId m_id;
        public JsonNode? m_args;
        /// <summary>
        /// 收到请求，最终需要
        /// </summary>
        /// <param name="args"></param>
        public abstract void OnRequest(JsonNode? args);
        /// <summary>
        /// 收到返回
        /// </summary>
        /// <param name="args"></param>
        public abstract void OnResponse(JsonNode? args);
        /// <summary>
        /// 收到返回，但是出错了。
        /// </summary>
        /// <param name="error"></param>
        public abstract void OnError(ResponseError error);

        /// <summary>
        /// 发出请求
        /// </summary>
        /// <param name="args"></param>
        public abstract void SendRequest(JsonNode? args);
        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="args"></param>
        public abstract void SendResponse(JsonNode? args);
        /// <summary>
        /// 返回报错
        /// </summary>
        /// <param name="error"></param>
        public abstract void SendError(ResponseError error);
    }

    public abstract class JsonRpcBase<TReq, TRes>: JsonRpcBase
        where TReq: IJson,new() where TRes: IJson, new()
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
