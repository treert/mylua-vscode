using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyServer.Protocol.SemanticTokensClientCapabilities;

namespace MyServer.Protocol;
public class WorkspaceSymbolClientCapabilities
{
    public bool? dynamicRegistration { get; set; }
    /// <summary>
    /// Specific capabilities for the `SymbolKind` in the `workspace/symbol` request.
    /// </summary>
    public ValueSet<SymbolKind>? symbolKind { get; set; }
    /// <summary>
    /// The client supports tags on `SymbolInformation` and `WorkspaceSymbol`.
    /// Clients supporting tags have to handle unknown tags gracefully.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public ValueSet<SymbolTag>? tagSupport { get; set; }
    /// <summary>
    /// The client support partial workspace symbols.The client will send the
    /// request `workspaceSymbol/resolve` to the server to resolve additional properties.
    /// <br/>
    /// @since 3.17.0 - proposedState
    /// </summary>
    public Properties resolveSupport { get; set; }
}
