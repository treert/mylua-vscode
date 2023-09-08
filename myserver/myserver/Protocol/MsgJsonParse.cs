using MyServer.JsonRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

/*
 集中定义协议的 json parse 部分。想通过外包配置来自动生成来着。
 */

namespace MyServer.Protocol
{
    public partial record MyId : IJson
    {
        public void ReadFrom(JsonNode? node)
        {
            var val = node!.AsValue();
            if (val.TryGetValue<string>(out _string))
            {
                _long = node.AsValue().GetValue<long>();
            }
        }

        public JsonNode? ToJsonNode()
        {
            if (IsLong)
            {
                return Long;
            }
            else
            {
                return String;
            }
        }
    }

    public static class JsonRpcExtensions
    {
        public static MyId ToMyId(this JsonNode node)
        {
            if (node.AsValue().TryGetValue<string>(out var str))
            {
                return new MyId(str);
            }
            else if (node.AsValue().TryGetValue<long>(out var num))
            {
                return new MyId(num);
            }
            throw new Exception();
        }
    }
}
