using MyServer.JsonRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol
{
    public enum MessageDirection
    {
        ClientToServer,
        ServerToClient,
        Both,
    }



}
