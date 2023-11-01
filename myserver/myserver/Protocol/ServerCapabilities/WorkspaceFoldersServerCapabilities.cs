using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class WorkspaceFoldersServerCapabilities
{
    /// <summary>
    /// The server has support for workspace folders
    /// </summary>
    public bool? supported { get; set; }
    /// <summary>
    /// Whether the server wants to receive workspace folder change notifications.
    /// </summary>
    public bool? changeNotifications { get; set; }
}
