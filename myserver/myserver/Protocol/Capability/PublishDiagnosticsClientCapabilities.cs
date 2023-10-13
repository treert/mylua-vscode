using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MyServer.Protocol;
public class PublishDiagnosticsClientCapabilities
{
    /// <summary>
    ///  Whether the clients accepts diagnostics with related information.
    /// </summary>
    public bool? relatedInformation { get; set; }
    /// <summary>
    /// Client supports the tag property to provide meta data about a diagnostic.
    /// Clients supporting tags have to handle unknown tags gracefully.
    /// </summary>
    public ValueSet<DiagnosticTag> tagSupport { get; set; }
    /// <summary>
    /// Whether the client interprets the version property of the
    /// `textDocument/publishDiagnostics` notification's parameter.
    /// <br/>
    /// @since 3.15.0
    /// </summary>
    public bool? versionSupport { get; set; }
    /// <summary>
    /// Client supports a codeDescription property
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public bool? codeDescriptionSupport { get; set; }
    /// <summary>
    /// Whether code action supports the `data` property which is
    /// preserved between a `textDocument/publishDiagnostics` and
    /// `textDocument/codeAction` request.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public bool? dataSupport { get; set; }
}
