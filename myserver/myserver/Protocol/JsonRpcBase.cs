
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

lsp一般只要使用一半的生命周期。
具体到单个rpc，OnRequest/OnCanceled/OnSuccess 如果不会用到，抛异常就行，算是Assert吧。
OnError 默认只是输出下日志，按需要 override 。

Notify 生命周期
    1. SendNotify
    2. OnNotifyParseArgs
    3. OnNotify

*/

namespace MyServer.Protocol
{
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
        where TReq : class, new()
        where TRes : class, new()
    {
        public TReq? m_req;
        public TReq ReqArgs {
            get {
                if(m_req == null)
                {
                    m_req = new TReq();
                }
                return m_req;
            }
            set
            {
                m_req = value;
            }
        }
        public TRes? m_res;
        public TRes ResData
        {
            get
            {
                if (m_res == null)
                {
                    m_res = new TRes();
                }
                return m_res;
            }
            set
            {
                m_res = value;
            }
        }
        public ResponseError? m_err;

        public override sealed void OnRequestParseArgs(JsonNode? args)
        {
            m_req = args?.ConvertTo<TReq>();
        }

        public override sealed void SendResponse()
        {
            var data = new JsonObject
            {
                ["id"] = m_id.ToJsonNode()
            };
            if (m_err != null)
            {
                My.Logger.Debug($"rpc.SendResponse id={m_id} {m_method}-Error");
                data["error"] = m_err.ToJsonNode();
            }
            else
            {
                My.Logger.Debug($"rpc.SendResponse id={m_id} {m_method}");
                data["result"] = m_res?.ToJsonNode();
            }
            JsonRpcMgr.Instance.SendResponseOrError(this, data);
        }

        public override sealed void SendRequest()
        {
            My.Logger.Debug($"rpc.SendRequest id={m_id} {m_method}");
            var data = new JsonObject();
            m_id = MyId.NewId();
            data["id"] = m_id.ToJsonNode();
            data["method"] = m_method;
            if (m_req != null)
            {
                data["params"] = m_req.ToJsonNode();
            }
            JsonRpcMgr.Instance.SendRequest(this, data);
        }

        public override sealed void SendCancel()
        {
            My.Logger.Debug($"rpc.SendCancel id={m_id} {m_method}");
            NtfCancelRequest ntf = new NtfCancelRequest();
            ntf.m_args = new CancelParams()
            {
                id = m_id,
            };
            ntf.SendNotify();
        }

        // 默认实现忽略cancel。如果已经开始了，就不能cancel。
        public override void OnCanceled()
        {
            // ignore
        }

        public override sealed void OnResponseParseData(JsonNode? result, ResponseError? error)
        {
            if (error != null)
            {
                My.Logger.Debug($"rpc.OnGetResponse id={m_id} {m_method}-Error");
                m_err = error;
            }
            else
            {
                My.Logger.Debug($"rpc.OnGetResponse id={m_id} {m_method}");
                m_res = result?.ConvertTo<TRes>();
            }
        }

        public override sealed void OnResponse()
        {
            
            if (m_err != null)
            {
                My.Logger.Debug($"rpc.OnResponse id={m_id} {m_method}-Error");
                OnError();
            }
            else
            {
                My.Logger.Debug($"rpc.OnResponse id={m_id} {m_method}-Error");
                OnSuccess();
            }
        }

        protected override void OnError()
        {
            My.Logger.Error($"rpc.OnError id={m_id} err={m_err!.ToJsonNode()}");
        }
    }

    public abstract class JsonRpcEmptyBase : JsonRpcBase<Dummy,Dummy>
    {

    }
}
