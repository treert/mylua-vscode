using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;
/// <summary>
/// Represents a reference to a command.
/// Provides a title which will be used to represent a command in the UI.
/// Commands are identified by a string identifier.
/// The recommended way to handle commands is to implement their execution
/// on the server side if the client and server provides the corresponding capabilities.
/// Alternatively the tool extension code could handle the command.
/// The protocol currently doesn’t specify a set of well-known commands.
/// </summary>
public class Command
{
    /// <summary>
    /// Title of the command, like `save`.
    /// </summary>
    public string title { get; set; }
    /// <summary>
    /// The identifier of the actual command handler.
    /// </summary>
    public string command { get; set; }
    /// <summary>
    /// Arguments that the command handler should be invoked with.
    /// </summary>
    public JsonArray? arguments { get; set; }
}
