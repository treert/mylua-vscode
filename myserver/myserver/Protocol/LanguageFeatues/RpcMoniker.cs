using MyServer.Misc;
using MyServer.Protocol.BaseStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyServer.Protocol.LanguageFeatues;

public class MonikerArgs
{
    public ProgressToken? partialResultToken { get; set; }
    public ProgressToken? workDoneToken { get; set; }
    public TextDocId textDocument { get; set; }
    public Position position { get; set; }
}

[JsonConverter(typeof(MyJsonEnumConverter))]
public enum UniquenessLevel
{
    /// <summary>
    /// The moniker is only unique inside a document
    /// </summary>
    Document,
    /// <summary>
    /// The moniker is unique inside a project for which a dump got created
    /// </summary>
    Project,
    /// <summary>
    /// The moniker is unique inside the group to which a project belongs
    /// </summary>
    Group,
    /// <summary>
    /// The moniker is unique inside the moniker scheme.
    /// </summary>
    Scheme,
    /// <summary>
    /// The moniker is globally unique
    /// </summary>
    Global,
}

[JsonConverter(typeof(MyJsonEnumConverter))]
public enum MonikerKind
{
    /// <summary>
    /// The moniker represent a symbol that is imported into a project
    /// </summary>
    Import,
    /// <summary>
    /// The moniker represents a symbol that is exported from a project
    /// </summary>
    Export,
    /// <summary>
    /// The moniker represents a symbol that is local to a project (e.g. a local
    /// variable of a function, a class not visible outside the project, ...)
    /// </summary>
    Local,
}

/// <summary>
/// Moniker definition to match LSIF 0.5 moniker definition.
/// </summary>
public class Moniker
{
    /// <summary>
    /// The scheme of the moniker. For example tsc or .Net
    /// </summary>
    public string scheme { get; set; }
    /// <summary>
    /// The identifier of the moniker. The value is opaque in LSIF however
    /// schema owners are allowed to define the structure if they want.
    /// </summary>
    public string indetifier { get; set; }
    /// <summary>
    /// The scope in which the moniker is unique
    /// </summary>
    public UniquenessLevel unique {  get; set; }
    /// <summary>
    /// The moniker kind if known.
    /// </summary>
    public MonikerKind? kind { get; set; }
}

[MyProto(Direction = ProtoDirection.ToServer)]
public class RpcMoniker : JsonRpcBase<MonikerArgs, List<Moniker[]>>
{
    public override string m_method => "textDocument/moniker";

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
