using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class SignatureHelpClientCapabilities
{
    public bool? dynamicRegistration { get; set; }
    public class _SignatureInformation
    {
        /// <summary>
        /// Client supports the follow content formats for the documentation
        /// property.The order describes the preferred format of the client.
        /// </summary>
        public MarkupKind[]? documentationFormat { get; set; }
        public class _parameterInformation
        {
            /// <summary>
            /// The client supports processing label offsets instead of a
            /// simple label string.
            /// <br/>
            /// @since 3.14.0
            /// </summary>
            public bool? labelOffsetSupport { get; set; }
        }
        /// <summary>
        /// Client capabilities specific to parameter information.
        /// </summary>
        public _parameterInformation? parameterInformation { get; set; }
        /// <summary>
        /// The client supports the `activeParameter` property on
        /// `SignatureInformation` literal.
        /// <br/>
        /// @since 3.16.0
        /// </summary>
        public bool? activeParameterSupport {  get; set; }
    }
    /// <summary>
    /// The client supports the following `SignatureInformation` specific properties.
    /// </summary>
    public _SignatureInformation? signatureInformation { get; set; }
    /// <summary>
    /// The client supports to send additional context information for a
    /// `textDocument/signatureHelp` request.A client that opts into
    /// contextSupport will also support the `retriggerCharacters` on
    /// `SignatureHelpOptions`.
    /// <br/>
    /// @since 3.15.0
    /// </summary>
    public bool? contextSupport { get; set; }
}
