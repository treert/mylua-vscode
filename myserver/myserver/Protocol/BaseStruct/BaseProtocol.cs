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
    ToServer,
    ToClient,
}


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class MyProtoAttribute : Attribute
{
    public required ProtoDirection Direction { get; set; }
}
