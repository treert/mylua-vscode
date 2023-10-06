
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol.LanguageFeatues;

public class ReferenceContext
{
    /// <summary>
    /// Include the declaration of the current symbol.
    /// </summary>
    public bool includeDeclaration { get; set; }
}

public class FindReferenceParams : PosAndTokenParams
{
    public ReferenceContext context { get; set; } = new ReferenceContext();
}

public class RpcFindReference : JsonRpcBase<FindReferenceParams, List<Location>>
{
    public override string m_method => "textDocument/references";

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
