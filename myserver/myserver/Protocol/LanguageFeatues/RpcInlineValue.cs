
using MyServer.Misc;
using MyServer.Protocol.BaseStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class InlineValueContext
{
    /// <summary>
    /// The stack frame (as a DAP Id) where the execution has stopped.
    /// </summary>
    public int frameId { get; set; }
    /// <summary>
    /// The document range where execution has stopped.
    /// Typically the end position of the range denotes the line where the
    /// inline values are shown.
    /// </summary>
    public Range stoppedLocation { get; set; }
}

public class InlineValueParams : DocIdAndTokenParams
{
    /// <summary>
    /// The document range for which inline values should be computed.
    /// </summary>
    public Range range { get; set; }
    public InlineValueContext context { get; set; }
}

/// <summary>
/// todo
/// </summary>
public class InlineValue
{
    public Range range { get; set; }
    public string? text { get; set; }
    public string? variableName { get; set; }
    public bool? caseSensitiveLookup { get; set; }
    public string? expression { get; set; }

}

[MyProto(Direction = ProtoDirection.ToServer)]
public class RpcInlineValue : JsonRpcBase<InlineValueParams, InlineValue>
{
    public override string m_method => "textDocument/inlineValue";

    public override void OnRequest()
    {
        throw new NotImplementedException();
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}

[MyProto(Direction = ProtoDirection.ToClient)]
public class RpcInlineValueRefresh : JsonRpcEmptyBase
{
    public override string m_method => "workspace/inlineValue/refresh";

    public override void OnRequest()
    {
        throw new NotImplementedException();
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}

