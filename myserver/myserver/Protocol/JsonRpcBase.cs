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
        public abstract void OnNotifyParseArgs(JsonNode? args);
        public abstract void OnNotify();
        public abstract void SendNotify();
    }

    /// <summary>
    /// RPC 的生命周期。
    /// Client->Server rpc
    ///     1. OnRequestParseArgs 解析完参数，排队等待处理。【为了方便实现，现在是一个一个的处理】
    ///     2. OnRequest 开始处理请求。
    ///     3. SendRespone 发送返回。
    /// Server->Client rpc
    ///     1. SendRequest 发送请求。【不排队，构建rpc结构，直接请求】
    ///     2. OnResponseParseArgs
    ///     3. OnResponse
    ///         - OnSuccess
    ///         - OnError
    /// </summary>
    public abstract class JsonRpcBase
    {
        public abstract string m_method { get; }
        public MyId m_id;
        public JsonNode? m_args;

        #region Client RPC
        /// <summary>
        /// 收到请求,
        /// </summary>
        /// <param name="args"></param>
        public abstract void OnRequestParseArgs(JsonNode? args);
        /// <summary>
        /// 开始处理请求。每个具体rpc自己实现
        /// </summary>
        public abstract void OnRequest();
        /// <summary>
        /// 返回结果。调用前，先设置好 m_res 或者 m_err
        /// </summary>
        /// <param name="args"></param>
        public abstract void SendResponse();
        #endregion

        #region Server RPC
        /// <summary>
        /// 发出请求
        /// </summary>
        /// <param name="args"></param>
        public abstract void SendRequest();
        /// <summary>
        /// 收到返回
        /// </summary>
        /// <param name="args"></param>
        public abstract void OnResponseParseArgs(JsonNode? args);
        /// <summary>
        /// 请求返回。默认实现分流调用 OnSuccess OnError
        /// </summary>
        public abstract void OnResponse();
        /// <summary>
        /// 请求返回成功。由具体的rpc实现
        /// </summary>
        public abstract void OnSuccess();
        /// <summary>
        /// 请求返回失败。默认实现在日志里输出点错误信息，具体的rpc也可以 override.
        /// </summary>
        public abstract void OnError();
        #endregion

    }

    public abstract class JsonRpcBase<TReq, TRes>: JsonRpcBase
        where TReq: IJson,new() where TRes: IJson, new()
    {
        public TReq? m_req;
        public TRes? m_res;
        public ResponseError? m_err;

        public override sealed void OnRequestParseArgs(JsonNode? args)
        {
            if (args != null)
            {
                m_req = new TReq();
                m_req.ReadFrom(args);
            }
        }

        public override sealed void SendResponse()
        {
            var data = new JsonObject();
            data["id"] = m_id.ToJsonNode();
            if (m_err != null)
            {
                data["error"] = m_err.ToJsonNode();
            }
            else
            {
                data["result"] = m_res?.ToJsonNode();
            }
        }

        public override sealed void SendRequest()
        {
            
        }
    }
}
