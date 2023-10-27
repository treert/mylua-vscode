using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class SignatureHelpOptions : WorkDoneProgressOptions
{
    /// <summary>
    /// The characters that trigger signature help automatically.
    /// </summary>
    public string[]? triggerCharacters { get; set; }
    /// <summary>
    /// List of characters that re-trigger signature help.
    /// <br/>
    /// These trigger characters are only active when signature help is already
    /// showing.All trigger characters are also counted as re-trigger
    /// characters.
    /// </summary>
    public string[]? retriggerCharacters { get; set;}
}
