using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyServer.Protocol;
/// <summary>
/// Options specific to a notebook plus its cells
/// to be synced to the server.
/// <br/>
/// If a selector provides a notebook document
/// filter but no cell selector all cells of a
/// matching notebook document will be synced.
/// <br/>
/// If a selector provides no notebook document
/// filter but only a cell selector all notebook
/// documents that contain at least one matching
/// cell will be synced.
/// <br/>
/// @since 3.17.0
/// </summary>
public class NotebookDocumentSyncOptions
{
    public class _NotebookSelector
    {
        /// <summary>
        /// The notebook to be synced.
        /// </summary>
        public NotebookDocumentFilter? notebook { get; set; }
        public class _Cell
        {
            public string language { get; set; }
        }
        /// <summary>
        /// The cells of the matching notebook to be synced.
        /// </summary>
        public _Cell[]? cells { get; set; }
    }
    /// <summary>
    /// The notebooks to be synced
    /// </summary>
    public List<_NotebookSelector> notebookSelector { get; set; } = [];
    /// <summary>
    /// Whether save notification should be forwarded to the server.
    /// Will only be honored if mode === `notebook`.
    /// </summary>
    public bool? save { get; set; }
}
