using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyServer.Protocol;

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

[Flags]
public enum SemanticTokenModifiers
{
    Declaration = 1,
    Definition = 1<<1,
    Readonly = 1 << 2,
    Static = 1 << 3,
    Deprecated = 1 << 4,
    Abstract = 1 << 5,
    Async = 1 << 6,
    Modification = 1 << 7,
    Documentation = 1 << 8,
    [MyEnum(Name = "defaultLibrary")]
    DefaultLibrary = 1 << 9,
}

[JsonConverter(typeof(MyJsonEnumConverter))]
public enum TokenFormat
{
    Relative,
}

public class SemanticTokensLegend
{
    /// <summary>
    /// The token types a server uses.
    /// </summary>
    public string[] tokenTypes {  get; set; }
    /// <summary>
    /// The token modifiers a server uses.
    /// </summary>
    public string[] tokenModifiers { get; set; }

    public SemanticTokensLegend()
    {
        var tokens = typeof(SemanticTokenTypes).GetEnumValues();
        tokenTypes = new string[tokens.Length];
        for (int i = 0; i < tokens.Length; i++)
        {
            tokenTypes[i] = MyJsonEnumConverter.GetMyName((tokens.GetValue(i) as Enum)!);
        }

        var mods = typeof(SemanticTokenModifiers).GetEnumValues();
        tokenModifiers = new string[mods.Length];
        for (int i = 0; i < mods.Length; i++)
        {
            tokenModifiers[i] = MyJsonEnumConverter.GetMyName((mods.GetValue(i) as Enum)!);
        }
    }
}
