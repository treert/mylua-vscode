using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace MyServer.Protocol;
public class CodeActionClientCapabilities
{
    public bool? dynamicRegistration { get; set; }
    public class _codeActionLiteralSupport
    {
        public ValueSet<CodeActionKind> codeActionKind;
    }
    /// <summary>
    /// The client supports code action literals as a valid
    /// response of the `textDocument/codeAction` request.
    /// <br/>
    /// @since 3.8.0
    /// </summary>
    public _codeActionLiteralSupport? codeActionLiteralSupport { get;set; }
    public bool? isPreferredSupport { get; set; }
    public bool? disabledSupport { get; set; }
    /// <summary>
    /// Whether code action supports the `data` property which is
    /// preserved between a `textDocument/codeAction` and a
    /// `codeAction/resolve` request.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public bool? dataSupport { get; set; }
    /// <summary>
    /// Whether the client supports resolving additional code action
    /// properties via a separate `codeAction/resolve` request.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public Properties? resolveSupport { get; set; }
    /// <summary>
    /// Whether the client honors the change annotations in
    /// text edits and resource operations returned via the
    /// `CodeAction#edit` property by for example presenting
    /// the workspace edit in the user interface and asking
    /// for confirmation.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public bool? honorsChangeAnnotations { get; set; }
}

/// <summary>
/// todo@xx
/// The kind of a code action.
/// <br/>
/// Kinds are a hierarchical list of identifiers separated by `.`,
/// e.g. `"refactor.extract.function"`.
/// <br/>
/// The set of kinds is open and client needs to announce the kinds it supports
/// to the server during initialization.
/// </summary>
public class CodeActionKind
{
}

