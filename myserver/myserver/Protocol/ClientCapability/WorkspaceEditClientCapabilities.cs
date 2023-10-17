using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyServer.Protocol;

[JsonConverter(typeof(MyJsonEnumConverter))]
public enum ResourceOperationKind
{
    Create,
    Rename,
    Delete,
}

[JsonConverter(typeof(MyJsonEnumConverter))]
public enum FailureHandlingKind
{
    /// <summary>
    /// Applying the workspace change is simply aborted if one of the changes provided fails.
    /// All operations executed before the failing operation stay executed.
    /// </summary>
    Abort,
    /// <summary>
    /// All operations are executed transactional. That means they either all succeed or no changes at all are applied to the workspace.
    /// </summary>
    Transactional,
    /// <summary>
    /// If the workspace edit contains only textual file changes they are executed transactional.
    /// If resource changes (create, rename or delete file) are part of the change the failure handling strategy is abort.
    /// </summary>
    TextOnlyTransactional,
    /// <summary>
    /// The client tries to undo the operations already executed.
    /// But there is no guarantee that this is succeeding.
    /// </summary>
    Undo,
}

public class WorkspaceEditClientCapabilities
{
    /// <summary>
    /// The client supports versioned document changes in `WorkspaceEdit`s
    /// </summary>
    public bool? documentChanges { get; set; }
    /// <summary>
    /// The resource operations the client supports.Clients should at least
    /// support 'create', 'rename' and 'delete' files and folders.
    /// <br/>
    /// @since 3.13.0
    /// </summary>
    public ResourceOperationKind[]? resourceOperations { get; set; }
    /// <summary>
    /// The failure handling strategy of a client if applying the workspace edit fails.
    /// <br/>
    /// @since 3.13.0
    /// </summary>
    public FailureHandlingKind? failureHandling { get; set; }
    /// <summary>
    /// Whether the client normalizes line endings to the client specific setting.
    /// If set to `true` the client will normalize line ending characters
    /// in a workspace edit to the client specific new line character(s).
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public bool? normalizesLineEndings { get; set; }
    public class _ChangeAnnotationSupport
    {
        /// <summary>
        /// Whether the client groups edits with equal labels into tree nodes,
        /// for instance all edits labelled with "Changes in Strings" would
        /// be a tree node.
        /// </summary>
        public bool groupsOnLabel { get; set; }
    }
    /// <summary>
    /// Whether the client in general supports change annotations on text edits,
    /// create file, rename file and delete file changes.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public _ChangeAnnotationSupport changeAnnotationSupport { get; set; }
}
