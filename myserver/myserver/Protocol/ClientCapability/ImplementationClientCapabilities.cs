using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class ImplementationClientCapabilities
{
    /// <summary>
    /// Whether implementation supports dynamic registration.If this is set to
    /// `true` the client supports the new `ImplementationRegistrationOptions`
    /// return value for the corresponding server capability as well.
    /// </summary>
    public bool? dynamicRegistration { get; set; }
    /// <summary>
    /// The client supports additional metadata in the form of definition links.
    /// <br/>
    /// @since 3.14.0
    /// </summary>
    public bool? linkSupport { get; set; }
}
