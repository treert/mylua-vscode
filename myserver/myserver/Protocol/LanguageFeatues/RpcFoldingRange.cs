﻿
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyServer.Protocol;

[JsonConverter(typeof(MyJsonEnumConverter))]
public enum FoldingRangeKind
{
    Comment,
    Imports,
    Region,
}

public class FoldingRange
{
    public uint startLine {  get; set; }
    public uint? startCharacter { get; set; }
    public uint endLine { get; set;}
    public uint? endCharacter { get; set;}
    public FoldingRangeKind? kind { get; set; }
    /// <summary>
    /// The text that the client should show when the specified range is
    /// collapsed.If not defined or not supported by the client, a default
    /// will be chosen by the client.
    /// 
    /// @since 3.17.0 - proposed
    /// </summary>
    public string? collapsedText { get; set; }
}

public class RpcFoldingRange : JsonRpcBase<DocIdAndTokenParams, List<FoldingRange>>
{
    public override string m_method => "textDocument/foldingRange";

    public override void OnRequest()
    {
        // todo
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}
