using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class DidChangeWatchedFilesClientCapabilities
{
    /// <summary>
    /// Did change watched files notification supports dynamic registration.
    /// Please note that the current protocol doesn't support static
    /// configuration for file changes from the server side.
    /// </summary>
    public bool? dynamicRegistration { get; set; }
    /// <summary>
    /// Whether the client has support for relative patterns or not.
    /// <br/>
    /// @since 3.17.0
    /// </summary>
    public bool? relativePatternSupport { get; set; }
}
