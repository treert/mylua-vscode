
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyServer.Protocol.LanguageFeatues;

/// <summary>
/// A document highlight kind.
/// </summary>
public enum DocumentHighlightKind
{
    /// <summary>
    /// A textual occurrence.
    /// </summary>
    Text = 1,
    /// <summary>
    /// Read-access of a symbol, like reading a variable.
    /// </summary>
    Read = 2,
    /// <summary>
    /// Write-access of a symbol, like writing to a variable.
    /// </summary>
    Write = 3,
}

public class DocumentHighlight
{
    public Range range { get; set; }
    public DocumentHighlightKind? kind { get; set; }
}

public class RpcDocHighlight : JsonRpcBase<PosAndTokenParams, List<DocumentHighlight>>
{
    public override string m_method => "textDocument/documentHighlight";

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
