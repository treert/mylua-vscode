using myserver;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyServer.Protocol
{
    internal class JsonRpcMgr
    {
        public static JsonRpcMgr Instance = new JsonRpcMgr();

        public void Init()
        {
            var assembly = typeof(JsonRpcMgr).Assembly;
            var types = assembly.GetTypes();
            var rpc_type = typeof(JsonRpcBase);
            var ntf_type = typeof(JsonNotifyBase);
            foreach (var t in types)
            {
                if (!t.IsAbstract && !t.IsGenericType)
                {
                    if (t.IsSubclassOf(rpc_type))
                    {
                        var rpc = Activator.CreateInstance(rpc_type) as JsonRpcBase;
                        var method = rpc!.m_method;
                        m_rpc_type_map.Add(method, t);
                    }
                    else if (t.IsSubclassOf(ntf_type))
                    {
                        var ntf = Activator.CreateInstance(ntf_type) as JsonNotifyBase;
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
            MyId? id = data["id"]?.ToMyId();
            string? method = data["method"]?.GetValue<string>();
            JsonNode? args = data["params"];
            JsonNode? result = data["result"];
            ResponseError? error = data["error"]?.ToResponseError();

            if (id is not null)// rpc
            {
                if (error is null)
                {
                    if(method is null) // res
                    {
                        if(m_server_rpc_map.TryGetValue(id,out var rpc))
                        {
                            rpc.OnResponse(result);
                        }
                        else
                        {
                            My.Logger.Error($"client res not valid id. {data}");
                        }
                    }
                    else// req
                    {
                        OnGetRequest(id, method, args);

                    }
                }
                else// res fail
                {
                    if (m_server_rpc_map.TryGetValue(id, out var rpc))
                    {
                        rpc.OnError(error);
                    }
                    else
                    {
                        My.Logger.Error($"client fail res not valid id. {data}");
                    }
                }
            }
            else if(method is not null) // notify
            {
                OnGetNotify(method, args);
            }
            else
            {
                My.Logger.Error($"recv bad format. {data}");
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
        public void OnGetRequest(MyId id, string method, JsonNode? args)
        {
            if (method == MyConst.Method.Init)
            {
                InitRpc initRpc = new InitRpc();
                return;// 初始化协议需要特殊处理
            }
            if (!MyServerMgr.Instance.IsInited)
            {
                SendErrorForRPC(id, ErrorCodes.ServerNotInitialized, "");
                return;
            }
            if (m_rpc_type_map.TryGetValue(method, out var tt))
            {
                JsonRpcBase? rpc = Activator.CreateInstance(tt) as JsonRpcBase;
                Debug.Assert(rpc != null);
                rpc.m_args = args;
                m_client_rpc_list.Enqueue(rpc);
                TryHandleOneRequest();
            }
            else
            {
                My.Logger.Error($"client req not support. method={method} args={args}");
            }
        }

        /// <summary>
        /// 有些情况需要特殊处理
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        public void OnGetNotify(string method, JsonNode? args)
        {
            if(method == MyConst.Method.Exit)
            {
                MyServerMgr.Instance.ExitApp();
                return;// 优先级最高
            }
            if (!MyServerMgr.Instance.IsInited)
            {
                return;// 为初始化前，忽视所有的ntf
            }
            if (m_notify_type_map.TryGetValue(method, out var tt))
            {
                JsonNotifyBase? notify = Activator.CreateInstance(tt) as JsonNotifyBase;
                Debug.Assert(notify != null);
                notify.OnNotify(args);
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
        public void TryHandleOneRequest()
        {
            if (m_client_rpc_doing is null && m_client_rpc_list.TryDequeue(out m_client_rpc_doing))
            {
                m_client_rpc_doing.OnRequest(m_client_rpc_doing.m_args);
            }
        }

        public void SendResponseOrError(JsonRpcBase rpc, JsonNode data)
        {
            Debug.Assert(rpc == m_client_rpc_doing);
            m_client_rpc_doing = null;
            SendData(data);

            TryHandleOneRequest();
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
