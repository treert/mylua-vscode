using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyServer.Protocol;
public class FoldingRangeClientCapabilities
{
    /// <summary>
    /// Whether implementation supports dynamic registration for folding range
    /// providers.If this is set to `true` the client supports the new
    /// `FoldingRangeRegistrationOptions` return value for the corresponding
    /// server capability as well.
    /// </summary>
    public bool? dynamicRegistration { get; set; }
    /// <summary>
    /// The maximum number of folding ranges that the client prefers to receive
    /// per document.The value serves as a hint, servers are free to follow the
    /// limit.
    /// </summary>
    public uint? rangeLimit { get; set; }
    /// <summary>
    /// If set, the client signals that it only supports folding complete lines.
    /// If set, client will ignore specified `startCharacter` and `endCharacter`
    /// properties in a FoldingRange.
    /// </summary>
    public bool? lineFoldingOnly { get; set; }
    /// <summary>
    /// Specific options for the folding range kind.
    /// <br/>
    /// @since 3.17.0
    /// </summary>
    public ValueSet<FoldingRangeKind>? foldingRangeKind { get; set; }
    public class _FoldingRange
    {
        /// <summary>
        /// If set, the client signals that it supports setting collapsedText on
        /// folding ranges to display custom labels instead of the default text.
        /// <br/>
        /// @since 3.17.0
        /// </summary>
        public bool? collapsedText { get; set; }
    }
    /// <summary>
    /// Specific options for the folding range.
    /// @since 3.17.0
    /// </summary>
    public _FoldingRange? foldingRange { get; set; }
}
