using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public enum MessageType
{
    Error = 1,
    Warning = 2,
    Info = 3,
    Log = 4,
    Debug = 5,
}

public class ShowMessageParams
{
    public MessageType type { get; set; }
    public string message { get; set; }
}

[MyProto(Direction = ProtoDirection.ToClient)]
public class NtfShowMessage : JsonNtfBase<ShowMessageParams>
{
    public override string m_method => "window/showMessage";

    public override void OnNotify()
    {

    }

    public static void ShowMessage(string message, MessageType type=MessageType.Info)
    {
        var ntf = new NtfShowMessage();
        ntf.Args.message = message;
        ntf.Args.type = type;
        ntf.SendNotify();
    }
}