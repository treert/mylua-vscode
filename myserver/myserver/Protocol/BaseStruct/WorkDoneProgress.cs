using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public enum WorkDoneProgressKind
{
    Begin,
    Report,
    End,
}

/// <summary>
/// begin | report | end
/// </summary>
public class WorkDoneProgress
{
    public WorkDoneProgressKind kind { get; set; }
    public string? message { get; set; }
    public bool? cancellable { get; set; }
    /// <summary>
    /// The value range is [0, 100]
    /// </summary>
    public uint? percentage { get; set; }
}
