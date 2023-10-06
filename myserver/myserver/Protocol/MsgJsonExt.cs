
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

/*
 集中定义协议的 json parse 部分。想通过外包配置来自动生成来着。
 */

namespace MyServer.Protocol
{
    public static class JsonRpcExtensions
    {

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
