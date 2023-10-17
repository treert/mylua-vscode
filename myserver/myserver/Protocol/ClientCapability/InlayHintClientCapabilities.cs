using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class InlayHintClientCapabilities
{
    public bool? dynamicRegistration { get; set; }
    /// <summary>
    /// Indicates which properties a client can resolve lazily on an inlay hint.
    /// </summary>
    public Properties? resolveSupport { get; set; }
}
