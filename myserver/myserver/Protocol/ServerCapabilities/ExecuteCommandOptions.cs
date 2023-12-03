using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
/// <summary>
/// 似乎支持客户端请求服务器执行命令
/// </summary>
public class ExecuteCommandOptions : WorkDoneProgressOptions
{
    /// <summary>
    /// The commands to be executed on the server
    /// </summary>
    public List<string> commands { get; set; } = new List<string>();
}
