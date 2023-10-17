using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class CodeLensWorkspaceClientCapabilities
{
    /// <summary>
    /// Whether the client implementation supports a refresh request sent from the
    /// server to the client.
    /// <br/>
    /// Note that this event is global and will force the client to refresh all
    /// code lenses currently shown. It should be used with absolute care and is
    /// useful for situation where a server for example detect a project wide
    /// change that requires such a calculation.
    /// </summary>
    public bool? refreshSupport { get; set; }
}
