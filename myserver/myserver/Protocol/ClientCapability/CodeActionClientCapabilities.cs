using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace MyServer.Protocol;
public class CodeActionClientCapabilities
{
    public bool? dynamicRegistration { get; set; }
    public class _codeActionLiteralSupport
    {
        public ValueSet<CodeActionKind> codeActionKind;
    }
    /// <summary>
    /// The client supports code action literals as a valid
    /// response of the `textDocument/codeAction` request.
    /// <br/>
    /// @since 3.8.0
    /// </summary>
    public _codeActionLiteralSupport? codeActionLiteralSupport { get;set; }
    public bool? isPreferredSupport { get; set; }
    public bool? disabledSupport { get; set; }
    /// <summary>
    /// Whether code action supports the `data` property which is
    /// preserved between a `textDocument/codeAction` and a
    /// `codeAction/resolve` request.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public bool? dataSupport { get; set; }
    /// <summary>
    /// Whether the client supports resolving additional code action
    /// properties via a separate `codeAction/resolve` request.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public Properties? resolveSupport { get; set; }
    /// <summary>
    /// Whether the client honors the change annotations in
    /// text edits and resource operations returned via the
    /// `CodeAction#edit` property by for example presenting
    /// the workspace edit in the user interface and asking
    /// for confirmation.
    /// <br/>
    /// @since 3.16.0
    /// </summary>
    public bool? honorsChangeAnnotations { get; set; }
}

/// <summary>
/// todo@xx
/// The kind of a code action.
/// <br/>
/// Kinds are a hierarchical list of identifiers separated by `.`,
/// e.g. `"refactor.extract.function"`.
/// <br/>
/// The set of kinds is open and client needs to announce the kinds it supports
/// to the server during initialization.
/// </summary>
[JsonConverter(typeof(CodeActionKindJsonConverter))]
public class CodeActionKind
{
    public enum EBaseKind
    {
        Empty,
        QuickFix,
        Refactor,
        RefactorExtract,
        RefactorInline,
        RefactorRewrite,
        Source,
        SourceOrganizeImports,
        /// <summary>
        /// Base kind for a 'fix all' source action: `source.fixAll`.
        /// <br/>
        /// 'Fix all' actions automatically fix errors that have a clear fix that
        /// do not require user input. They should not suppress errors or perform
        /// unsafe fixes such as generating new types or classes.
        /// </summary>
        SourceFixAll,
    }
    private EBaseKind m_base_kind = EBaseKind.Empty;
    private string m_raw_kind = string.Empty;
    public EBaseKind BaseKind
    {
        get { return m_base_kind; }
        set { 
            m_base_kind = value;
            switch (m_base_kind)
            {
                case EBaseKind.Empty:
                    m_raw_kind = "";
                    break;
                case EBaseKind.RefactorExtract:
                    m_raw_kind = "refactor.extract";
                    break;
                case EBaseKind.RefactorInline:
                    m_raw_kind = "refactor.inline";
                    break;
                case EBaseKind.RefactorRewrite:
                    m_raw_kind = "refactor.rewrite";
                    break;
                case EBaseKind.Source:
                    m_raw_kind = "source";
                    break;
                case EBaseKind.SourceOrganizeImports:
                    m_raw_kind = "source.organizeImports";
                    break;
                case EBaseKind.SourceFixAll:
                    m_raw_kind = "source.fixAll";
                    break;
                case EBaseKind.QuickFix:
                    m_raw_kind = "quickfix";
                    break;
            }
        }
    }
    public string RawKind {
        get {
            return m_raw_kind;
        }
        set {
            m_raw_kind = value;
            if (value.StartsWith("quickfix"))
            {
                m_base_kind = EBaseKind.QuickFix;
            }
            else if (value.StartsWith("refactor.extract"))
            {
                m_base_kind = EBaseKind.RefactorExtract;
            }
            else if (value.StartsWith("refactor.inline"))
            {
                m_base_kind = EBaseKind.RefactorInline;
            }
            else if (value.StartsWith("refactor.rewrite"))
            {
                m_base_kind = EBaseKind.RefactorRewrite;
            }
            else if (value.StartsWith("source.organizeImports"))
            {
                m_base_kind = EBaseKind.SourceOrganizeImports;
            }
            else if (value.StartsWith("source.fixAll"))
            {
                m_base_kind = EBaseKind.SourceFixAll;
            }
            else if (value.StartsWith("source"))
            {
                m_base_kind = EBaseKind.Source;
            }
            else
            {
                m_base_kind = EBaseKind.Empty;
            }
        }
    }
}

public class CodeActionKindJsonConverter : JsonConverter<CodeActionKind>
{
    public override CodeActionKind? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if(reader.TokenType == JsonTokenType.String)
        {
            CodeActionKind codeActionKind = new CodeActionKind();
            codeActionKind.RawKind = reader.GetString()!;
            return codeActionKind;
        }
        throw new Exception();
    }

    public override void Write(Utf8JsonWriter writer, CodeActionKind value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.RawKind);
    }
}

