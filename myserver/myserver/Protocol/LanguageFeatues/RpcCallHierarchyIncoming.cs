using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol.LanguageFeatues;
public class CallHierarchyIncomingParams : IJson, IWorkDoneProgress, IPartialResult
{
    public ProgressToken? partialResultToken { get; set; }
    public ProgressToken? workDoneToken { get; set; }

    public CallHierarchyItem item;

    public virtual void ReadFrom(JsonNode node)
    {
        item = node["item"]!.ConvertTo<CallHierarchyItem>();
        partialResultToken = node["partialResultToken"];
        workDoneToken = node["workDoneToken"];
    }

    public virtual JsonNode ToJsonNode()
    {
        JsonObject data = new JsonObject();
        data.AddKeyValue("item", item);
        data.TryAddKeyValue("partialResultToken", partialResultToken);
        data.TryAddKeyValue("workDoneToken", workDoneToken);
        return data;
    }
}

public class CallHierarchyIncomingCall
{
    /// <summary>
    /// The item that makes the call.
    /// </summary>
    public CallHierarchyItem from { get; set; }
    /// <summary>
    /// The ranges at which the calls appear. This is relative to the caller
    /// denoted by[`this.from`](#CallHierarchyIncomingCall.from).
    /// </summary>
    public Range[] fromRanges { get; set; }
}

public class CallHierarchyIncomingResult : IJson
{
    public List<CallHierarchyIncomingCall> items = new List<CallHierarchyIncomingCall>();
    public void ReadFrom(JsonNode node)
    {
        items = node.ConvertTo<List<CallHierarchyIncomingCall>>();
    }

    public JsonNode ToJsonNode()
    {
        return items.ToJsonNode();
    }
}

public class RpcCallHierarchyIncoming : JsonRpcBase<CallHierarchyIncomingParams, CallHierarchyIncomingResult>
{
    public override string m_method => "callHierarchy/incomingCalls";

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
