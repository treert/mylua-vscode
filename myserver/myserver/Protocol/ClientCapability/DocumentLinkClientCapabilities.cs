using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class DocumentLinkClientCapabilities
{
    public bool? dynamicRegistration { get; set; }
    public bool? tooltipSupport { get; set; }
}
