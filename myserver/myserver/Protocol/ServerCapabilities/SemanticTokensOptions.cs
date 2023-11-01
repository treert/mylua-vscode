using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class SemanticTokensOptions : WorkDoneProgressOptions
{
    /// <summary>
    /// The legend used by the server
    /// </summary>
    public SemanticTokensLegend legend { get; set; }
    /// <summary>
    /// Server supports providing semantic tokens for a specific range of a document.
    /// </summary>
    public bool? range { get; set; }
    public class _Full
    {
        /// <summary>
        /// The server supports deltas for full documents.
        /// </summary>
        public bool? delta { get; set; }
    }
    /// <summary>
    /// Server supports providing semantic tokens for a full document.
    /// </summary>
    public _Full? full { get; set; }
}
