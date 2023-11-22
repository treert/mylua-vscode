using myserver;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static MyServer.Misc.MyConst;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyServer.Protocol
{
    public partial class JsonRpcMgr
    {
        public static JsonRpcMgr Instance = new JsonRpcMgr();

        public void Init()
        {
            // clear cache
            m_client_rpc_doing = null;
            m_client_rpc_list.Clear();
            m_server_rpc_map.Clear();
            m_notify_type_map.Clear();
            m_rpc_type_map.Clear();

            var assembly = typeof(JsonRpcMgr).Assembly;
            var types = assembly.GetTypes();
            var rpc_type = typeof(JsonRpcBase);
            var ntf_type = typeof(JsonNtfBase);
            foreach (var t in types)
            {
                if (!t.IsAbstract && !t.IsGenericType)
                {
                    if (t.IsSubclassOf(rpc_type))
                    {
                        var rpc = Activator.CreateInstance(t) as JsonRpcBase;
                        var method = rpc!.m_method;
                        m_rpc_type_map.Add(method, t);
                    }
                    else if (t.IsSubclassOf(ntf_type))
                    {
                        var ntf = Activator.CreateInstance(t) as JsonNtfBase;
                        var method = ntf!.m_method;
                        m_notify_type_map.Add(method, t);
                    }
                }
            }
        }

        /// <summary>
        /// 收到 json 消息. 需要处理各种分发。
        /// </summary>
        /// <param name="data"></param>
        public void ReceiveData(JsonNode data)
        {
            MyId? id = data["id"];
            string? method = data["method"]?.GetValue<string>();
            JsonNode? args = data["params"];
            JsonNode? result = data["result"];
            ResponseError? error = data["error"]?.ToResponseError();

            if (id is not null)// rpc
            {
                if(method is not null)// req
                {
                    OnGetRequest(id, method, args);
                }
                else// res
                {
                    OnGetResponse(id, result, error);
                }
            }
            else if(method is not null) // notify
            {
                OnGetNotify(method, args);
            }
            else
            {
                My.Logger.Error($"recv bad format msg. {data}");
            }
        }

        public void SendData(JsonNode data)
        {
            data["jsonrpc"] = "2.0";
            MySession.Instance.SendData(data);
        }

        public void SendErrorForRPC(MyId id, int code, string? msg)
        {
            var data = new JsonObject();
            data["id"] = id.ToJsonNode();
            var err = new JsonObject();
            err["code"] = code;
            err["message"] = msg;
            data["error"] = err;
            SendData(data);
        }

        /// <summary>
        /// 收到请求。有些请求需要特殊处理
        /// </summary>
        /// <param name="id"></param>
        /// <param name="method"></param>
        /// <param name="args"></param>
        private void OnGetRequest(MyId id, string method, JsonNode? args)
        {
            My.Logger.Debug($"rpc.OnGetRequest id={id} {method}");
            if (!MyServerMgr.Instance.IsInited && method != MyConst.Method.Init)
            {
                SendErrorForRPC(id, ErrorCodes.ServerNotInitialized, "server has not initialized");
                return;
            }
            if (m_rpc_type_map.TryGetValue(method, out var tt))
            {
                JsonRpcBase? rpc = Activator.CreateInstance(tt) as JsonRpcBase;
                Debug.Assert(rpc != null);
                rpc.m_id = id;
                rpc.OnRequestParseArgs(args);
                m_client_rpc_list.Enqueue(rpc);
                TryHandleOneRequest();
            }
            else
            {
                My.Logger.Error($"method not support. OnGetRequest method={method}");
            }
        }

        private void OnGetResponse(MyId id, JsonNode? result, ResponseError? error)
        {
            if (m_server_rpc_map.TryGetValue(id, out var rpc))
            {
                rpc.OnResponseParseData(result, error);
                rpc.OnResponse();
            }
            else
            {
                My.Logger.Error($"client res unexpect. id={id} result={result} error={error}");
            }
        }

        /// <summary>
        /// 有些情况需要特殊处理
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        private void OnGetNotify(string method, JsonNode? args)
        {
            if(method == MyConst.Method.Exit)
            {
                MyServerMgr.Instance.ExitApp();
                return;// 优先级最高
            }
            //if (!MyServerMgr.Instance.IsInited)
            //{
            //    return;// 为初始化前，忽视所有的ntf
            //}
            if (m_notify_type_map.TryGetValue(method, out var tt))
            {
                JsonNtfBase? notify = Activator.CreateInstance(tt) as JsonNtfBase;
                Debug.Assert(notify != null);
                notify.OnNotifyParseArgs(args);
                notify.OnNotify();
            }
            else
            {
                My.Logger.Error($"client req not support. method={method} args={args}");
            }
        }

        public void SendRequest(JsonRpcBase rpc, JsonNode data)
        {
            m_server_rpc_map.Add(rpc.m_id, rpc);
            SendData(data);
        }

        // 现在一次只会处理一个
        private void TryHandleOneRequest()
        {
            if (m_client_rpc_doing is null && m_client_rpc_list.TryDequeue(out m_client_rpc_doing))
            {
                My.Logger.Debug($"rpc.OnRequest id={m_client_rpc_doing.m_id} {m_client_rpc_doing.m_method}");
                m_client_rpc_doing.OnRequest();
            }
        }

        public void SendResponseOrError(JsonRpcBase rpc, JsonNode data)
        {
            Debug.Assert(rpc == m_client_rpc_doing);
            m_client_rpc_doing = null;
            SendData(data);

            TryHandleOneRequest();
        }

        public void CancelRequest(MyId id)
        {
            if(m_client_rpc_doing is not null && m_client_rpc_doing.m_id == id)
            {
                My.Logger.Debug($"rpc.CancelRequest id={id} {m_client_rpc_doing.m_method}");
                m_client_rpc_doing.OnCanceled();
            }
            else
            {
                var arr = m_client_rpc_list.ToArray();
                m_client_rpc_list.Clear();
                foreach (var item in arr)
                {
                    if(item.m_id != id)
                    {
                        m_client_rpc_list.Enqueue(item);
                    }
                    else
                    {
                        My.Logger.Debug($"rpc.CancelRequest id={id} {item.m_method} NotStartYet");
                        // 直接丢弃，反正还没开始工作。
                    }
                }
            }
        }

        /// <summary>
        /// client request list. wait for server res.
        /// </summary>
        private Queue<JsonRpcBase> m_client_rpc_list = new Queue<JsonRpcBase>();
        private JsonRpcBase? m_client_rpc_doing = null;
        /// <summary>
        /// server request. wait for client res.
        /// </summary>
        private Dictionary<MyId, JsonRpcBase> m_server_rpc_map = new();
        /// <summary>
        /// 处理rpc的类型
        /// </summary>
        private Dictionary<string, Type> m_rpc_type_map = new Dictionary<string, Type>();
        /// <summary>
        /// 处理notfiy的类型
        /// </summary>
        private Dictionary<string, Type> m_notify_type_map = new Dictionary<string, Type>();
    }
}
