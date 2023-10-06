
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol.LanguageFeatues;

public class CallHierarchyOutgoingCall
{
    /// <summary>
    /// The item that is called.
    /// </summary>
    public CallHierarchyItem to { get; set; }
    /// <summary>
    /// The range at which this item is called. This is the range relative to
    /// the caller, e.g the item passed to `callHierarchy/outgoingCalls` request.
    /// </summary>
    public Range[] fromRanges { get; set; }
}

public class RpcCallHierarchyOutgoing : JsonRpcBase<CallHierarchyIncomingParams, List<CallHierarchyOutgoingCall>>
{
    public override string m_method => "callHierarchy/outgoingCalls";

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

