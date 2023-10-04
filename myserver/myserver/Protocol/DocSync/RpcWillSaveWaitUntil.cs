using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class WillSaveWaitUntilResult : IJson
{
    public List<TextEdit> edits = new List<TextEdit>();
    public void ReadFrom(JsonNode node)
    {
        edits = node.ConvertTo<List<TextEdit>>();
    }

    public JsonNode ToJsonNode()
    {
        return edits.ToJsonNode();
    }
}

/*
类似 NtfWillSave 但是给予服务器修改文件的机会。
客户端可以丢弃服务器返回的修改。
 */
public class RpcWillSaveWaitUntil : JsonRpcBase<WillSaveTextDocParams, WillSaveWaitUntilResult>
{
    public override string m_method => "textDocument/willSaveWaitUntil";

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
