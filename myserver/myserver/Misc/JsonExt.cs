﻿using System;
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
    public static class MyJsonExt
    {
        private static JsonSerializerOptions s_json_options = new JsonSerializerOptions()
        {
            WriteIndented = false,
        };

        public static string ToJsonStr(this JsonNode node)
        {
            return node.ToJsonString(s_json_options);
        }

        public static T ConvertTo<T>(this JsonNode node)
        {
            // 这个实现性能并不好。它会先序列化成字符串，然后再重新解析一遍。(⊙o⊙)…
            T t = JsonSerializer.Deserialize<T>(node, s_json_options)!;
            return t;
        }

        public static JsonNode ToJsonNode(this object value)
        {
            var node = JsonSerializer.SerializeToNode(value, value.GetType(), s_json_options);
            return node!;
        }

        public static void AddKeyValue(this JsonObject json, string key, object? value)
        {
            json.Add(key, value?.ToJsonNode());
        }

        public static bool TryAddKeyValue(this JsonObject json, string key, object? value)
        {
            if (value != null)
            {
                json.Add(key, value.ToJsonNode());
                return true;
            }
            return false;
        }
    }

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
