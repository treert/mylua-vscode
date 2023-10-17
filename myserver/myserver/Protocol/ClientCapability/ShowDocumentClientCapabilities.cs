using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class ShowDocumentClientCapabilities
{
    /// <summary>
    /// The client has support for the show document request.
    /// </summary>
    public bool support { get; set; }
}
