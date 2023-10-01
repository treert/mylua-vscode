using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.JsonRpc
{
    internal class JsonRpcUtils
    {
    }

    public interface IJson
    {
        void ReadFrom(JsonNode node);
        JsonNode ToJsonNode();
    }

    public class EmptyObject : IJson
    {
        public void ReadFrom(JsonNode node)
        {
        }

        public JsonNode ToJsonNode()
        {
            return new JsonObject();
        }
    }
}
