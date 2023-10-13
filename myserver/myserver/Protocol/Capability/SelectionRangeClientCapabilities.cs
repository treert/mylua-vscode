using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class SelectionRangeClientCapabilities
{
    /// <summary>
    /// Whether implementation supports dynamic registration for selection range
    /// providers.If this is set to `true` the client supports the new
    /// `SelectionRangeRegistrationOptions` return value for the corresponding
    /// server capability as well.
    /// </summary>
    public bool? dynamicRegistration { get; set; }
}
