using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class CodeActionOptions : WorkDoneProgressOptions
{
    /// <summary>
    /// CodeActionKinds that this server may return.
    /// <br/>
    /// The list of kinds may be generic, such as `CodeActionKind.Refactor`,
    /// or the server may list out every specific kind they provide.
    /// </summary>
    public CodeActionKind[]? codeActionKinds { get; set; }
    /// <summary>
    /// The server provides support to resolve additional information for a code action.
    /// </summary>
    public bool? resolveProvider { get; set; }
}
