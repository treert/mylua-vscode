using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyServer.Protocol.LanguageFeatues;


[JsonConverter(typeof(MyJsonEnumConverter))]
public enum MarkupKind
{
    PlainText,
    Markdown,
}

public class MarkupContent
{
    /// <summary>
    /// MarkupKind : plaintext | markdown
    /// </summary>
    public MarkupKind kind {  get; set; }
    public string value { get; set; }
}

public class HoverResult : IJson
{
    public MarkupContent contents { get; set; }
    public Range? range { get; set; }

    public void ReadFrom(JsonNode node)
    {
        contents = node["contents"]!.ConvertTo<MarkupContent>();
        range = node["range"]?.ConvertTo<Range>();
    }

    public JsonNode ToJsonNode()
    {
        JsonObject data = new JsonObject();
        data.AddKeyValue("contents", contents);
        data.TryAddKeyValue("range", range);
        return data;
    }
}


public class RpcHover : JsonRpcBase<PosAndTokenParams, HoverResult>
{
    public override string m_method => "textDocument/hover";

    public override void OnCanceled()
    {
       // todo
    }

    public override void OnRequest()
    {
        // todo
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}


