using MyServer.Protocol;
using MyServer.Protocol.MyProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Model;

public class StatusBarMgr
{
    public static StatusBarMgr Instance { get; set; } = new StatusBarMgr();

    public void Show()
    {
        var ntf = new NtfStatusBarShow();
        ntf.SendNotify();
    }
    public void Hide()
    {
        var ntf = new NtfStatusBarHide();
        ntf.SendNotify();
    }
    private int count = 0;
    public void OnClick()
    {
        count++;
        NtfShowMessage.ShowMessage("you clicked status bar "+count.ToString());
    }
    public void Report(string text, string? tooltip = null)
    {
        var ntf = new NtfStatusBarReport();
        ntf.Args.text = text;
        ntf.Args.tooltip = tooltip;
        ntf.SendNotify();
    }
}
