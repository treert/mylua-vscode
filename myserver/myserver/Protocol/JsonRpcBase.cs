using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
/*
RPC 的生命周期。
Client->Server rpc
    1. OnRequestParseArgs 解析完参数，排队等待处理。【为了方便实现，现在是一个一个的处理】
    2. OnRequest 开始处理请求。
        - OnCanceled 取消请求。当已经开始请求时需要处理下
    3. SendRespone 发送返回。
Server->Client rpc
    1. SendRequest 发送请求。【不排队，构建rpc结构，直接请求】
        - SendCancel
    2. OnResponseParseArgs
    3. OnResponse
        - OnSuccess
        - OnError
*/

namespace MyServer.Protocol
{
    public abstract class JsonNotifyBase
    {
        public abstract string m_method { get; }
        public virtual void OnNotifyParseArgs(JsonNode? args) { }
        /// <summary>
        /// 子类需要实现
        /// </summary>
        public abstract void OnNotify();
        public virtual void SendNotify()
        {
            JsonObject data = new();
            data["method"] = m_method;
            JsonRpcMgr.Instance.SendData(data);
        }
    }

    public abstract class JsonNotifyBase<TArgs>: JsonNotifyBase where TArgs:IJson,new()
    {
        public TArgs? m_args;

        public override sealed void OnNotifyParseArgs(JsonNode? args)
        {
            if(args is not null)
            {
                m_args = new TArgs();
                m_args.ReadFrom(args);
            }
        }

        public override sealed void SendNotify()
        {
            JsonObject data = new();
            data["method"] = m_method;
            if(m_args is not null)
            {
                data["params"] = m_args.ToJsonNode();
            }
            JsonRpcMgr.Instance.SendData(data);
        }
    }

    public abstract class JsonRpcBase
    {
        public enum Status
        {
            Init,
            SendRequest,
            SendCancel,// 只有 SendRequest 会转移到这个状态
            OnRequestParseArgs,
            OnRequest,
            OnCanceled,// 只有 OnRequest 转移到这个状态。这时rpc需要及时结束
            SendResponse,
            OnResponseParseArgs,
            OnResponse,
            OnSuccess,
            OnError,
        }

        public abstract string m_method { get; }
        public MyId m_id;
        public Status m_status = Status.Init;

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

        public abstract void OnCanceled();

        /// <summary>
        /// 返回结果。调用前，先设置好 m_res 或者 m_err
        /// </summary>
        /// <param name="args"></param>
        public abstract void SendResponse();
        #endregion

        #region Server RPC
        /// <summary>
        /// 发出请求，应该准备好m_req。会新生成id。应该只调用一次。
        /// </summary>
        public abstract void SendRequest();

        /// <summary>
        /// 发送取消指令
        /// </summary>
        public abstract void SendCancel();
        /// <summary>
        /// 收到返回
        /// </summary>
        /// <param name="result"></param>
        public abstract void OnResponseParseData(JsonNode? result, ResponseError? error);
        /// <summary>
        /// 请求返回。默认实现分流调用 OnSuccess OnError
        /// </summary>
        public abstract void OnResponse();
        /// <summary>
        /// 请求返回成功。由具体的rpc实现
        /// </summary>
        protected abstract void OnSuccess();
        /// <summary>
        /// 请求返回失败。默认实现在日志里输出点错误信息，具体的rpc也可以 override.
        /// </summary>
        protected abstract void OnError();
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
            m_status = Status.OnRequestParseArgs;
            if (args != null)
            {
                m_req = new TReq();
                m_req.ReadFrom(args);
            }
        }

        public override sealed void SendResponse()
        {
            m_status = Status.SendResponse;
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
            JsonRpcMgr.Instance.SendResponseOrError(this, data);
        }

        public override sealed void SendRequest()
        {
            m_status = Status.SendRequest;
            var data = new JsonObject();
            m_id = MyId.NewId();
            data["id"] = m_id.ToJsonNode();
            data["method"] = m_method;
            if (m_res != null)
            {
                data["params"] = m_res.ToJsonNode();
            }
            JsonRpcMgr.Instance.SendRequest(this, data);
        }

        public override sealed void SendCancel()
        {
            throw new NotImplementedException();
        }

        public override sealed void OnResponseParseData(JsonNode? result, ResponseError? error)
        {
            if (error != null)
            {
                m_err = error;
            }
            else if(result != null)
            {
                m_res = new TRes();
                m_res.ReadFrom(result);
            }
        }

        public override sealed void OnResponse()
        {
            if (m_err != null)
            {
                OnError();
            }
            else
            {
                OnSuccess();
            }
        }

        protected override void OnError()
        {
            My.Logger.Error($"rpc.OnError id={m_id} err={m_err!.ToJsonNode()}");
        }
    }
}
