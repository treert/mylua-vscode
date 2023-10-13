using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class DocumentSymbolClientCapabilities
{
    public bool? dynamicRegistration { get; set; }
    /// <summary>
    /// Specific capabilities for the `SymbolKind` in the `textDocument/documentSymbol` request.
    /// <br/>
    /// The symbol kind values the client supports. When this
    /// property exists the client also guarantees that it will
    /// handle values outside its set gracefully and falls back
    /// to a default value when unknown.
    /// <br/>
    /// If this property is not present the client only supports
    /// the symbol kinds from `File` to `Array` as defined in
    /// the initial version of the protocol.
    /// </summary>
    public ValueSet<SymbolKind>? symbolKind { get; set; }
    public bool? hierarchicalDocumentSymbolSupport { get; set; }
    /// <summary>
    /// The client supports tags on `SymbolInformation`. Tags are supported on
    /// `DocumentSymbol` if `hierarchicalDocumentSymbolSupport` is set to true.
    /// Clients supporting tags have to handle unknown tags gracefully.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public ValueSet<SymbolTag>? tagSupport { get; set; }
    /// <summary>
    /// The client supports an additional label presented in the UI when
    /// registering a document symbol provider.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public bool? labelSupport { get; set; }
}
