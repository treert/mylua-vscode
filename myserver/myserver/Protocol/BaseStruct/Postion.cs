using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyServer.Protocol;

[JsonConverter(typeof(MyJsonEnumConverter))]
public enum PositionEncodingKind
{
    [MyEnum(Name = "utf-8")]
    UTF8,
    /// <summary>
    /// This is the default and must always be supported by servers
    /// </summary>
    [MyEnum(Name = "utf-16")]
    UTF16,
    [MyEnum(Name = "utf-32")]
    UTF32,
}

public class Position
{
    /// <summary>
    /// Line position in a document (zero-based).
    /// </summary>
    public uint line { get; set; }

    /// <summary>
    /// Character offset on a line in a document(zero-based). The meaning of this
    /// offset is determined by the negotiated `PositionEncodingKind`.
    /// <br/>
    /// If the character value is greater than the line length it defaults back
    /// to the line length.
    /// </summary>
    public uint character { get; set; }
}

/// <summary>
/// 左闭右开
/// 
/// Therefore, the end position is exclusive.
/// If you want to specify a range that contains a line including the line ending character(s)
/// then use an end position denoting the start of the next line. For example:
/// </summary>
public class Range
{
    public Position start { get; set; }
    public Position end { get; set; }
}

