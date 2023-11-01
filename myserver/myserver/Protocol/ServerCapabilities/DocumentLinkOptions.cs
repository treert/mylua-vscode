using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class DocumentLinkOptions : WorkDoneProgressOptions
{
    /// <summary>
    /// Document links have a resolve provider as well.
    /// </summary>
    public bool? resolveProvider { get; set; }
}
