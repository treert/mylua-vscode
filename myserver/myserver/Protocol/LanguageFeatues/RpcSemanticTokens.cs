
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class SemanticTokensEdit{
    /// <summary>
    /// The start offset of the edit.
    /// </summary>
    public uint start { get; set; }
    /// <summary>
    /// The count of elements to remove.
    /// </summary>
    public uint deleteCount { get; set; }
    /// <summary>
    /// The elements to insert.
    /// </summary>
    public uint[]? data { get; set; }
}

/// <summary>
/// 混合了 SemanticTokens | SemanticTokensPartial | SemanticTokensDelta | SemanticTokensDeltaPartial
/// </summary>
public class SemanticTokensResult
{
    /// <summary>
    /// An optional result id.If provided and clients support delta updating
    /// the client will include the result id in the next semantic token request.
    /// A server can then instead of computing all semantic tokens again simply
    /// send a delta.
    /// </summary>
    public string? resultId { get; set; }
    /// <summary>
    /// The actual tokens.
    /// </summary>
    public uint[]? data { get; set; }

    /// <summary>
    /// The semantic token edits to transform a previous result into a new result.
    /// </summary>
    public SemanticTokensEdit[]? edits { get; set; }
}

public class RpcSemanticTokensFull : JsonRpcBase<DocIdAndTokenParams, SemanticTokensResult>
{
    public override string m_method => "textDocument/semanticTokens/full";

    public override void OnRequest()
    {
        // todo
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}

public class SemanticTokensDeltaParams : DocIdAndTokenParams
{
    /// <summary>
    /// The result id of a previous response. The result Id can either point to
    /// a full response or a delta response depending on what was received last.
    /// </summary>
    public string previousResultId { get; set; }
}


public class RpcSemanticTokensFullDelta : JsonRpcBase<SemanticTokensDeltaParams, SemanticTokensResult>
{
    public override string m_method => "textDocument/semanticTokens/full/delta";

    public override void OnRequest()
    {
        // todo
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}

public class SemanticTokensRangeParams : DocIdAndTokenParams
{
    public Range range { get; set; }
}

public class RpcSemanticTokensRange : JsonRpcBase<SemanticTokensRangeParams, SemanticTokensResult>
{
    public override string m_method => "textDocument/semanticTokens/range";

    public override void OnRequest()
    {
        // todo
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}

public class RpcSemanticTokensRefresh : JsonRpcBase<Dummy, Dummy>
{
    public override string m_method => "workspace/semanticTokens/refresh";

    public override void OnRequest()
    {
        throw new NotImplementedException();
    }

    protected override void OnSuccess()
    {
        // todo
    }
}

