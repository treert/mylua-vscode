using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

/// <summary>
/// Represents a location inside a resource, such as a line inside a text file.
/// </summary>
public class Location
{
    public Uri uri { get; set; }
    public Range range { get; set; }
}

/// <summary>
/// 对比 Location 可以忽略range
/// </summary>
public class LocationOptRange
{
    public Uri uri { get; set; }
    public Range? range { get; set; }
}

/// <summary>
/// Represents a link between a source and a target location.
/// </summary>
public class LocationLink
{
    /// <summary>
    /// Span of the origin of this link.
    /// <br/>
    /// Used as the underlined span for mouse interaction. Defaults to the word
    /// range at the mouse position.
    /// </summary>
    public Range? originSelectionRange { get; set; }
    /// <summary>
    /// The target resource identifier of this link.
    /// </summary>
    public Uri targetUri { get; set; }
    /// <summary>
    /// The full target range of this link. If the target for example is a symbol
    /// then target range is the range enclosing this symbol not including
    /// leading/trailing whitespace but everything else like comments. This
    /// information is typically used to highlight the range in the editor.
    /// </summary>
    public Range targetRange { get; set; }
    /// <summary>
    /// The range that should be selected and revealed when this link is being
    /// followed, e.g the name of a function.Must be contained by the
    /// `targetRange`. See also `DocumentSymbol#range`
    /// </summary>
    public Range targetSelectionRange { get; set; }
}

