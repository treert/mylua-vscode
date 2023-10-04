using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol.LanguageFeatues;

public class PosAndTokenParams : IWorkDoneProgress, IPartialResult, IJson
{
    public ProgressToken? partialResultToken { get; set; }
    public ProgressToken? workDoneToken { get; set; }
    public TextDocPosition TextDocPosition { get; set; }

    public virtual void ReadFrom(JsonNode node)
    {
        TextDocPosition = node.ConvertTo<TextDocPosition>();
        partialResultToken = node["partialResultToken"];
        workDoneToken = node["workDoneToken"];
    }

    public virtual JsonNode ToJsonNode()
    {
        JsonObject data = TextDocPosition.ToJsonNode().AsObject();
        data.TryAddKeyValue("partialResultToken", partialResultToken);
        data.TryAddKeyValue("workDoneToken", workDoneToken);
        return data;
    }
}

public class GotoDeclareResult : IJson
{
    public void ReadFrom(JsonNode node)
    {
        throw new NotImplementedException();
    }

    public JsonNode ToJsonNode()
    {
        throw new NotImplementedException();
    }
}

public class RpcGotoDeclare : JsonRpcBase<PosAndTokenParams, GotoDeclareResult>
{
    public override string m_method => "textDocument/declaration";

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
