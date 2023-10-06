
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

/// <summary>
/// 和 CallHierarchyItem 一样
/// </summary>
public class TypeHierarchyItem
{
    public string name { get; set; }
    public SymbolKind kind { get; set; }
    /// <summary>
    /// 现在只有一个 deprecated
    /// </summary>
    public SymbolTag[]? tags { get; set; }
    public string? detail { get; set; }
    public Uri uri { get; set; }
    /// <summary>
    /// The range enclosing this symbol not including leading/trailing whitespace
    /// but everything else, e.g. comments and code.
    /// </summary>
    public Range range { get; set; }
    /// <summary>
    /// The range that should be selected and revealed when this symbol is being
    /// picked, e.g.the name of a function. Must be contained by the
    /// [`range`](#TypeHierarchyItem.range).
    /// </summary>
    public Range selectionRange { get; set; }
    public JsonNode? data { get; set; }
}

public class RpcPrepareTypeHierarchy : JsonRpcBase<CallHierarchyPrepareParams, List<TypeHierarchyItem>>
{
    public override string m_method => "textDocument/prepareTypeHierarchy";

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

