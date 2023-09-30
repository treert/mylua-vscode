using MyServer.JsonRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol
{
    public enum LspTrace
    {
        Off,
        Messages,
        Verbose,
    }

    public static class LspTraceExt
    {
        public static string TraceToStr(this LspTrace node)
        {
            switch (node)
            {
                default:
                    return "off";
                case LspTrace.Messages:
                    return "messages";
                case LspTrace.Verbose:
                    return "verbose";
            }
        }

        public static LspTrace StrToTrace(this string node)
        {
            switch (node)
            {
                case "messages":
                    return LspTrace.Messages;
                case "verbose":
                    return LspTrace.Verbose;
                default:
                    return LspTrace.Off;
            }
        }
    }

    public class LogTraceParams : IJson
    {
        public LspTrace? Trace;
        public string message;

        public void ReadFrom(JsonNode node)
        {
            throw new NotImplementedException();
        }

        public JsonNode ToJsonNode()
        {
            JsonObject data = new JsonObject();
            data.Add("message", message);
            if (Trace != null)
            {
                data.Add("verbose", Trace.Value.TraceToStr());
            }
            return data;
        }
    }

    public class NtfLogTrace : JsonNotifyBase<LogTraceParams>
    {
        public override string m_method => "$/logTrace";

        public override void OnNotify()
        {
            throw new NotImplementedException();
        }
    }

    public class SetTraceParams : IJson
    {
        public LspTrace Trace;
        public void ReadFrom(JsonNode node)
        {
            var str = node["value"]!.GetValue<string>();
            Trace = str.StrToTrace();
        }

        public JsonNode ToJsonNode()
        {
            throw new NotImplementedException();
        }
    }

    public class NtfSetTrace : JsonNotifyBase<SetTraceParams>
    {
        public override string m_method => "$/setTrace";

        public override void OnNotify()
        {
            MyServerMgr.Instance.LspTrace = m_args.Trace;
        }
    }
}
