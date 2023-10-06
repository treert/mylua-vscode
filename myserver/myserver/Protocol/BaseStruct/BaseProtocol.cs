using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class ArrayJson<T> : IJson
{
    public List<T> Items { get; set; } = new List<T>();

    public void ReadFrom(JsonNode node)
    {
        Items = node.ConvertTo<List<T>>();
    }

    public JsonNode ToJsonNode()
    {
        return Items.ToJsonNode();
    }
}

public class OneJson<T> : IJson where T : new()
{
    public T Value { get; set; } = new T();

    public void ReadFrom(JsonNode node)
    {
        Value = node.ConvertTo<T>();
    }

    public JsonNode ToJsonNode()
    {
        return Value!.ToJsonNode();
    }
}

public enum ProtoDirection
{
    ToServer,
    ToClient,
}


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class MyProtoAttribute : Attribute
{
    public required ProtoDirection Direction { get; set; }
}
