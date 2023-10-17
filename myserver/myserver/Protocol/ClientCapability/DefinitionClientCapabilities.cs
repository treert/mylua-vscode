using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class DefinitionClientCapabilities
{
    public bool? dynamicRegistration { get; set; }
    /// <summary>
    /// The client supports additional metadata in the form of definition links.
    /// </summary>
    public bool? linkSupport { get; set; }
}
