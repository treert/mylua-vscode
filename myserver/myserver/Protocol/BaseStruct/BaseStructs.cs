
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

public interface IWorkDoneProgress
{
    ProgressToken? workDoneToken { get; set; }
}

public interface IPartialResult
{
    ProgressToken? partialResultToken { get; set; }
}

public class TextDocPosition
{
    public TextDocId textDocument { get; set; }
    public Position? position { get; set; }
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



public enum SymbolKind
{
    File = 1,
    Module = 2,
    Namespace = 3,
    Package = 4,
    Class = 5,
    Method = 6,
    Property = 7,
    Field = 8,
    Constructor = 9,
    Enum = 10,
    Interface = 11,
    Function = 12,
    Variable = 13,
    Constant = 14,
    String = 15,
    Number = 16,
    Boolean = 17,
    Array = 18,
    Object = 19,
    Key = 20,
    Null = 21,
    EnumMember = 22,
    Struct = 23,
    Event = 24,
    Operator = 25,
    TypeParameter = 26,
}

public enum SymbolTag
{
    Deprecated = 1,
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








#region NoteBook

public class ExecutionSummary
{
    /// <summary>
    /// A strict monotonically increasing value 
    /// indicating the execution order of a cell inside a notebook.
    /// </summary>
    public uint executionOrder { get; set; }

    /// <summary>
    /// Whether the execution was successful or not if known by the client.
    /// </summary>
    public bool? success { get; set; }
}

public enum NotebookCellKind
{
    /// <summary>
    /// A markup-cell is formatted source that is used for display.
    /// </summary>
    Markup = 1,
    /// <summary>
    /// A code-cell is source code.
    /// </summary>
    Code = 2,
}



public class NotebookCell
{
    public NotebookCellKind kind { get; set;}
    public Uri document { get; set; }
    public JsonObject? metadata { get; set; }
    public ExecutionSummary? executionSummary { get; set; }
}

public class NotebookDoc
{
    public Uri uri { get; set; }
    public string notebookType { get; set; }
    public int version { get; set; }
    public JsonObject? metadata { get; set; }
}

[JsonConverter(typeof(NotebookDocumentFilterConverter))]
public class NotebookDocumentFilter
{
    public string? notebookType { get; set;}
    public string? scheme { get; set; }
    public string? pattern { get; set; }
    /// <summary>
    /// 兼容处理下。 lsp 里定义成了 string | NotebookDocumentFilter
    /// </summary>
    public string? type_pattern { get; set; }


    public bool IsValid()
    {
        return type_pattern != null || scheme != null || pattern != null || notebookType != null;
    }
}

public class NotebookDocumentFilterConverter : JsonConverter<NotebookDocumentFilter>
{
    //public override bool HandleNull => true;
    public override NotebookDocumentFilter? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        NotebookDocumentFilter ret = new NotebookDocumentFilter();
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                ret.type_pattern = reader.GetString();
                break;
            case JsonTokenType.StartObject:
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        break;
                    }
                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        var propertyName = reader.GetString();
                        reader.Read();
                        switch (propertyName)
                        {
                            case "notebookType":
                                ret.notebookType = reader.GetString();
                                break;
                            case "scheme":
                                ret.scheme = reader.GetString();
                                break;
                            case "pattern":
                                ret.pattern = reader.GetString();
                                break;
                        }
                    }
                }
                break;
        }
        if (ret.IsValid())
        {
            return ret;
        }
        throw new Exception();
    }

    public override void Write(Utf8JsonWriter writer, NotebookDocumentFilter value, JsonSerializerOptions options)
    {
        if (value.type_pattern == null)
        {
            writer.WriteStartObject();
            if (value.notebookType == null)
            {
                writer.WriteString("notebookType", value.notebookType);
            }
            if (value.scheme == null)
            {
                writer.WriteString("scheme", value.scheme);
            }
            if (value.pattern == null)
            {
                writer.WriteString("pattern", value.pattern);
            }
            writer.WriteEndObject();
        }
        else
        {
            writer.WriteStringValue(value.type_pattern);
        }
    }
}

public class NotebookCellTextDocumentFilter
{
    public NotebookDocumentFilter notebook { get; set; }
    public string? language { get; set; }
}

#endregion NoteBook
