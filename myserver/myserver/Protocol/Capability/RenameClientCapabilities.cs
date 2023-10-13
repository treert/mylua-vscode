using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace MyServer.Protocol;
public enum PrepareSupportDefaultBehavior
{
    /// <summary>
    /// The client's default behavior is to select the identifier
    /// according to the language's syntax rule.
    /// </summary>
    Identifier = 1,
}

public class RenameClientCapabilities
{
    public bool? dynamicRegistration { get; set; }
    /// <summary>
    /// Client supports testing for validity of rename operations
    /// before execution.
    /// <br/>
    /// @since version 3.12.0
    /// </summary>
    public bool? prepareSupport { get; set; }
    /// <summary>
    /// Client supports the default behavior result (`{ defaultBehavior: boolean}`).
    /// <br/>
    /// The value indicates the default behavior used by the client.
    /// <br/>
    /// @since version 3.16.0
    /// </summary>
    public PrepareSupportDefaultBehavior? prepareSupportDefaultBehavior { get; set; }
    /// <summary>
    /// Whether the client honors the change annotations in
    /// text edits and resource operations returned via the
    /// rename request's workspace edit by for example presenting
    /// the workspace edit in the user interface and asking
    /// for confirmation.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public bool? honorsChangeAnnotations { get; set; }
}
