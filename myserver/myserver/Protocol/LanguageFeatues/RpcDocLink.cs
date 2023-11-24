
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyServer.Protocol;

public class DocIdAndTokenParams : IWorkDoneProgress, IPartialResult
{
    public ProgressToken? partialResultToken { get; set; }
    public ProgressToken? workDoneToken { get; set; }
    public TextDocId textDocument { get; set; }
}

/// <summary>
/// A document link is a range in a text document that links to an internal or
/// external resource, like another text document or a web site.
/// </summary>
public class DocumentLink
{
    /// <summary>
    /// The range this link applies to.
    /// </summary>
    public Range range { get; set; }
    /// <summary>
    /// The uri this link points to. If missing a resolve request is sent later.
    /// </summary>
    public Uri? target { get; set; }
    /// <summary>
    /// The tooltip text when you hover over this link.
    /// 
    /// If a tooltip is provided, is will be displayed in a string that includes
    /// instructions on how to trigger the link, such as `{0} (ctrl + click)`.
    /// The specific instructions vary depending on OS, user settings, and
    /// localization.
    /// 
    /// @since 3.15.0
    /// </summary>
    public string? tooltip { get; set; }
    /// <summary>
    /// A data entry field that is preserved on a document link between a
    /// DocumentLinkRequest and a DocumentLinkResolveRequest.
    /// </summary>
    public JsonNode? data { get; set; }
}

public class RpcDocLink : JsonRpcBase<DocIdAndTokenParams, List<DocumentLink>>
{
    public override string m_method => "textDocument/documentLink";

    public override void OnRequest()
    {
        // todo
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}
