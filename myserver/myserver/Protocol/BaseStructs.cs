global using ProgressToken = MyServer.Protocol.MyId;
// lsp 里定义的一些 id: integer | string;
using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace MyServer.Protocol;

[JsonConverter(typeof(MyIdJsonConverter))]
public class MyId : IEquatable<int>, IEquatable<string>, IJson
{
    private int? _number;
    private string? _string;

    public MyId()
    {
        _string = string.Empty;
    }

    public MyId(int value)
    {
        _number = value;
        _string = null;
    }

    public MyId(string value)
    {
        _number = null;
        _string = value;
    }

    public bool IsNumber => _number.HasValue;

    public int Number
    {
        get => _number ?? 0;
        set
        {
            _string = null;
            _number = value;
        }
    }

    public bool IsString => _string != null;

    public string String
    {
        get => _string ?? string.Empty;
        set
        {
            _string = value;
            _number = null;
        }
    }

    static int s_num = 0;
    public static MyId NewId()
    {
        int num = Interlocked.Increment(ref s_num);
        return new MyId(num);
    }

    public static implicit operator MyId(int value) => new MyId(value);

    public static implicit operator MyId(string value) => new MyId(value);

    public static implicit operator MyId?(JsonNode? node)
    {
        if (node == null)
        {
            return null;
        }
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

    public static implicit operator JsonNode(MyId id)
    {
        return id.ToJsonNode();
    }

    public bool Equals(int other) => IsNumber && _number == other;
    public bool Equals(string? other) => IsString && _string == other;

    private string DebuggerDisplay => IsString ? String : IsNumber ? Number.ToString() : "";

    public override string ToString() => DebuggerDisplay;

    public void ReadFrom(JsonNode node)
    {
        var val = node!.AsValue();
        if (val.TryGetValue<string>(out _string))
        {
            _number = node.AsValue().GetValue<int>();
        }
    }

    public JsonNode ToJsonNode()
    {
        if (IsNumber)
        {
            return Number;
        }
        else
        {
            return String!;
        }
    }
}


public class MyIdJsonConverter : JsonConverter<MyId>
{
    //public override bool HandleNull => true;
    public override MyId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Number when reader.TryGetInt32(out int l) => l,
            JsonTokenType.String => reader.GetString()!,
            _ => throw new Exception("myid is int32 or string"),
        };
    }

    public override void Write(Utf8JsonWriter writer, MyId value, JsonSerializerOptions options)
    {
        JsonNode node = value;
        JsonSerializer.Serialize(writer, node, options);
    }
}

public interface IWorkDoneProgressParams : IJson
{
    ProgressToken? workDoneToken { get => null; }
}

public class WorkDoneProgressParams
{
    ProgressToken? workDoneToken = null;
    public void ReadFrom(JsonNode node)
    {
        workDoneToken = node["workDoneToken"];
    }

    public void WriteTo(JsonNode node)
    {
        if(workDoneToken != null)
        {
            node["workDoneToken"] = workDoneToken.ToJsonNode();
        }
    }
}

public class ResponseError : IJson
{
    public int code;
    public string message = string.Empty;
    public JsonNode? data;

    public void ReadFrom(JsonNode node)
    {
        code = node!["code"]!.GetValue<int>();
        message = node!["message"]!.GetValue<string>();
        data = node!["data"];
    }

    public JsonNode ToJsonNode()
    {
        JsonObject result = new()
            {
                { "code", code },
                { "message", message }
            };
        if (data != null)
        {
            result["data"] = data;
        }
        return result;
    }
}

public class TextDocItem
{
    public Uri uri { get; set; }
    public string languageId { get; set; }
    /// <summary>
    /// it will increase after each change, including undo/redo
    /// </summary>
    public int version { get; set; }
    public string text { get; set; }
}

public class TextDocId
{
    public Uri uri { get; set; }
}

public class VersionedTextDocId : TextDocId
{
    public int version { get; set; }
}

public class OptionalVersionedTextDocId : TextDocId
{
    public int? version { get; set; }
}

public class Position
{
    /// <summary>
    /// Line position in a document (zero-based).
    /// </summary>
    public uint line {get;set; }

    /// <summary>
    /// Character offset on a line in a document(zero-based). The meaning of this
    /// offset is determined by the negotiated `PositionEncodingKind`.
    /// 
    /// If the character value is greater than the line length it defaults back
    /// to the line length.
    /// </summary>
    public uint character { get;set;}
}

public class Range
{
    public Position start { get; set; }
    public Position end { get; set; }
}

public class TextEdit
{
    public Range range { get; set; }

    /// <summary>
    /// 替换的文本。delete 操作使用空串
    /// </summary>
    public string newText { get; set; }

    /// <summary>
    /// A special text edit with an additional change annotation.
    /// @since 3.16.0.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? annotationId { get; set; }
}

