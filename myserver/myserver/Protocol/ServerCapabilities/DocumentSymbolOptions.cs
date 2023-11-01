using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class DocumentSymbolOptions: WorkDoneProgressOptions
{
    /// <summary>
    /// A human-readable string that is shown when multiple outlines trees are shown for the same document.
    /// </summary>
    public string? label { get; set; }
}
