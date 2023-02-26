using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyServer.Protocol;

internal class InitializeParams
{
    /**
         * The process Id of the parent process that started the server. Is null if
         * the process has not been started by another process. If the parent
         * process is not alive then the server should exit (see exit notification)
         * its process.
         */
    public long? processId { get; set; }

    public class ClientInfo
    {
        public string name { get; set; }
        public string version { get; set; }
    };
    public ClientInfo clientInfo { get; set; }
    /**
         * The locale the client is currently showing the user interface
         * in. This must not necessarily be the locale of the operating
         * system.
         *
         * Uses IETF language tags as the value's syntax
         * (See https://en.wikipedia.org/wiki/IETF_language_tag)
         *
         * @since 3.16.0
         */
    public string? locale { get; set; }

    /// <summary>
    /// The rootUri of the workspace. Is null if no folder is open.
    /// If both `rootPath` and `rootUri` are set `rootUri` wins.
    /// 
    /// @deprecated in favour of `workspaceFolders`
    /// </summary>
    public Uri rootUri { get; set; }

    /// <summary>
    /// User provided initialization options.
    /// </summary>\
    //object initializationOptions { get; init; }

    ///// <summary>
    ///// The capabilities provided by the client (editor or tool)
    ///// </summary>
    //ClientCapabilities capabilities { get; init; }
}