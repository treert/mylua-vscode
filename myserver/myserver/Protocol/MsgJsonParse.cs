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
                _number = node.AsValue().GetValue<int>();
            }
        }

        public JsonNode? ToJsonNode()
        {
            if (IsNumber)
            {
                return Number;
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
            else if (node.AsValue().TryGetValue<int>(out var num))
            {
                return new MyId(num);
            }
            throw new Exception();
        }

        public static ResponseError ToResponseError(this JsonNode node)
        {
            var error = new ResponseError();
            error.code = node["code"]!.GetValue<int>();
            error.message = node["message"]!.GetValue<string>();
            error.data = node["data"];
            return error;
        }
    }
}
