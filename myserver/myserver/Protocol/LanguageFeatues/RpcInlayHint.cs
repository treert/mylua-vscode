using MyServer.Misc;
using MyServer.Protocol.BaseStruct;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class InlayHintParams : DocIdAndTokenParams
{
    public Range range {  get; set; }
}

public enum InlayHintKind
{
    Type = 1,
    Parameter = 2,
}

/// <summary>
/// An inlay hint label part allows for interactive and composite labels of inlay hints.
/// </summary>
public class InlayHintLabelPart
{
    /// <summary>
    /// The value of this label part.
    /// </summary>
    public string value;
    /// <summary>
    /// The tooltip text when you hover over this label part. Depending on
    /// the client capability `inlayHint.resolveSupport` clients might resolve
    /// this property late using the resolve request.
    /// </summary>
    public MarkupContent? content;
    /// <summary>
    /// An optional source code location that represents this
    /// label part.
    /// <br/>
    /// The editor will use this location for the hover and for code navigation
    /// features: This part will become a clickable link that resolves to the
    /// definition of the symbol at the given location (not necessarily the
    /// location itself), it shows the hover that shows at the given location,
    /// and it shows a context menu with further code navigation commands.
    /// <br/>
    /// Depending on the client capability `inlayHint.resolveSupport` clients
    /// might resolve this property late using the resolve request.
    /// </summary>
    public Location? location;
    /// <summary>
    /// An optional command for this label part.
    /// <br/>
    /// Depending on the client capability `inlayHint.resolveSupport` clients
    /// might resolve this property late using the resolve request.
    /// </summary>
    public Command? command;
}

public class InlayHint
{
    public Position position {  get; set; }
    /// <summary>
    /// The label of this hint.
    /// lsp 是支持裸的字符串的。我不支持。
    /// </summary>
    public InlayHintLabelPart[] label { get; set; }
    /// <summary>
    /// The kind of this hint. Can be omitted in which case the client
    /// should fall back to a reasonable default.
    /// </summary>
    public InlayHintKind? kind { get; set; }
    /// <summary>
    /// Optional text edits that are performed when accepting this inlay hint.
    /// <br/>
    /// *Note* that edits are expected to change the document so that the inlay
    /// hint (or its nearest variant) is now part of the document and the inlay
    /// hint itself is now obsolete.
    /// <br/>
    /// Depending on the client capability `inlayHint.resolveSupport` clients
    /// might resolve this property late using the resolve request.
    /// </summary>
    public TextEdit[]? textEdits { get; set; }
    /// <summary>
    /// The tooltip text when you hover over this item.
    /// <br/>
    /// Depending on the client capability `inlayHint.resolveSupport` clients
    /// might resolve this property late using the resolve request.
    /// </summary>
    public MarkupContent? tooptip {  get; set; }
    /// <summary>
    /// Render padding before the hint.
    /// <br/>
    /// Note: Padding should use the editor's background color, not the
    /// background color of the hint itself.That means padding can be used
    /// to visually align/separate an inlay hint.
    /// </summary>
    public bool? paddingLeft { get; set; }
    /// <summary>
    /// Render padding after  the hint.
    /// </summary>
    public bool? paddingRight { get; set; }
    /// <summary>
    /// A data entry field that is preserved on an inlay hint between
    /// a `textDocument/inlayHint` and a `inlayHint/resolve` request.
    /// </summary>
    public JsonNode? data { get; set; }
}

[MyProto(Direction = ProtoDirection.ToServer)]
public class RpcInlayHint : JsonRpcBase<InlayHintParams, List<InlayHint>>
{
    public override string m_method => "textDocument/inlayHint";

    public override void OnCanceled()
    {
        throw new NotImplementedException();
    }

    public override void OnRequest()
    {
        throw new NotImplementedException();
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}
