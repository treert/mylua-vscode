using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class DocumentOnTypeFormattingOptions
{
    /// <summary>
    /// A character on which formatting should be triggered, like `{`.
    /// </summary>
    public string firstTriggerCharacter { get; set; }
    /// <summary>
    /// More trigger characters.
    /// </summary>
    public string[]? moreTriggerCharacter { get; set; }
}
