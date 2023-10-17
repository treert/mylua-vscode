using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class NotebookDocumentClientCapabilities
{
    /// <summary>
    /// Capabilities specific to notebook document synchronization
    /// <br/>
    /// @since 3.17.0
    /// </summary>
    public NotebookDocumentSyncClientCapabilities synchronization { get; set; }
}
