using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public enum ProtoDirection
{
    /// <summary>
    /// 客户端发送给服务器
    /// </summary>
    ToServer,
    /// <summary>
    /// 服务器发送给客户端
    /// </summary>
    ToClient,
}


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class MyProtoAttribute : Attribute
{
    public required ProtoDirection Direction { get; set; }
    /// <summary>
    /// 是否时只读的。默认时
    /// </summary>
    public bool IsReadOnly { get; set; } = true;
}
