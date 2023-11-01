using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class CodeLensOptions:WorkDoneProgressOptions
{
    /// <summary>
    /// Code lens has a resolve provider as well.
    /// </summary>
    public bool? resolveProvider { get; set; }
}
