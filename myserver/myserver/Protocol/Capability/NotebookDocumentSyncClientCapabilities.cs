using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class NotebookDocumentSyncClientCapabilities
{
    /// <summary>
    // Whether implementation supports dynamic registration.If this is
    // set to `true` the client supports the new
    // `(TextDocumentRegistrationOptions & StaticRegistrationOptions)`
    // return value for the corresponding server capability as well.
    /// </summary>
    public bool? dynamicRegistration { get; set; }
    /// <summary>
    /// The client supports sending execution summary data per cell.
    /// </summary>
    public bool? executionSummarySupport { get; set; }
}
