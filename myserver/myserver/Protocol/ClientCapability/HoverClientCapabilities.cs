using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class HoverClientCapabilities
{
    public bool? dynamicRegistration {  get; set; }
    /// <summary>
    /// Client supports the follow content formats if the content
    /// property refers to a `literal of type MarkupContent`.
    /// The order describes the preferred format of the client.
    /// </summary>
    public MarkupKind[]? contentFormat { get; set; }
}
