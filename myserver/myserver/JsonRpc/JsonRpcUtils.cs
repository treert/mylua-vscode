using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.JsonRpc
{
    internal class JsonRpcUtils
    {
    }

    public interface IJson
    {
        void ReadFrom(JsonNode? node);
        JsonNode? ToJsonNode();
    }
}
