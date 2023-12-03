using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class ApplyWorkspaceEditParams
{
    /// <summary>
    /// An optional label of the workspace edit.This label is
    /// presented in the user interface for example on an undo
    /// stack to undo the workspace edit.
    /// </summary>
    public string label { get; set;}
    public WorkspaceEdit edit { get; set;}
}

public class ApplyWorkspaceEditResult
{
    /// <summary>
    /// Indicates whether the edit was applied or not.
    /// </summary>
    public bool applied { get; set; }
    /// <summary>
    /// An optional textual description for why the edit was not applied.
    /// This may be used by the server for diagnostic logging or to provide
    /// a suitable error for a request that triggered the edit.
    /// </summary>
    public string? failureReason { get; set; }
    /// <summary>
    /// Depending on the client's failure handling strategy `failedChange`
    /// might contain the index of the change that failed.This property is
    /// only available if the client signals a `failureHandling` strategy
    /// in its client capabilities.
    /// </summary>
    public uint? failedChange { get; set; }
}

[MyProto(Direction = ProtoDirection.ToClient)]
public class RpcApplyEdit : JsonRpcBase<ApplyWorkspaceEditParams, ApplyWorkspaceEditResult>
{
    public override string m_method => "workspace/applyEdit";

    public override void OnRequest()
    {
        throw new NotImplementedException();
    }

    protected override void OnSuccess()
    {
        // todo log something
    }
}
