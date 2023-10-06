
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyServer.Protocol
{
    [JsonConverter(typeof(MyJsonEnumConverter))]
    public enum LspTrace
    {
        Off,
        Messages,
        Verbose,
    }

    public class LogTraceParams
    {
        /// <summary>
        /// Additional information that can be computed if the `trace` configuration
        /// is set to `'verbose'`
        /// </summary>
        public LspTrace? Trace { get; set; }
        public string message { get; set; }
    }

    /// <summary>
    /// 服务器发送日志给客户端
    /// </summary>
    public class NtfLogTrace : JsonNtfBase<LogTraceParams>
    {
        public override string m_method => "$/logTrace";

        public override void OnNotify()
        {
            throw new NotImplementedException();
        }
    }

    public class SetTraceParams
    {
        public LspTrace value { get; set; }
    }

    public class NtfSetTrace : JsonNtfBase<SetTraceParams>
    {
        public override string m_method => "$/setTrace";

        public override void OnNotify()
        {
            MyServerMgr.Instance.LspTrace = Args.value;
        }
    }
}
