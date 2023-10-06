
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol.LanguageFeatues;
public class CallHierarchyIncomingParams : IWorkDoneProgress, IPartialResult
{
    public ProgressToken? partialResultToken { get; set; }
    public ProgressToken? workDoneToken { get; set; }

    public CallHierarchyItem item { get; set; }
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

public class RpcCallHierarchyIncoming : JsonRpcBase<CallHierarchyIncomingParams, List<CallHierarchyIncomingCall>>
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
