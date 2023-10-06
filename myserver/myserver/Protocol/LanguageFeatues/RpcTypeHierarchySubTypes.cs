using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class RpcTypeHierarchySubTypes : JsonRpcBase<TypeHierarchySupertypesParams, List<TypeHierarchyItem>>
{
    public override string m_method => "typeHierarchy/subtypes";

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
