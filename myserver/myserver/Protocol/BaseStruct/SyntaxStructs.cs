using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyServer.Protocol;

[JsonConverter(typeof(MyJsonEnumConverter))]
public enum SemanticTokenTypes
{
    Namespace,
    /// <summary>
    /// Represents a generic type. Acts as a fallback for types which
    /// can't be mapped to a specific type like class or enum.
    /// </summary>
    Type,
    Class,
    Enum,
    Interface,
    Struct,
    [MyEnum(Name = "typeParameter")]
    TypeParameter,
    Parameter,
    Variable,
    Property,
    [MyEnum(Name = "enumMember")]
    EnumMember,
    Event,
    Function,
    Method,
    Macro,
    Keyword,
    Modifier,
    Comment,
    String,
    Number,
    Regexp,
    Operator,
    Decorator,
}

[JsonConverter(typeof(MyJsonEnumConverter))]
public enum SemanticTokenModifiers
{
    Declaration,
    Definition,
    Readonly,
    Static,
    Deprecated,
    Abstract,
    Async,
    Modification,
    Documentation,
    [MyEnum(Name = "defaultLibrary")]
    DefaultLibrary,
}

public enum TokenFormat
{
    Relative,
}

public class SemanticTokensLegend
{
    /// <summary>
    /// The token types a server uses.
    /// </summary>
    public SemanticTokenTypes[] tokenTypes {  get; set; }
    /// <summary>
    /// The token modifiers a server uses.
    /// </summary>
    public SemanticTokenModifiers[] tokenModifiers { get; set; }
}
