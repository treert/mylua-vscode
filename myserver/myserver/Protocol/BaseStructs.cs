global using ProgressToken = MyServer.Protocol.MyId;
// lsp 里定义的一些 id: integer | string;
using MyServer.JsonRpc;
using System.Text.Json.Nodes;


namespace MyServer.Protocol;
public record MyId : IEquatable<int>, IEquatable<string>, IJson
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