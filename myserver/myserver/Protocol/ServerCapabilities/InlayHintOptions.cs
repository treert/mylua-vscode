﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class InlayHintOptions : WorkDoneProgressOptions
{
    /// <summary>
    /// The server provides support to resolve additional information for an inlay hint item.
    /// </summary>
    public bool? resolveProvider { get; set; }
}
