using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class RenameOptions : WorkDoneProgressOptions
{
    /// <summary>
    /// Renames should be checked and tested before being executed.
    /// </summary>
    public bool? prepareProvider { get; set; }
}
