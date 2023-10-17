using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class DeclarationClientCapabilities
{
    /// <summary>
    /// Whether declaration supports dynamic registration.If this is set to
    /// `true` the client supports the new `DeclarationRegistrationOptions`
    /// return value for the corresponding server capability as well.
    /// </summary>
    public bool? dynamicRegistration { get; set; }
    /// <summary>
    /// The client supports additional metadata in the form of declaration links.
    /// </summary>
    public bool? linkSupport { get; set; }
}
