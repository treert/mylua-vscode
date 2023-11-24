
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

/// <summary>
/// Represents programming constructs like variables, classes, interfaces etc.
/// that appear in a document.Document symbols can be hierarchical and they
/// have two ranges: one that encloses its definition and one that points to its
/// most interesting range, e.g.the range of an identifier.
/// </summary>
public class DocumentSymbol
{
    /// <summary>
    /// The name of this symbol.Will be displayed in the user interface and
    /// therefore must not be an empty string or a string only consisting of
    /// white spaces.
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// More detail for this symbol, e.g the signature of a function.
    /// </summary>
    public string? detail { get; set; }
    public SymbolKind kind { get; set; }
    public SymbolTag[]? tags { get; set; }
    public Range Range { get; set; }
    public Range selectionRange { get; set; }
    /// <summary>
    /// Children of this symbol, e.g. properties of a class.
    /// </summary>
    public DocumentSymbol[]? children { get; set; }
}

[MyProto(Direction = ProtoDirection.ToServer)]
public class RpcDocSymbols : JsonRpcBase<DocIdAndTokenParams, List<DocumentSymbol>>
{
    public override string m_method => "textDocument/documentSymbol";

    public override void OnRequest()
    {
        var path = ReqArgs.textDocument.uri.ToLocalFilePath();
        ResData.Clear();
        SendResponse();
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}
