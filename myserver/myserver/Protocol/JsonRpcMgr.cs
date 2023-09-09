using myserver;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

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
                        if(m_rpc_type_map.TryGetValue(method,out var tt))
                        {
                            JsonRpcBase? rpc = Activator.CreateInstance(tt) as JsonRpcBase;
                            Debug.Assert(rpc != null);
                            rpc.m_args = args;
                            m_client_rpc_list.Enqueue(rpc);
                            TryHandleOneRequest();
                        }
                        else
                        {
                            My.Logger.Error($"client req not support. method={method}. {data}");
                        }
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
                if (m_notify_type_map.TryGetValue(method, out var tt))
                {
                    JsonNotifyBase? notify = Activator.CreateInstance(tt) as JsonNotifyBase;
                    Debug.Assert(notify != null);
                    notify.OnNotify(args);
                }
                else
                {
                    My.Logger.Error($"client req not support. method={method}. {data}");
                }
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
