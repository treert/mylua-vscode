using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class DiagnosticOptions : WorkDoneProgressOptions
{
    /// <summary>
    /// An optional identifier under which the diagnostics are managed by the client.
    /// </summary>
    public string? identifier {  get; set; }
    /// <summary>
    /// Whether the language has inter file dependencies meaning that
    /// editing code in one file can result in a different diagnostic
    /// set in another file.Inter file dependencies are common for
    /// most programming languages and typically uncommon for linters.
    /// </summary>
    public bool interFileDependencies { get; set; }
    /// <summary>
    /// The server provides support for workspace diagnostics as well.
    /// </summary>
    public bool workspaceDiagnostics { get; set; }
}
