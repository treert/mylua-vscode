﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
