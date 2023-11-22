using MyServer.Misc;
using MyServer.Protocol;
using System.Text.Json.Nodes;

namespace MyServer.Protocol;

public abstract class JsonNtfBase
{
    public abstract string m_method { get; }
    public virtual void OnNotifyParseArgs(JsonNode? args) {
        My.Logger.Debug($"OnNotify {m_method}");
    }
    /// <summary>
    /// 子类需要实现。ToClient 类型的实现个空的。
    /// </summary>
    public abstract void OnNotify();
    /// <summary>
    /// 发送请求。如果有参数，设置好参数后，再调用。
    /// </summary>
    public virtual void SendNotify()
    {
        My.Logger.Debug($"SendNotify {m_method}");
        JsonObject data = new();
        data["method"] = m_method;
        JsonRpcMgr.Instance.SendData(data);
    }
}

public abstract class JsonNtfBase<TArgs> : JsonNtfBase where TArgs : class,new()
{
    public TArgs? m_args;
    public TArgs Args { get {
            if (m_args == null)
            {
                m_args = new TArgs();
            }
            return m_args;
        }
    }

    public override sealed void OnNotifyParseArgs(JsonNode? args)
    {
        My.Logger.Debug($"OnNotify {m_method}");
        m_args = args?.ConvertTo<TArgs>();
    }

    public override sealed void SendNotify()
    {
        My.Logger.Debug($"SendNotify {m_method}");
        JsonObject data = new();
        data["method"] = m_method;
        if (m_args is not null)
        {
            data["params"] = m_args.ToJsonNode();
        }
        JsonRpcMgr.Instance.SendData(data);
    }
}