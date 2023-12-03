
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

/// <summary>
/// 类似 NtfWillSave 但是给予服务器修改文件的机会。<br/>
/// 如果客户端接受修改。客户端会再次发送 didChange ntf，然后在发送 didsave ntf。
/// 客户端也可以丢弃服务器返回的修改。
/// </summary>
[MyProto(Direction = ProtoDirection.ToServer)]
public class RpcWillSaveWaitUntil : JsonRpcBase<WillSaveTextDocParams, List<TextEdit>>
{
    public override string m_method => "textDocument/willSaveWaitUntil";

    public override void OnCanceled()
    {
        // nothing now
        m_res = null;
        SendResponse();
    }

    public override void OnRequest()
    {
        // nothing now
        m_res = null;
        // debug
        //m_res = new List<TextEdit>();
        //m_res.Add(new TextEdit()
        //{
        //    newText = "add one line \n",
        //    range = new Range()
        //    {
        //        start = new Position() { character = 0 , line = 0 },
        //        end = new Position() { character = 0, line = 0 },
        //    }
        //});
        SendResponse();
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}
