
// lsp 里定义的一些 id: integer | string;
using MyServer.Misc;
using System;
using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace MyServer.Protocol;

[JsonConverter(typeof(MyIdJsonConverter))]
public record MyId : IEquatable<int>, IEquatable<string>
{
    private int? _number;
    private string? _string;

    public MyId()
    {
        _number = null;
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

    static int s_num = 1;
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
        if (id.IsNumber)
        {
            return id.Number;
        }
        else
        {
            return id.String!;
        }
    }

    public bool Equals(int other) => IsNumber && _number == other;
    public bool Equals(string? other) => IsString && _string == other;

    private string DebuggerDisplay => IsString ? String : Number.ToString();

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
        if (value.IsString)
        {
            writer.WriteStringValue(value.String);
        }
        else
        {
            writer.WriteNumberValue(value.Number);
        }
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



public class ResponseError
{
    public int code { get; set; }
    public string message { get; set; }
    public JsonNode? data { get; set; }
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

/// <summary>
/// A notebook document filter denotes a notebook document by different properties.
/// </summary>
public class NotebookDocumentFilter
{
    /// <summary>
    /// e.g. jupyter-notebook
    /// </summary>
    public string? notebookType { get; set;}
    /// <summary>
    /// e.g. file
    /// </summary>
    public string? scheme { get; set; }
    /// <summary>
    /// e.g. **/books1/**
    /// </summary>
    public string? pattern { get; set; }
}

/// <summary>
/// A notebook cell text document filter denotes a cell text document by different properties.
/// </summary>
public class NotebookCellTextDocumentFilter
{

    public NotebookDocumentFilter notebook { get; set; }
    public string? language { get; set; }
}

#endregion NoteBook
