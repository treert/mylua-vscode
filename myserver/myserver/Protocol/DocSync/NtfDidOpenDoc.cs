﻿
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class DidOpenDocParams
{
    public TextDocItem textDocument { get; set; }
}

[MyProto(Direction = ProtoDirection.ToServer, IsReadOnly = false)]
public class NtfDidOpenDoc : JsonNtfBase<DidOpenDocParams>
{
    public override string m_method => "textDocument/didOpen";

    public override void OnNotify()
    {
        // todo
    }
}
