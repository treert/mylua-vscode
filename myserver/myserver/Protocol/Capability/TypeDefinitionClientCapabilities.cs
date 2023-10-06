using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class TypeDefinitionClientCapabilities
{
    public bool? dynamicRegistration { get; set; }
    public bool? linkSupport { get; set; }
}
