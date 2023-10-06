
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class TypeHierarchySupertypesParams : IWorkDoneProgress, IPartialResult
{
    public ProgressToken? partialResultToken { get; set; }
    public ProgressToken? workDoneToken { get; set; }

    public TypeHierarchyItem item { get; set; }
}

public class RpcTypeHierarchySuperTypes : JsonRpcBase<TypeHierarchySupertypesParams, List<TypeHierarchyItem>>
{
    public override string m_method => "typeHierarchy/supertypes";

    public override void OnCanceled()
    {
        // todo
    }

    public override void OnRequest()
    {
        // todo
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}

