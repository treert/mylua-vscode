
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyServer.Protocol;



public class HoverResult
{
    public MarkupContent contents { get; set; }
    public Range? range { get; set; }
}


public class RpcHover : JsonRpcBase<PosAndTokenParams, HoverResult>
{
    public override string m_method => "textDocument/hover";

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


