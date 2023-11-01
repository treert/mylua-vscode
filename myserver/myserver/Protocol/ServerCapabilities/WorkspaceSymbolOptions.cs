using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class WorkspaceSymbolOptions : WorkDoneProgressOptions
{
    /// <summary>
    /// The server provides support to resolve additional information for a workspace symbol.
    /// </summary>
    public bool? resolveProvider { get; set; }
}
