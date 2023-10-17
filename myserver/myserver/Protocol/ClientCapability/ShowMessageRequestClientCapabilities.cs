using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class ShowMessageRequestClientCapabilities
{
    public class _messageActionItem
    {
        /// <summary>
        /// Whether the client supports additional attributes which are preserved
        /// and sent back to the server in the request's response.
        /// </summary>
        public bool? additionalPropertiesSupport { get; set; }
    }
    /// <summary>
    /// Capabilities specific to the `MessageActionItem` type.
    /// </summary>
    public _messageActionItem? messageActionItem { get; set; }
}
