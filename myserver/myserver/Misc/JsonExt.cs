using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Nodes;

namespace MyServer.Misc
{
    [JsonConverter(typeof(MyNodeJsonConverter))]
    public class MyNode
    {
        /// <summary>
        /// Json全局序列化选项。先放在这儿
        /// </summary>
        public static JsonSerializerOptions Options = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        public T? As<T>()
        {
            if (node is null)
            {
                return default(T?);
            }
            else
            {
                try
                {
                    T? ret = node.Deserialize<T>(MyNode.Options);
                    return ret;
                }
                catch (Exception)
                {
                    return default(T?);
                }
            }
        }

        public bool IsNull()
        {
            return node is null;
        }

        public JsonNode Node { get { return node!; } }

        public override string ToString()
        {
            if (node is null)
            {
                return "null";
            }
            else
            {
                return node.ToJsonString();
            }
        }
        public JsonNode? node;
    }

    public class MyNodeJsonConverter : JsonConverter<MyNode>
    {
        public override bool HandleNull => true;
        public override MyNode? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            MyNode mynode = new MyNode();
            mynode.node = JsonNode.Parse(ref reader, new JsonNodeOptions { PropertyNameCaseInsensitive = options.PropertyNameCaseInsensitive });
            return mynode;
        }

        public override void Write(Utf8JsonWriter writer, MyNode value, JsonSerializerOptions options)
        {
            if (value is null || value.node is null)
            {
                writer.WriteNullValue();
            }
            else
            {
                value.node.WriteTo(writer, options);
            }
        }
    }
}
