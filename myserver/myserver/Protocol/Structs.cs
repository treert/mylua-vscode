// // auto generate, do not modify it.
// using MyServer.Misc;

// namespace MyServer.Protocol;


// // typeAliases
// /// <summary>
// /// Information about where a symbol is defined.
// /// 
// /// Provides additional metadata over normal {@link Location location} definitions, including the range of
// /// the defining symbol
// /// </summary>
// using DefinitionLink = LocationLink;

// /// <summary>
// /// Information about where a symbol is declared.
// /// 
// /// Provides additional metadata over normal {@link Location location} declarations, including the range of
// /// the declaring symbol.
// /// 
// /// Servers should prefer returning `DeclarationLink` over `Declaration` if supported
// /// by the client.
// /// </summary>
// using DeclarationLink = LocationLink;

// /// <summary>
// /// The definition of a symbol represented as one or many {@link Location locations}.
// /// For most programming languages there is only one location at which a symbol is
// /// defined.
// /// 
// /// Servers should prefer returning `DefinitionLink` over `Definition` if supported
// /// by the client.
// /// MyNode<Location,List<Location>>
// /// </summary>
// using Definition = MyNode;

// /// <summary>
// /// The LSP any type.
// /// Please note that strictly speaking a property with the value `undefined`
// /// can't be converted into JSON preserving the property name. However for
// /// convenience it is allowed and assumed that all these properties are
// /// optional as well.
// /// @since 3.17.0
// /// MyNode<null,LSPObject,LSPArray,string,int,uint,float,bool>
// /// </summary>
// using LSPAny = MyNode;

// /// <summary>
// /// The declaration of a symbol representation as one or many {@link Location locations}.
// /// MyNode<Location,List<Location>>
// /// </summary>
// using Declaration = MyNode;

// /// <summary>
// /// Inline value information can be provided by different means:
// /// - directly as a text value (class InlineValueText).
// /// - as a name to use for a variable lookup (class InlineValueVariableLookup)
// /// - as an evaluatable expression (class InlineValueEvaluatableExpression)
// /// The InlineValue types combines all inline value types into one type.
// /// 
// /// @since 3.17.0
// /// MyNode<InlineValueText,InlineValueVariableLookup,InlineValueEvaluatableExpression>
// /// </summary>
// using InlineValue = MyNode;

// /// <summary>
// /// The result of a document diagnostic pull request. A report can
// /// either be a full report containing all diagnostics for the
// /// requested document or an unchanged report indicating that nothing
// /// has changed in terms of diagnostics in comparison to the last
// /// pull request.
// /// 
// /// @since 3.17.0
// /// MyNode<RelatedFullDocumentDiagnosticReport,RelatedUnchangedDocumentDiagnosticReport>
// /// </summary>
// using DocumentDiagnosticReport = MyNode;

// /// <summary>
// /// 
// /// MyNode<Range,MyNode,MyNode>
// /// </summary>
// using PrepareRenameResult = MyNode;

// /// <summary>
// /// 
// /// MyNode<int,string>
// /// </summary>
// using ProgressToken = MyNode;

// /// <summary>
// /// A workspace diagnostic document report.
// /// 
// /// @since 3.17.0
// /// MyNode<WorkspaceFullDocumentDiagnosticReport,WorkspaceUnchangedDocumentDiagnosticReport>
// /// </summary>
// using WorkspaceDocumentDiagnosticReport = MyNode;

// /// <summary>
// /// An event describing a change to a text document. If only a text is provided
// /// it is considered to be the full content of the document.
// /// MyNode<MyNode,MyNode>
// /// </summary>
// using TextDocumentContentChangeEvent = MyNode;

// /// <summary>
// /// MarkedString can be used to render human readable text. It is either a markdown string
// /// or a code-block that provides a language and a code snippet. The language identifier
// /// is semantically equal to the optional language identifier in fenced code blocks in GitHub
// /// issues. See https://help.github.com/articles/creating-and-highlighting-code-blocks/#syntax-highlighting
// /// 
// /// The pair of a language and a value is an equivalent to markdown:
// /// ```${language}
// /// ${value}
// /// ```
// /// 
// /// Note that markdown strings will be sanitized - that means html will be escaped.
// /// @deprecated use MarkupContent instead.
// /// MyNode<string,MyNode>
// /// </summary>
// using MarkedString = MyNode;

// /// <summary>
// /// A document filter describes a top level text document or
// /// a notebook cell document.
// /// 
// /// @since 3.17.0 - proposed support for NotebookCellTextDocumentFilter.
// /// MyNode<TextDocumentFilter,NotebookCellTextDocumentFilter>
// /// </summary>
// using DocumentFilter = MyNode;

// /// <summary>
// /// The glob pattern. Either a string pattern or a relative pattern.
// /// 
// /// @since 3.17.0
// /// MyNode<Pattern,RelativePattern>
// /// </summary>
// using GlobPattern = MyNode;

// /// <summary>
// /// A document filter denotes a document by different properties like
// /// the {@link TextDocument.languageId language}, the {@link Uri.scheme scheme} of
// /// its resource, or a glob-pattern that is applied to the {@link TextDocument.fileName path}.
// /// 
// /// Glob patterns can have the following syntax:
// /// - `*` to match one or more characters in a path segment
// /// - `?` to match on one character in a path segment
// /// - `**` to match any number of path segments, including none
// /// - `{}` to group sub patterns into an OR expression. (e.g. `**​/*.{ts,js}` matches all TypeScript and JavaScript files)
// /// - `[]` to declare a range of characters to match in a path segment (e.g., `example.[0-9]` to match on `example.0`, `example.1`, …)
// /// - `[!...]` to negate a range of characters to match in a path segment (e.g., `example.[!0-9]` to match on `example.a`, `example.b`, but not `example.0`)
// /// 
// /// @sample A language filter that applies to typescript files on disk: `{ language: 'typescript', scheme: 'file' }`
// /// @sample A language filter that applies to all package.json paths: `{ language: 'json', pattern: '**package.json' }`
// /// 
// /// @since 3.17.0
// /// MyNode<MyNode,MyNode,MyNode>
// /// </summary>
// using TextDocumentFilter = MyNode;

// /// <summary>
// /// A notebook document filter denotes a notebook document by
// /// different properties. The properties will be match
// /// against the notebook's URI (same as with documents)
// /// 
// /// @since 3.17.0
// /// MyNode<MyNode,MyNode,MyNode>
// /// </summary>
// using NotebookDocumentFilter = MyNode;

// /// <summary>
// /// An identifier to refer to a change annotation stored with a workspace edit.
// /// </summary>
// using ChangeAnnotationIdentifier = string;

// /// <summary>
// /// The glob pattern to watch relative to the base path. Glob patterns can have the following syntax:
// /// - `*` to match one or more characters in a path segment
// /// - `?` to match on one character in a path segment
// /// - `**` to match any number of path segments, including none
// /// - `{}` to group conditions (e.g. `**​/*.{ts,js}` matches all TypeScript and JavaScript files)
// /// - `[]` to declare a range of characters to match in a path segment (e.g., `example.[0-9]` to match on `example.0`, `example.1`, …)
// /// - `[!...]` to negate a range of characters to match in a path segment (e.g., `example.[!0-9]` to match on `example.a`, `example.b`, but not `example.0`)
// /// 
// /// @since 3.17.0
// /// </summary>
// using Pattern = string;

// /// <summary>
// /// LSP object definition.
// /// @since 3.17.0
// /// </summary>
// using LSPObject = Dictionary<string,LSPAny>;

// /// <summary>
// /// A document selector is the combination of one or many document filters.
// /// 
// /// @sample `let sel:DocumentSelector = [{ language: 'typescript' }, { language: 'json', pattern: '**∕tsconfig.json' }]`;
// /// 
// /// The use of a string as a document filter is deprecated @since 3.16.0.
// /// </summary>
// using DocumentSelector = List<DocumentFilter>;

// /// <summary>
// /// LSP arrays.
// /// @since 3.17.0
// /// </summary>
// using LSPArray = List<LSPAny>;


// // Structures

// public class ImplementationParams: ITextDocumentPositionParams,IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// The text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position inside the text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }
// }

// /// <summary>
// /// Represents a location inside a resource, such as a line
// /// inside a text file.
// /// </summary>
// public class Location{

    
//     public DocumentUri uri { get; set; }

    
//     public Range range { get; set; }
// }


// public class ImplementationRegistrationOptions: ITextDocumentRegistrationOptions,IImplementationOptions,IStaticRegistrationOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from ImplementationOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again. See also Registration#id.
//     /// extend from StaticRegistrationOptions
//     /// </summary>
//     public string? id { get; set; }
// }


// public class TypeDefinitionParams: ITextDocumentPositionParams,IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// The text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position inside the text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }
// }


// public class TypeDefinitionRegistrationOptions: ITextDocumentRegistrationOptions,ITypeDefinitionOptions,IStaticRegistrationOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from TypeDefinitionOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again. See also Registration#id.
//     /// extend from StaticRegistrationOptions
//     /// </summary>
//     public string? id { get; set; }
// }

// /// <summary>
// /// A workspace folder inside a client.
// /// </summary>
// public class WorkspaceFolder{

//     /// <summary>
//     /// The associated URI for this workspace folder.
//     /// </summary>
//     public Uri uri { get; set; }

//     /// <summary>
//     /// The name of the workspace folder. Used to refer to this
//     /// workspace folder in the user interface.
//     /// </summary>
//     public string name { get; set; }
// }

// /// <summary>
// /// The parameters of a `workspace/didChangeWorkspaceFolders` notification.
// /// </summary>
// public class DidChangeWorkspaceFoldersParams{

//     /// <summary>
//     /// The actual workspace folder change event.
//     /// </summary>
//     public WorkspaceFoldersChangeEvent @event { get; set; }
// }

// /// <summary>
// /// The parameters of a configuration request.
// /// </summary>
// public class ConfigurationParams{

    
//     public List<ConfigurationItem> items { get; set; }
// }

// /// <summary>
// /// Parameters for a {@link DocumentColorRequest}.
// /// </summary>
// public class DocumentColorParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

//     /// <summary>
//     /// The text document.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }
// }

// /// <summary>
// /// Represents a color range from a document.
// /// </summary>
// public class ColorInformation{

//     /// <summary>
//     /// The range in the document where this color appears.
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// The actual color value for this color range.
//     /// </summary>
//     public Color color { get; set; }
// }


// public class DocumentColorRegistrationOptions: ITextDocumentRegistrationOptions,IDocumentColorOptions,IStaticRegistrationOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from DocumentColorOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again. See also Registration#id.
//     /// extend from StaticRegistrationOptions
//     /// </summary>
//     public string? id { get; set; }
// }

// /// <summary>
// /// Parameters for a {@link ColorPresentationRequest}.
// /// </summary>
// public class ColorPresentationParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

//     /// <summary>
//     /// The text document.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The color to request presentations for.
//     /// </summary>
//     public Color color { get; set; }

//     /// <summary>
//     /// The range where the color would be inserted. Serves as a context.
//     /// </summary>
//     public Range range { get; set; }
// }


// public class ColorPresentation{

//     /// <summary>
//     /// The label of this color presentation. It will be shown on the color
//     /// picker header. By default this is also the text that is inserted when selecting
//     /// this color presentation.
//     /// </summary>
//     public string label { get; set; }

//     /// <summary>
//     /// An {@link TextEdit edit} which is applied to a document when selecting
//     /// this presentation for the color.  When `falsy` the {@link ColorPresentation.label label}
//     /// is used.
//     /// </summary>
//     public TextEdit? textEdit { get; set; }

//     /// <summary>
//     /// An optional array of additional {@link TextEdit text edits} that are applied when
//     /// selecting this color presentation. Edits must not overlap with the main {@link ColorPresentation.textEdit edit} nor with themselves.
//     /// </summary>
//     public List<TextEdit>? additionalTextEdits { get; set; }
// }


// public interface IWorkDoneProgressOptions {

    
//     public bool? workDoneProgress { get; set; }
// }


// public class WorkDoneProgressOptions: IWorkDoneProgressOptions{

    
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// General text document registration options.
// /// </summary>
// public interface ITextDocumentRegistrationOptions {

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// </summary>
//     public MyNode documentSelector { get; set; }
// }

// /// <summary>
// /// General text document registration options.
// /// </summary>
// public class TextDocumentRegistrationOptions: ITextDocumentRegistrationOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// </summary>
//     public MyNode documentSelector { get; set; }
// }

// /// <summary>
// /// Parameters for a {@link FoldingRangeRequest}.
// /// </summary>
// public class FoldingRangeParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

//     /// <summary>
//     /// The text document.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }
// }

// /// <summary>
// /// Represents a folding range. To be valid, start and end line must be bigger than zero and smaller
// /// than the number of lines in the document. Clients are free to ignore invalid ranges.
// /// </summary>
// public class FoldingRange{

//     /// <summary>
//     /// The zero-based start line of the range to fold. The folded area starts after the line's last character.
//     /// To be valid, the end must be zero or larger and smaller than the number of lines in the document.
//     /// </summary>
//     public uint startLine { get; set; }

//     /// <summary>
//     /// The zero-based character offset from where the folded range starts. If not defined, defaults to the length of the start line.
//     /// </summary>
//     public uint? startCharacter { get; set; }

//     /// <summary>
//     /// The zero-based end line of the range to fold. The folded area ends with the line's last character.
//     /// To be valid, the end must be zero or larger and smaller than the number of lines in the document.
//     /// </summary>
//     public uint endLine { get; set; }

//     /// <summary>
//     /// The zero-based character offset before the folded range ends. If not defined, defaults to the length of the end line.
//     /// </summary>
//     public uint? endCharacter { get; set; }

//     /// <summary>
//     /// Describes the kind of the folding range such as `comment' or 'region'. The kind
//     /// is used to categorize folding ranges and used by commands like 'Fold all comments'.
//     /// See {@link FoldingRangeKind} for an enumeration of standardized kinds.
//     /// </summary>
//     public string? kind { get; set; }

//     /// <summary>
//     /// The text that the client should show when the specified range is
//     /// collapsed. If not defined or not supported by the client, a default
//     /// will be chosen by the client.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public string? collapsedText { get; set; }
// }


// public class FoldingRangeRegistrationOptions: ITextDocumentRegistrationOptions,IFoldingRangeOptions,IStaticRegistrationOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from FoldingRangeOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again. See also Registration#id.
//     /// extend from StaticRegistrationOptions
//     /// </summary>
//     public string? id { get; set; }
// }


// public class DeclarationParams: ITextDocumentPositionParams,IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// The text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position inside the text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }
// }


// public class DeclarationRegistrationOptions: IDeclarationOptions,ITextDocumentRegistrationOptions,IStaticRegistrationOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from DeclarationOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again. See also Registration#id.
//     /// extend from StaticRegistrationOptions
//     /// </summary>
//     public string? id { get; set; }
// }

// /// <summary>
// /// A parameter literal used in selection range requests.
// /// </summary>
// public class SelectionRangeParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

//     /// <summary>
//     /// The text document.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The positions inside the text document.
//     /// </summary>
//     public List<Position> positions { get; set; }
// }

// /// <summary>
// /// A selection range represents a part of a selection hierarchy. A selection range
// /// may have a parent selection range that contains it.
// /// </summary>
// public class SelectionRange{

//     /// <summary>
//     /// The {@link Range range} of this selection range.
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// The parent selection range containing this range. Therefore `parent.range` must contain `this.range`.
//     /// </summary>
//     public SelectionRange? parent { get; set; }
// }


// public class SelectionRangeRegistrationOptions: ISelectionRangeOptions,ITextDocumentRegistrationOptions,IStaticRegistrationOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from SelectionRangeOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again. See also Registration#id.
//     /// extend from StaticRegistrationOptions
//     /// </summary>
//     public string? id { get; set; }
// }


// public class WorkDoneProgressCreateParams{

//     /// <summary>
//     /// The token to be used to report progress.
//     /// </summary>
//     public ProgressToken token { get; set; }
// }


// public class WorkDoneProgressCancelParams{

//     /// <summary>
//     /// The token to be used to report progress.
//     /// </summary>
//     public ProgressToken token { get; set; }
// }

// /// <summary>
// /// The parameter of a `textDocument/prepareCallHierarchy` request.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class CallHierarchyPrepareParams: ITextDocumentPositionParams,IWorkDoneProgressParams{

//     /// <summary>
//     /// The text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position inside the text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }
// }

// /// <summary>
// /// Represents programming constructs like functions or constructors in the context
// /// of call hierarchy.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class CallHierarchyItem{

//     /// <summary>
//     /// The name of this item.
//     /// </summary>
//     public string name { get; set; }

//     /// <summary>
//     /// The kind of this item.
//     /// </summary>
//     public uint kind { get; set; }

//     /// <summary>
//     /// Tags for this item.
//     /// </summary>
//     public List<SymbolTag>? tags { get; set; }

//     /// <summary>
//     /// More detail for this item, e.g. the signature of a function.
//     /// </summary>
//     public string? detail { get; set; }

//     /// <summary>
//     /// The resource identifier of this item.
//     /// </summary>
//     public DocumentUri uri { get; set; }

//     /// <summary>
//     /// The range enclosing this symbol not including leading/trailing whitespace but everything else, e.g. comments and code.
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// The range that should be selected and revealed when this symbol is being picked, e.g. the name of a function.
//     /// Must be contained by the {@link CallHierarchyItem.range `range`}.
//     /// </summary>
//     public Range selectionRange { get; set; }

//     /// <summary>
//     /// A data entry field that is preserved between a call hierarchy prepare and
//     /// incoming calls or outgoing calls requests.
//     /// </summary>
//     public LSPAny? data { get; set; }
// }

// /// <summary>
// /// Call hierarchy options used during static or dynamic registration.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class CallHierarchyRegistrationOptions: ITextDocumentRegistrationOptions,ICallHierarchyOptions,IStaticRegistrationOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from CallHierarchyOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again. See also Registration#id.
//     /// extend from StaticRegistrationOptions
//     /// </summary>
//     public string? id { get; set; }
// }

// /// <summary>
// /// The parameter of a `callHierarchy/incomingCalls` request.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class CallHierarchyIncomingCallsParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

    
//     public CallHierarchyItem item { get; set; }
// }

// /// <summary>
// /// Represents an incoming call, e.g. a caller of a method or constructor.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class CallHierarchyIncomingCall{

//     /// <summary>
//     /// The item that makes the call.
//     /// </summary>
//     public CallHierarchyItem from { get; set; }

//     /// <summary>
//     /// The ranges at which the calls appear. This is relative to the caller
//     /// denoted by {@link CallHierarchyIncomingCall.from `this.from`}.
//     /// </summary>
//     public List<Range> fromRanges { get; set; }
// }

// /// <summary>
// /// The parameter of a `callHierarchy/outgoingCalls` request.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class CallHierarchyOutgoingCallsParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

    
//     public CallHierarchyItem item { get; set; }
// }

// /// <summary>
// /// Represents an outgoing call, e.g. calling a getter from a method or a method from a constructor etc.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class CallHierarchyOutgoingCall{

//     /// <summary>
//     /// The item that is called.
//     /// </summary>
//     public CallHierarchyItem to { get; set; }

//     /// <summary>
//     /// The range at which this item is called. This is the range relative to the caller, e.g the item
//     /// passed to {@link CallHierarchyItemProvider.provideCallHierarchyOutgoingCalls `provideCallHierarchyOutgoingCalls`}
//     /// and not {@link CallHierarchyOutgoingCall.to `this.to`}.
//     /// </summary>
//     public List<Range> fromRanges { get; set; }
// }

// /// <summary>
// /// @since 3.16.0
// /// </summary>
// public class SemanticTokensParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

//     /// <summary>
//     /// The text document.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }
// }

// /// <summary>
// /// @since 3.16.0
// /// </summary>
// public class SemanticTokens{

//     /// <summary>
//     /// An optional result id. If provided and clients support delta updating
//     /// the client will include the result id in the next semantic token request.
//     /// A server can then instead of computing all semantic tokens again simply
//     /// send a delta.
//     /// </summary>
//     public string? resultId { get; set; }

//     /// <summary>
//     /// The actual tokens.
//     /// </summary>
//     public List<uint> data { get; set; }
// }

// /// <summary>
// /// @since 3.16.0
// /// </summary>
// public class SemanticTokensPartialResult{

    
//     public List<uint> data { get; set; }
// }

// /// <summary>
// /// @since 3.16.0
// /// </summary>
// public class SemanticTokensRegistrationOptions: ITextDocumentRegistrationOptions,ISemanticTokensOptions,IStaticRegistrationOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from SemanticTokensOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The legend used by the server
//     /// extend from SemanticTokensOptions
//     /// </summary>
//     public SemanticTokensLegend legend { get; set; }

//     /// <summary>
//     /// Server supports providing semantic tokens for a specific range
//     /// of a document.
//     /// extend from SemanticTokensOptions
//     /// </summary>
//     public MyNode? range { get; set; }

//     /// <summary>
//     /// Server supports providing semantic tokens for a full document.
//     /// extend from SemanticTokensOptions
//     /// </summary>
//     public MyNode? full { get; set; }

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again. See also Registration#id.
//     /// extend from StaticRegistrationOptions
//     /// </summary>
//     public string? id { get; set; }
// }

// /// <summary>
// /// @since 3.16.0
// /// </summary>
// public class SemanticTokensDeltaParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

//     /// <summary>
//     /// The text document.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The result id of a previous response. The result Id can either point to a full response
//     /// or a delta response depending on what was received last.
//     /// </summary>
//     public string previousResultId { get; set; }
// }

// /// <summary>
// /// @since 3.16.0
// /// </summary>
// public class SemanticTokensDelta{

    
//     public string? resultId { get; set; }

//     /// <summary>
//     /// The semantic token edits to transform a previous result into a new result.
//     /// </summary>
//     public List<SemanticTokensEdit> edits { get; set; }
// }

// /// <summary>
// /// @since 3.16.0
// /// </summary>
// public class SemanticTokensDeltaPartialResult{

    
//     public List<SemanticTokensEdit> edits { get; set; }
// }

// /// <summary>
// /// @since 3.16.0
// /// </summary>
// public class SemanticTokensRangeParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

//     /// <summary>
//     /// The text document.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The range the semantic tokens are requested for.
//     /// </summary>
//     public Range range { get; set; }
// }

// /// <summary>
// /// Params to show a document.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class ShowDocumentParams{

//     /// <summary>
//     /// The document uri to show.
//     /// </summary>
//     public Uri uri { get; set; }

//     /// <summary>
//     /// Indicates to show the resource in an external program.
//     /// To show for example `https://code.visualstudio.com/`
//     /// in the default WEB browser set `external` to `true`.
//     /// </summary>
//     public bool? external { get; set; }

//     /// <summary>
//     /// An optional property to indicate whether the editor
//     /// showing the document should take focus or not.
//     /// Clients might ignore this property if an external
//     /// program is started.
//     /// </summary>
//     public bool? takeFocus { get; set; }

//     /// <summary>
//     /// An optional selection range if the document is a text
//     /// document. Clients might ignore the property if an
//     /// external program is started or the file is not a text
//     /// file.
//     /// </summary>
//     public Range? selection { get; set; }
// }

// /// <summary>
// /// The result of a showDocument request.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class ShowDocumentResult{

//     /// <summary>
//     /// A boolean indicating if the show was successful.
//     /// </summary>
//     public bool success { get; set; }
// }


// public class LinkedEditingRangeParams: ITextDocumentPositionParams,IWorkDoneProgressParams{

//     /// <summary>
//     /// The text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position inside the text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }
// }

// /// <summary>
// /// The result of a linked editing range request.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class LinkedEditingRanges{

//     /// <summary>
//     /// A list of ranges that can be edited together. The ranges must have
//     /// identical length and contain identical text content. The ranges cannot overlap.
//     /// </summary>
//     public List<Range> ranges { get; set; }

//     /// <summary>
//     /// An optional word pattern (regular expression) that describes valid contents for
//     /// the given ranges. If no pattern is provided, the client configuration's word
//     /// pattern will be used.
//     /// </summary>
//     public string? wordPattern { get; set; }
// }


// public class LinkedEditingRangeRegistrationOptions: ITextDocumentRegistrationOptions,ILinkedEditingRangeOptions,IStaticRegistrationOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from LinkedEditingRangeOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again. See also Registration#id.
//     /// extend from StaticRegistrationOptions
//     /// </summary>
//     public string? id { get; set; }
// }

// /// <summary>
// /// The parameters sent in notifications/requests for user-initiated creation of
// /// files.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class CreateFilesParams{

//     /// <summary>
//     /// An array of all files/folders created in this operation.
//     /// </summary>
//     public List<FileCreate> files { get; set; }
// }

// /// <summary>
// /// A workspace edit represents changes to many resources managed in the workspace. The edit
// /// should either provide `changes` or `documentChanges`. If documentChanges are present
// /// they are preferred over `changes` if the client can handle versioned document edits.
// /// 
// /// Since version 3.13.0 a workspace edit can contain resource operations as well. If resource
// /// operations are present clients need to execute the operations in the order in which they
// /// are provided. So a workspace edit for example can consist of the following two changes:
// /// (1) a create file a.txt and (2) a text document edit which insert text into file a.txt.
// /// 
// /// An invalid sequence (e.g. (1) delete file a.txt and (2) insert text into file a.txt) will
// /// cause failure of the operation. How the client recovers from the failure is described by
// /// the client capability: `workspace.workspaceEdit.failureHandling`
// /// </summary>
// public class WorkspaceEdit{

//     /// <summary>
//     /// Holds changes to existing resources.
//     /// </summary>
//     public Dictionary<DocumentUri,List<TextEdit>>? changes { get; set; }

//     /// <summary>
//     /// Depending on the client capability `workspace.workspaceEdit.resourceOperations` document changes
//     /// are either an array of `TextDocumentEdit`s to express changes to n different text documents
//     /// where each text document edit addresses a specific version of a text document. Or it can contain
//     /// above `TextDocumentEdit`s mixed with create, rename and delete file / folder operations.
//     /// 
//     /// Whether a client supports versioned document edits is expressed via
//     /// `workspace.workspaceEdit.documentChanges` client capability.
//     /// 
//     /// If a client neither supports `documentChanges` nor `workspace.workspaceEdit.resourceOperations` then
//     /// only plain `TextEdit`s using the `changes` property are supported.
//     /// </summary>
//     public List<MyNode>? documentChanges { get; set; }

//     /// <summary>
//     /// A map of change annotations that can be referenced in `AnnotatedTextEdit`s or create, rename and
//     /// delete file / folder operations.
//     /// 
//     /// Whether clients honor this property depends on the client capability `workspace.changeAnnotationSupport`.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public Dictionary<ChangeAnnotationIdentifier,ChangeAnnotation>? changeAnnotations { get; set; }
// }

// /// <summary>
// /// The options to register for file operations.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class FileOperationRegistrationOptions{

//     /// <summary>
//     /// The actual filters.
//     /// </summary>
//     public List<FileOperationFilter> filters { get; set; }
// }

// /// <summary>
// /// The parameters sent in notifications/requests for user-initiated renames of
// /// files.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class RenameFilesParams{

//     /// <summary>
//     /// An array of all files/folders renamed in this operation. When a folder is renamed, only
//     /// the folder will be included, and not its children.
//     /// </summary>
//     public List<FileRename> files { get; set; }
// }

// /// <summary>
// /// The parameters sent in notifications/requests for user-initiated deletes of
// /// files.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class DeleteFilesParams{

//     /// <summary>
//     /// An array of all files/folders deleted in this operation.
//     /// </summary>
//     public List<FileDelete> files { get; set; }
// }


// public class MonikerParams: ITextDocumentPositionParams,IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// The text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position inside the text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }
// }

// /// <summary>
// /// Moniker definition to match LSIF 0.5 moniker definition.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class Moniker{

//     /// <summary>
//     /// The scheme of the moniker. For example tsc or .Net
//     /// </summary>
//     public string scheme { get; set; }

//     /// <summary>
//     /// The identifier of the moniker. The value is opaque in LSIF however
//     /// schema owners are allowed to define the structure if they want.
//     /// </summary>
//     public string identifier { get; set; }

//     /// <summary>
//     /// The scope in which the moniker is unique
//     /// </summary>
//     public string unique { get; set; }

//     /// <summary>
//     /// The moniker kind if known.
//     /// </summary>
//     public string? kind { get; set; }
// }


// public class MonikerRegistrationOptions: ITextDocumentRegistrationOptions,IMonikerOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from MonikerOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// The parameter of a `textDocument/prepareTypeHierarchy` request.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class TypeHierarchyPrepareParams: ITextDocumentPositionParams,IWorkDoneProgressParams{

//     /// <summary>
//     /// The text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position inside the text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }
// }

// /// <summary>
// /// @since 3.17.0
// /// </summary>
// public class TypeHierarchyItem{

//     /// <summary>
//     /// The name of this item.
//     /// </summary>
//     public string name { get; set; }

//     /// <summary>
//     /// The kind of this item.
//     /// </summary>
//     public uint kind { get; set; }

//     /// <summary>
//     /// Tags for this item.
//     /// </summary>
//     public List<SymbolTag>? tags { get; set; }

//     /// <summary>
//     /// More detail for this item, e.g. the signature of a function.
//     /// </summary>
//     public string? detail { get; set; }

//     /// <summary>
//     /// The resource identifier of this item.
//     /// </summary>
//     public DocumentUri uri { get; set; }

//     /// <summary>
//     /// The range enclosing this symbol not including leading/trailing whitespace
//     /// but everything else, e.g. comments and code.
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// The range that should be selected and revealed when this symbol is being
//     /// picked, e.g. the name of a function. Must be contained by the
//     /// {@link TypeHierarchyItem.range `range`}.
//     /// </summary>
//     public Range selectionRange { get; set; }

//     /// <summary>
//     /// A data entry field that is preserved between a type hierarchy prepare and
//     /// supertypes or subtypes requests. It could also be used to identify the
//     /// type hierarchy in the server, helping improve the performance on
//     /// resolving supertypes and subtypes.
//     /// </summary>
//     public LSPAny? data { get; set; }
// }

// /// <summary>
// /// Type hierarchy options used during static or dynamic registration.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class TypeHierarchyRegistrationOptions: ITextDocumentRegistrationOptions,ITypeHierarchyOptions,IStaticRegistrationOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from TypeHierarchyOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again. See also Registration#id.
//     /// extend from StaticRegistrationOptions
//     /// </summary>
//     public string? id { get; set; }
// }

// /// <summary>
// /// The parameter of a `typeHierarchy/supertypes` request.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class TypeHierarchySupertypesParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

    
//     public TypeHierarchyItem item { get; set; }
// }

// /// <summary>
// /// The parameter of a `typeHierarchy/subtypes` request.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class TypeHierarchySubtypesParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

    
//     public TypeHierarchyItem item { get; set; }
// }

// /// <summary>
// /// A parameter literal used in inline value requests.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class InlineValueParams: IWorkDoneProgressParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// The text document.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The document range for which inline values should be computed.
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// Additional information about the context in which inline values were
//     /// requested.
//     /// </summary>
//     public InlineValueContext context { get; set; }
// }

// /// <summary>
// /// Inline value options used during static or dynamic registration.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class InlineValueRegistrationOptions: IInlineValueOptions,ITextDocumentRegistrationOptions,IStaticRegistrationOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from InlineValueOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again. See also Registration#id.
//     /// extend from StaticRegistrationOptions
//     /// </summary>
//     public string? id { get; set; }
// }

// /// <summary>
// /// A parameter literal used in inlay hint requests.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class InlayHintParams: IWorkDoneProgressParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// The text document.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The document range for which inlay hints should be computed.
//     /// </summary>
//     public Range range { get; set; }
// }

// /// <summary>
// /// Inlay hint information.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class InlayHint{

//     /// <summary>
//     /// The position of this hint.
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// The label of this hint. A human readable string or an array of
//     /// InlayHintLabelPart label parts.
//     /// 
//     /// *Note* that neither the string nor the label part can be empty.
//     /// </summary>
//     public MyNode label { get; set; }

//     /// <summary>
//     /// The kind of this hint. Can be omitted in which case the client
//     /// should fall back to a reasonable default.
//     /// </summary>
//     public uint? kind { get; set; }

//     /// <summary>
//     /// Optional text edits that are performed when accepting this inlay hint.
//     /// 
//     /// *Note* that edits are expected to change the document so that the inlay
//     /// hint (or its nearest variant) is now part of the document and the inlay
//     /// hint itself is now obsolete.
//     /// </summary>
//     public List<TextEdit>? textEdits { get; set; }

//     /// <summary>
//     /// The tooltip text when you hover over this item.
//     /// </summary>
//     public MyNode? tooltip { get; set; }

//     /// <summary>
//     /// Render padding before the hint.
//     /// 
//     /// Note: Padding should use the editor's background color, not the
//     /// background color of the hint itself. That means padding can be used
//     /// to visually align/separate an inlay hint.
//     /// </summary>
//     public bool? paddingLeft { get; set; }

//     /// <summary>
//     /// Render padding after the hint.
//     /// 
//     /// Note: Padding should use the editor's background color, not the
//     /// background color of the hint itself. That means padding can be used
//     /// to visually align/separate an inlay hint.
//     /// </summary>
//     public bool? paddingRight { get; set; }

//     /// <summary>
//     /// A data entry field that is preserved on an inlay hint between
//     /// a `textDocument/inlayHint` and a `inlayHint/resolve` request.
//     /// </summary>
//     public LSPAny? data { get; set; }
// }

// /// <summary>
// /// Inlay hint options used during static or dynamic registration.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class InlayHintRegistrationOptions: IInlayHintOptions,ITextDocumentRegistrationOptions,IStaticRegistrationOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from InlayHintOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The server provides support to resolve additional
//     /// information for an inlay hint item.
//     /// extend from InlayHintOptions
//     /// </summary>
//     public bool? resolveProvider { get; set; }

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again. See also Registration#id.
//     /// extend from StaticRegistrationOptions
//     /// </summary>
//     public string? id { get; set; }
// }

// /// <summary>
// /// Parameters of the document diagnostic request.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class DocumentDiagnosticParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

//     /// <summary>
//     /// The text document.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The additional identifier  provided during registration.
//     /// </summary>
//     public string? identifier { get; set; }

//     /// <summary>
//     /// The result id of a previous response if provided.
//     /// </summary>
//     public string? previousResultId { get; set; }
// }

// /// <summary>
// /// A partial result for a document diagnostic report.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class DocumentDiagnosticReportPartialResult{

    
//     public Dictionary<DocumentUri,MyNode> relatedDocuments { get; set; }
// }

// /// <summary>
// /// Cancellation data returned from a diagnostic request.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class DiagnosticServerCancellationData{

    
//     public bool retriggerRequest { get; set; }
// }

// /// <summary>
// /// Diagnostic registration options.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class DiagnosticRegistrationOptions: ITextDocumentRegistrationOptions,IDiagnosticOptions,IStaticRegistrationOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from DiagnosticOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// An optional identifier under which the diagnostics are
//     /// managed by the client.
//     /// extend from DiagnosticOptions
//     /// </summary>
//     public string? identifier { get; set; }

//     /// <summary>
//     /// Whether the language has inter file dependencies meaning that
//     /// editing code in one file can result in a different diagnostic
//     /// set in another file. Inter file dependencies are common for
//     /// most programming languages and typically uncommon for linters.
//     /// extend from DiagnosticOptions
//     /// </summary>
//     public bool interFileDependencies { get; set; }

//     /// <summary>
//     /// The server provides support for workspace diagnostics as well.
//     /// extend from DiagnosticOptions
//     /// </summary>
//     public bool workspaceDiagnostics { get; set; }

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again. See also Registration#id.
//     /// extend from StaticRegistrationOptions
//     /// </summary>
//     public string? id { get; set; }
// }

// /// <summary>
// /// Parameters of the workspace diagnostic request.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class WorkspaceDiagnosticParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

//     /// <summary>
//     /// The additional identifier provided during registration.
//     /// </summary>
//     public string? identifier { get; set; }

//     /// <summary>
//     /// The currently known diagnostic reports with their
//     /// previous result ids.
//     /// </summary>
//     public List<PreviousResultId> previousResultIds { get; set; }
// }

// /// <summary>
// /// A workspace diagnostic report.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class WorkspaceDiagnosticReport{

    
//     public List<WorkspaceDocumentDiagnosticReport> items { get; set; }
// }

// /// <summary>
// /// A partial result for a workspace diagnostic report.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class WorkspaceDiagnosticReportPartialResult{

    
//     public List<WorkspaceDocumentDiagnosticReport> items { get; set; }
// }

// /// <summary>
// /// The params sent in an open notebook document notification.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class DidOpenNotebookDocumentParams{

//     /// <summary>
//     /// The notebook document that got opened.
//     /// </summary>
//     public NotebookDocument notebookDocument { get; set; }

//     /// <summary>
//     /// The text documents that represent the content
//     /// of a notebook cell.
//     /// </summary>
//     public List<TextDocumentItem> cellTextDocuments { get; set; }
// }

// /// <summary>
// /// The params sent in a change notebook document notification.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class DidChangeNotebookDocumentParams{

//     /// <summary>
//     /// The notebook document that did change. The version number points
//     /// to the version after all provided changes have been applied. If
//     /// only the text document content of a cell changes the notebook version
//     /// doesn't necessarily have to change.
//     /// </summary>
//     public VersionedNotebookDocumentIdentifier notebookDocument { get; set; }

//     /// <summary>
//     /// The actual changes to the notebook document.
//     /// 
//     /// The changes describe single state changes to the notebook document.
//     /// So if there are two changes c1 (at array index 0) and c2 (at array
//     /// index 1) for a notebook in state S then c1 moves the notebook from
//     /// S to S' and c2 from S' to S''. So c1 is computed on the state S and
//     /// c2 is computed on the state S'.
//     /// 
//     /// To mirror the content of a notebook using change events use the following approach:
//     /// - start with the same initial content
//     /// - apply the 'notebookDocument/didChange' notifications in the order you receive them.
//     /// - apply the `NotebookChangeEvent`s in a single notification in the order
//     ///   you receive them.
//     /// </summary>
//     public NotebookDocumentChangeEvent change { get; set; }
// }

// /// <summary>
// /// The params sent in a save notebook document notification.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class DidSaveNotebookDocumentParams{

//     /// <summary>
//     /// The notebook document that got saved.
//     /// </summary>
//     public NotebookDocumentIdentifier notebookDocument { get; set; }
// }

// /// <summary>
// /// The params sent in a close notebook document notification.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class DidCloseNotebookDocumentParams{

//     /// <summary>
//     /// The notebook document that got closed.
//     /// </summary>
//     public NotebookDocumentIdentifier notebookDocument { get; set; }

//     /// <summary>
//     /// The text documents that represent the content
//     /// of a notebook cell that got closed.
//     /// </summary>
//     public List<TextDocumentIdentifier> cellTextDocuments { get; set; }
// }


// public class RegistrationParams{

    
//     public List<Registration> registrations { get; set; }
// }


// public class UnregistrationParams{

    
//     public List<Unregistration> unregisterations { get; set; }
// }


// public class InitializeParams: I_InitializeParams,IWorkspaceFoldersInitializeParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// extend from _InitializeParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// The process Id of the parent process that started
//     /// the server.
//     /// 
//     /// Is `null` if the process has not been started by another process.
//     /// If the parent process is not alive then the server should exit.
//     /// extend from _InitializeParams
//     /// </summary>
//     public MyNode processId { get; set; }

//     /// <summary>
//     /// Information about the client
//     /// 
//     /// @since 3.15.0
//     /// extend from _InitializeParams
//     /// </summary>
//     public MyNode? clientInfo { get; set; }

//     /// <summary>
//     /// The locale the client is currently showing the user interface
//     /// in. This must not necessarily be the locale of the operating
//     /// system.
//     /// 
//     /// Uses IETF language tags as the value's syntax
//     /// (See https://en.wikipedia.org/wiki/IETF_language_tag)
//     /// 
//     /// @since 3.16.0
//     /// extend from _InitializeParams
//     /// </summary>
//     public string? locale { get; set; }

//     /// <summary>
//     /// The rootPath of the workspace. Is null
//     /// if no folder is open.
//     /// 
//     /// @deprecated in favour of rootUri.
//     /// extend from _InitializeParams
//     /// </summary>
//     public MyNode? rootPath { get; set; }

//     /// <summary>
//     /// The rootUri of the workspace. Is null if no
//     /// folder is open. If both `rootPath` and `rootUri` are set
//     /// `rootUri` wins.
//     /// 
//     /// @deprecated in favour of workspaceFolders.
//     /// extend from _InitializeParams
//     /// </summary>
//     public MyNode rootUri { get; set; }

//     /// <summary>
//     /// The capabilities provided by the client (editor or tool)
//     /// extend from _InitializeParams
//     /// </summary>
//     public ClientCapabilities capabilities { get; set; }

//     /// <summary>
//     /// User provided initialization options.
//     /// extend from _InitializeParams
//     /// </summary>
//     public LSPAny? initializationOptions { get; set; }

//     /// <summary>
//     /// The initial trace setting. If omitted trace is disabled ('off').
//     /// extend from _InitializeParams
//     /// </summary>
//     public string? trace { get; set; }

//     /// <summary>
//     /// The workspace folders configured in the client when the server starts.
//     /// 
//     /// This property is only available if the client supports workspace folders.
//     /// It can be `null` if the client supports workspace folders but none are
//     /// configured.
//     /// 
//     /// @since 3.6.0
//     /// extend from WorkspaceFoldersInitializeParams
//     /// </summary>
//     public MyNode? workspaceFolders { get; set; }
// }

// /// <summary>
// /// The result returned from an initialize request.
// /// </summary>
// public class InitializeResult{

//     /// <summary>
//     /// The capabilities the language server provides.
//     /// </summary>
//     public ServerCapabilities capabilities { get; set; }

//     /// <summary>
//     /// Information about the server.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public MyNode? serverInfo { get; set; }
// }

// /// <summary>
// /// The data type of the ResponseError if the
// /// initialize request fails.
// /// </summary>
// public class InitializeError{

//     /// <summary>
//     /// Indicates whether the client execute the following retry logic:
//     /// (1) show the message provided by the ResponseError to the user
//     /// (2) user selects retry or cancel
//     /// (3) if user selected retry the initialize method is sent again.
//     /// </summary>
//     public bool retry { get; set; }
// }


// public class InitializedParams{
// }

// /// <summary>
// /// The parameters of a change configuration notification.
// /// </summary>
// public class DidChangeConfigurationParams{

//     /// <summary>
//     /// The actual changed settings
//     /// </summary>
//     public LSPAny settings { get; set; }
// }


// public class DidChangeConfigurationRegistrationOptions{

    
//     public MyNode? section { get; set; }
// }

// /// <summary>
// /// The parameters of a notification message.
// /// </summary>
// public class ShowMessageParams{

//     /// <summary>
//     /// The message type. See {@link MessageType}
//     /// </summary>
//     public uint type { get; set; }

//     /// <summary>
//     /// The actual message.
//     /// </summary>
//     public string message { get; set; }
// }


// public class ShowMessageRequestParams{

//     /// <summary>
//     /// The message type. See {@link MessageType}
//     /// </summary>
//     public uint type { get; set; }

//     /// <summary>
//     /// The actual message.
//     /// </summary>
//     public string message { get; set; }

//     /// <summary>
//     /// The message action items to present.
//     /// </summary>
//     public List<MessageActionItem>? actions { get; set; }
// }


// public class MessageActionItem{

//     /// <summary>
//     /// A short title like 'Retry', 'Open Log' etc.
//     /// </summary>
//     public string title { get; set; }
// }

// /// <summary>
// /// The log message parameters.
// /// </summary>
// public class LogMessageParams{

//     /// <summary>
//     /// The message type. See {@link MessageType}
//     /// </summary>
//     public uint type { get; set; }

//     /// <summary>
//     /// The actual message.
//     /// </summary>
//     public string message { get; set; }
// }

// /// <summary>
// /// The parameters sent in an open text document notification
// /// </summary>
// public class DidOpenTextDocumentParams{

//     /// <summary>
//     /// The document that was opened.
//     /// </summary>
//     public TextDocumentItem textDocument { get; set; }
// }

// /// <summary>
// /// The change text document notification's parameters.
// /// </summary>
// public class DidChangeTextDocumentParams{

//     /// <summary>
//     /// The document that did change. The version number points
//     /// to the version after all provided content changes have
//     /// been applied.
//     /// </summary>
//     public VersionedTextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The actual content changes. The content changes describe single state changes
//     /// to the document. So if there are two content changes c1 (at array index 0) and
//     /// c2 (at array index 1) for a document in state S then c1 moves the document from
//     /// S to S' and c2 from S' to S''. So c1 is computed on the state S and c2 is computed
//     /// on the state S'.
//     /// 
//     /// To mirror the content of a document using change events use the following approach:
//     /// - start with the same initial content
//     /// - apply the 'textDocument/didChange' notifications in the order you receive them.
//     /// - apply the `TextDocumentContentChangeEvent`s in a single notification in the order
//     ///   you receive them.
//     /// </summary>
//     public List<TextDocumentContentChangeEvent> contentChanges { get; set; }
// }

// /// <summary>
// /// Describe options to be used when registered for text document change events.
// /// </summary>
// public class TextDocumentChangeRegistrationOptions: ITextDocumentRegistrationOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// How documents are synced to the server.
//     /// </summary>
//     public uint syncKind { get; set; }
// }

// /// <summary>
// /// The parameters sent in a close text document notification
// /// </summary>
// public class DidCloseTextDocumentParams{

//     /// <summary>
//     /// The document that was closed.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }
// }

// /// <summary>
// /// The parameters sent in a save text document notification
// /// </summary>
// public class DidSaveTextDocumentParams{

//     /// <summary>
//     /// The document that was saved.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// Optional the content when saved. Depends on the includeText value
//     /// when the save notification was requested.
//     /// </summary>
//     public string? text { get; set; }
// }

// /// <summary>
// /// Save registration options.
// /// </summary>
// public class TextDocumentSaveRegistrationOptions: ITextDocumentRegistrationOptions,ISaveOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// The client is supposed to include the content on save.
//     /// extend from SaveOptions
//     /// </summary>
//     public bool? includeText { get; set; }
// }

// /// <summary>
// /// The parameters sent in a will save text document notification.
// /// </summary>
// public class WillSaveTextDocumentParams{

//     /// <summary>
//     /// The document that will be saved.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The 'TextDocumentSaveReason'.
//     /// </summary>
//     public uint reason { get; set; }
// }

// /// <summary>
// /// A text edit applicable to a text document.
// /// </summary>
// public interface ITextEdit {

//     /// <summary>
//     /// The range of the text document to be manipulated. To insert
//     /// text into a document create a range where start === end.
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// The string to be inserted. For delete operations use an
//     /// empty string.
//     /// </summary>
//     public string newText { get; set; }
// }

// /// <summary>
// /// A text edit applicable to a text document.
// /// </summary>
// public class TextEdit: ITextEdit{

//     /// <summary>
//     /// The range of the text document to be manipulated. To insert
//     /// text into a document create a range where start === end.
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// The string to be inserted. For delete operations use an
//     /// empty string.
//     /// </summary>
//     public string newText { get; set; }
// }

// /// <summary>
// /// The watched files change notification's parameters.
// /// </summary>
// public class DidChangeWatchedFilesParams{

//     /// <summary>
//     /// The actual file events.
//     /// </summary>
//     public List<FileEvent> changes { get; set; }
// }

// /// <summary>
// /// Describe options to be used when registered for text document change events.
// /// </summary>
// public class DidChangeWatchedFilesRegistrationOptions{

//     /// <summary>
//     /// The watchers to register.
//     /// </summary>
//     public List<FileSystemWatcher> watchers { get; set; }
// }

// /// <summary>
// /// The publish diagnostic notification's parameters.
// /// </summary>
// public class PublishDiagnosticsParams{

//     /// <summary>
//     /// The URI for which diagnostic information is reported.
//     /// </summary>
//     public DocumentUri uri { get; set; }

//     /// <summary>
//     /// Optional the version number of the document the diagnostics are published for.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public int? version { get; set; }

//     /// <summary>
//     /// An array of diagnostic information items.
//     /// </summary>
//     public List<Diagnostic> diagnostics { get; set; }
// }

// /// <summary>
// /// Completion parameters
// /// </summary>
// public class CompletionParams: ITextDocumentPositionParams,IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// The text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position inside the text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

//     /// <summary>
//     /// The completion context. This is only available it the client specifies
//     /// to send this using the client capability `textDocument.completion.contextSupport === true`
//     /// </summary>
//     public CompletionContext? context { get; set; }
// }

// /// <summary>
// /// A completion item represents a text snippet that is
// /// proposed to complete text that is being typed.
// /// </summary>
// public class CompletionItem{

//     /// <summary>
//     /// The label of this completion item.
//     /// 
//     /// The label property is also by default the text that
//     /// is inserted when selecting this completion.
//     /// 
//     /// If label details are provided the label itself should
//     /// be an unqualified name of the completion item.
//     /// </summary>
//     public string label { get; set; }

//     /// <summary>
//     /// Additional details for the label
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public CompletionItemLabelDetails? labelDetails { get; set; }

//     /// <summary>
//     /// The kind of this completion item. Based of the kind
//     /// an icon is chosen by the editor.
//     /// </summary>
//     public uint? kind { get; set; }

//     /// <summary>
//     /// Tags for this completion item.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public List<CompletionItemTag>? tags { get; set; }

//     /// <summary>
//     /// A human-readable string with additional information
//     /// about this item, like type or symbol information.
//     /// </summary>
//     public string? detail { get; set; }

//     /// <summary>
//     /// A human-readable string that represents a doc-comment.
//     /// </summary>
//     public MyNode? documentation { get; set; }

//     /// <summary>
//     /// Indicates if this item is deprecated.
//     /// @deprecated Use `tags` instead.
//     /// </summary>
//     public bool? deprecated { get; set; }

//     /// <summary>
//     /// Select this item when showing.
//     /// 
//     /// *Note* that only one completion item can be selected and that the
//     /// tool / client decides which item that is. The rule is that the *first*
//     /// item of those that match best is selected.
//     /// </summary>
//     public bool? preselect { get; set; }

//     /// <summary>
//     /// A string that should be used when comparing this item
//     /// with other items. When `falsy` the {@link CompletionItem.label label}
//     /// is used.
//     /// </summary>
//     public string? sortText { get; set; }

//     /// <summary>
//     /// A string that should be used when filtering a set of
//     /// completion items. When `falsy` the {@link CompletionItem.label label}
//     /// is used.
//     /// </summary>
//     public string? filterText { get; set; }

//     /// <summary>
//     /// A string that should be inserted into a document when selecting
//     /// this completion. When `falsy` the {@link CompletionItem.label label}
//     /// is used.
//     /// 
//     /// The `insertText` is subject to interpretation by the client side.
//     /// Some tools might not take the string literally. For example
//     /// VS Code when code complete is requested in this example
//     /// `con<cursor position>` and a completion item with an `insertText` of
//     /// `console` is provided it will only insert `sole`. Therefore it is
//     /// recommended to use `textEdit` instead since it avoids additional client
//     /// side interpretation.
//     /// </summary>
//     public string? insertText { get; set; }

//     /// <summary>
//     /// The format of the insert text. The format applies to both the
//     /// `insertText` property and the `newText` property of a provided
//     /// `textEdit`. If omitted defaults to `InsertTextFormat.PlainText`.
//     /// 
//     /// Please note that the insertTextFormat doesn't apply to
//     /// `additionalTextEdits`.
//     /// </summary>
//     public uint? insertTextFormat { get; set; }

//     /// <summary>
//     /// How whitespace and indentation is handled during completion
//     /// item insertion. If not provided the clients default value depends on
//     /// the `textDocument.completion.insertTextMode` client capability.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public uint? insertTextMode { get; set; }

//     /// <summary>
//     /// An {@link TextEdit edit} which is applied to a document when selecting
//     /// this completion. When an edit is provided the value of
//     /// {@link CompletionItem.insertText insertText} is ignored.
//     /// 
//     /// Most editors support two different operations when accepting a completion
//     /// item. One is to insert a completion text and the other is to replace an
//     /// existing text with a completion text. Since this can usually not be
//     /// predetermined by a server it can report both ranges. Clients need to
//     /// signal support for `InsertReplaceEdits` via the
//     /// `textDocument.completion.insertReplaceSupport` client capability
//     /// property.
//     /// 
//     /// *Note 1:* The text edit's range as well as both ranges from an insert
//     /// replace edit must be a [single line] and they must contain the position
//     /// at which completion has been requested.
//     /// *Note 2:* If an `InsertReplaceEdit` is returned the edit's insert range
//     /// must be a prefix of the edit's replace range, that means it must be
//     /// contained and starting at the same position.
//     /// 
//     /// @since 3.16.0 additional type `InsertReplaceEdit`
//     /// </summary>
//     public MyNode? textEdit { get; set; }

//     /// <summary>
//     /// The edit text used if the completion item is part of a CompletionList and
//     /// CompletionList defines an item default for the text edit range.
//     /// 
//     /// Clients will only honor this property if they opt into completion list
//     /// item defaults using the capability `completionList.itemDefaults`.
//     /// 
//     /// If not provided and a list's default range is provided the label
//     /// property is used as a text.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public string? textEditText { get; set; }

//     /// <summary>
//     /// An optional array of additional {@link TextEdit text edits} that are applied when
//     /// selecting this completion. Edits must not overlap (including the same insert position)
//     /// with the main {@link CompletionItem.textEdit edit} nor with themselves.
//     /// 
//     /// Additional text edits should be used to change text unrelated to the current cursor position
//     /// (for example adding an import statement at the top of the file if the completion item will
//     /// insert an unqualified type).
//     /// </summary>
//     public List<TextEdit>? additionalTextEdits { get; set; }

//     /// <summary>
//     /// An optional set of characters that when pressed while this completion is active will accept it first and
//     /// then type that character. *Note* that all commit characters should have `length=1` and that superfluous
//     /// characters will be ignored.
//     /// </summary>
//     public List<string>? commitCharacters { get; set; }

//     /// <summary>
//     /// An optional {@link Command command} that is executed *after* inserting this completion. *Note* that
//     /// additional modifications to the current document should be described with the
//     /// {@link CompletionItem.additionalTextEdits additionalTextEdits}-property.
//     /// </summary>
//     public Command? command { get; set; }

//     /// <summary>
//     /// A data entry field that is preserved on a completion item between a
//     /// {@link CompletionRequest} and a {@link CompletionResolveRequest}.
//     /// </summary>
//     public LSPAny? data { get; set; }
// }

// /// <summary>
// /// Represents a collection of {@link CompletionItem completion items} to be presented
// /// in the editor.
// /// </summary>
// public class CompletionList{

//     /// <summary>
//     /// This list it not complete. Further typing results in recomputing this list.
//     /// 
//     /// Recomputed lists have all their items replaced (not appended) in the
//     /// incomplete completion sessions.
//     /// </summary>
//     public bool isIncomplete { get; set; }

//     /// <summary>
//     /// In many cases the items of an actual completion result share the same
//     /// value for properties like `commitCharacters` or the range of a text
//     /// edit. A completion list can therefore define item defaults which will
//     /// be used if a completion item itself doesn't specify the value.
//     /// 
//     /// If a completion list specifies a default value and a completion item
//     /// also specifies a corresponding value the one from the item is used.
//     /// 
//     /// Servers are only allowed to return default values if the client
//     /// signals support for this via the `completionList.itemDefaults`
//     /// capability.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public MyNode? itemDefaults { get; set; }

//     /// <summary>
//     /// The completion items.
//     /// </summary>
//     public List<CompletionItem> items { get; set; }
// }

// /// <summary>
// /// Registration options for a {@link CompletionRequest}.
// /// </summary>
// public class CompletionRegistrationOptions: ITextDocumentRegistrationOptions,ICompletionOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from CompletionOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// Most tools trigger completion request automatically without explicitly requesting
//     /// it using a keyboard shortcut (e.g. Ctrl+Space). Typically they do so when the user
//     /// starts to type an identifier. For example if the user types `c` in a JavaScript file
//     /// code complete will automatically pop up present `console` besides others as a
//     /// completion item. Characters that make up identifiers don't need to be listed here.
//     /// 
//     /// If code complete should automatically be trigger on characters not being valid inside
//     /// an identifier (for example `.` in JavaScript) list them in `triggerCharacters`.
//     /// extend from CompletionOptions
//     /// </summary>
//     public List<string>? triggerCharacters { get; set; }

//     /// <summary>
//     /// The list of all possible characters that commit a completion. This field can be used
//     /// if clients don't support individual commit characters per completion item. See
//     /// `ClientCapabilities.textDocument.completion.completionItem.commitCharactersSupport`
//     /// 
//     /// If a server provides both `allCommitCharacters` and commit characters on an individual
//     /// completion item the ones on the completion item win.
//     /// 
//     /// @since 3.2.0
//     /// extend from CompletionOptions
//     /// </summary>
//     public List<string>? allCommitCharacters { get; set; }

//     /// <summary>
//     /// The server provides support to resolve additional
//     /// information for a completion item.
//     /// extend from CompletionOptions
//     /// </summary>
//     public bool? resolveProvider { get; set; }

//     /// <summary>
//     /// The server supports the following `CompletionItem` specific
//     /// capabilities.
//     /// 
//     /// @since 3.17.0
//     /// extend from CompletionOptions
//     /// </summary>
//     public MyNode? completionItem { get; set; }
// }

// /// <summary>
// /// Parameters for a {@link HoverRequest}.
// /// </summary>
// public class HoverParams: ITextDocumentPositionParams,IWorkDoneProgressParams{

//     /// <summary>
//     /// The text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position inside the text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }
// }

// /// <summary>
// /// The result of a hover request.
// /// </summary>
// public class Hover{

//     /// <summary>
//     /// The hover's content
//     /// </summary>
//     public MyNode contents { get; set; }

//     /// <summary>
//     /// An optional range inside the text document that is used to
//     /// visualize the hover, e.g. by changing the background color.
//     /// </summary>
//     public Range? range { get; set; }
// }

// /// <summary>
// /// Registration options for a {@link HoverRequest}.
// /// </summary>
// public class HoverRegistrationOptions: ITextDocumentRegistrationOptions,IHoverOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from HoverOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Parameters for a {@link SignatureHelpRequest}.
// /// </summary>
// public class SignatureHelpParams: ITextDocumentPositionParams,IWorkDoneProgressParams{

//     /// <summary>
//     /// The text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position inside the text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// The signature help context. This is only available if the client specifies
//     /// to send this using the client capability `textDocument.signatureHelp.contextSupport === true`
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public SignatureHelpContext? context { get; set; }
// }

// /// <summary>
// /// Signature help represents the signature of something
// /// callable. There can be multiple signature but only one
// /// active and only one active parameter.
// /// </summary>
// public class SignatureHelp{

//     /// <summary>
//     /// One or more signatures.
//     /// </summary>
//     public List<SignatureInformation> signatures { get; set; }

//     /// <summary>
//     /// The active signature. If omitted or the value lies outside the
//     /// range of `signatures` the value defaults to zero or is ignored if
//     /// the `SignatureHelp` has no signatures.
//     /// 
//     /// Whenever possible implementors should make an active decision about
//     /// the active signature and shouldn't rely on a default value.
//     /// 
//     /// In future version of the protocol this property might become
//     /// mandatory to better express this.
//     /// </summary>
//     public uint? activeSignature { get; set; }

//     /// <summary>
//     /// The active parameter of the active signature. If omitted or the value
//     /// lies outside the range of `signatures[activeSignature].parameters`
//     /// defaults to 0 if the active signature has parameters. If
//     /// the active signature has no parameters it is ignored.
//     /// In future version of the protocol this property might become
//     /// mandatory to better express the active parameter if the
//     /// active signature does have any.
//     /// </summary>
//     public uint? activeParameter { get; set; }
// }

// /// <summary>
// /// Registration options for a {@link SignatureHelpRequest}.
// /// </summary>
// public class SignatureHelpRegistrationOptions: ITextDocumentRegistrationOptions,ISignatureHelpOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from SignatureHelpOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// List of characters that trigger signature help automatically.
//     /// extend from SignatureHelpOptions
//     /// </summary>
//     public List<string>? triggerCharacters { get; set; }

//     /// <summary>
//     /// List of characters that re-trigger signature help.
//     /// 
//     /// These trigger characters are only active when signature help is already showing. All trigger characters
//     /// are also counted as re-trigger characters.
//     /// 
//     /// @since 3.15.0
//     /// extend from SignatureHelpOptions
//     /// </summary>
//     public List<string>? retriggerCharacters { get; set; }
// }

// /// <summary>
// /// Parameters for a {@link DefinitionRequest}.
// /// </summary>
// public class DefinitionParams: ITextDocumentPositionParams,IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// The text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position inside the text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }
// }

// /// <summary>
// /// Registration options for a {@link DefinitionRequest}.
// /// </summary>
// public class DefinitionRegistrationOptions: ITextDocumentRegistrationOptions,IDefinitionOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from DefinitionOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Parameters for a {@link ReferencesRequest}.
// /// </summary>
// public class ReferenceParams: ITextDocumentPositionParams,IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// The text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position inside the text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

    
//     public ReferenceContext context { get; set; }
// }

// /// <summary>
// /// Registration options for a {@link ReferencesRequest}.
// /// </summary>
// public class ReferenceRegistrationOptions: ITextDocumentRegistrationOptions,IReferenceOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from ReferenceOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Parameters for a {@link DocumentHighlightRequest}.
// /// </summary>
// public class DocumentHighlightParams: ITextDocumentPositionParams,IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// The text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position inside the text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }
// }

// /// <summary>
// /// A document highlight is a range inside a text document which deserves
// /// special attention. Usually a document highlight is visualized by changing
// /// the background color of its range.
// /// </summary>
// public class DocumentHighlight{

//     /// <summary>
//     /// The range this highlight applies to.
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// The highlight kind, default is {@link DocumentHighlightKind.Text text}.
//     /// </summary>
//     public uint? kind { get; set; }
// }

// /// <summary>
// /// Registration options for a {@link DocumentHighlightRequest}.
// /// </summary>
// public class DocumentHighlightRegistrationOptions: ITextDocumentRegistrationOptions,IDocumentHighlightOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from DocumentHighlightOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Parameters for a {@link DocumentSymbolRequest}.
// /// </summary>
// public class DocumentSymbolParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

//     /// <summary>
//     /// The text document.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }
// }

// /// <summary>
// /// Represents information about programming constructs like variables, classes,
// /// interfaces etc.
// /// </summary>
// public class SymbolInformation: IBaseSymbolInformation{

//     /// <summary>
//     /// The name of this symbol.
//     /// extend from BaseSymbolInformation
//     /// </summary>
//     public string name { get; set; }

//     /// <summary>
//     /// The kind of this symbol.
//     /// extend from BaseSymbolInformation
//     /// </summary>
//     public uint kind { get; set; }

//     /// <summary>
//     /// Tags for this symbol.
//     /// 
//     /// @since 3.16.0
//     /// extend from BaseSymbolInformation
//     /// </summary>
//     public List<SymbolTag>? tags { get; set; }

//     /// <summary>
//     /// The name of the symbol containing this symbol. This information is for
//     /// user interface purposes (e.g. to render a qualifier in the user interface
//     /// if necessary). It can't be used to re-infer a hierarchy for the document
//     /// symbols.
//     /// extend from BaseSymbolInformation
//     /// </summary>
//     public string? containerName { get; set; }

//     /// <summary>
//     /// Indicates if this symbol is deprecated.
//     /// 
//     /// @deprecated Use tags instead
//     /// </summary>
//     public bool? deprecated { get; set; }

//     /// <summary>
//     /// The location of this symbol. The location's range is used by a tool
//     /// to reveal the location in the editor. If the symbol is selected in the
//     /// tool the range's start information is used to position the cursor. So
//     /// the range usually spans more than the actual symbol's name and does
//     /// normally include things like visibility modifiers.
//     /// 
//     /// The range doesn't have to denote a node range in the sense of an abstract
//     /// syntax tree. It can therefore not be used to re-construct a hierarchy of
//     /// the symbols.
//     /// </summary>
//     public Location location { get; set; }
// }

// /// <summary>
// /// Represents programming constructs like variables, classes, interfaces etc.
// /// that appear in a document. Document symbols can be hierarchical and they
// /// have two ranges: one that encloses its definition and one that points to
// /// its most interesting range, e.g. the range of an identifier.
// /// </summary>
// public class DocumentSymbol{

//     /// <summary>
//     /// The name of this symbol. Will be displayed in the user interface and therefore must not be
//     /// an empty string or a string only consisting of white spaces.
//     /// </summary>
//     public string name { get; set; }

//     /// <summary>
//     /// More detail for this symbol, e.g the signature of a function.
//     /// </summary>
//     public string? detail { get; set; }

//     /// <summary>
//     /// The kind of this symbol.
//     /// </summary>
//     public uint kind { get; set; }

//     /// <summary>
//     /// Tags for this document symbol.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public List<SymbolTag>? tags { get; set; }

//     /// <summary>
//     /// Indicates if this symbol is deprecated.
//     /// 
//     /// @deprecated Use tags instead
//     /// </summary>
//     public bool? deprecated { get; set; }

//     /// <summary>
//     /// The range enclosing this symbol not including leading/trailing whitespace but everything else
//     /// like comments. This information is typically used to determine if the clients cursor is
//     /// inside the symbol to reveal in the symbol in the UI.
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// The range that should be selected and revealed when this symbol is being picked, e.g the name of a function.
//     /// Must be contained by the `range`.
//     /// </summary>
//     public Range selectionRange { get; set; }

//     /// <summary>
//     /// Children of this symbol, e.g. properties of a class.
//     /// </summary>
//     public List<DocumentSymbol>? children { get; set; }
// }

// /// <summary>
// /// Registration options for a {@link DocumentSymbolRequest}.
// /// </summary>
// public class DocumentSymbolRegistrationOptions: ITextDocumentRegistrationOptions,IDocumentSymbolOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from DocumentSymbolOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// A human-readable string that is shown when multiple outlines trees
//     /// are shown for the same document.
//     /// 
//     /// @since 3.16.0
//     /// extend from DocumentSymbolOptions
//     /// </summary>
//     public string? label { get; set; }
// }

// /// <summary>
// /// The parameters of a {@link CodeActionRequest}.
// /// </summary>
// public class CodeActionParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

//     /// <summary>
//     /// The document in which the command was invoked.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The range for which the command was invoked.
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// Context carrying additional information.
//     /// </summary>
//     public CodeActionContext context { get; set; }
// }

// /// <summary>
// /// Represents a reference to a command. Provides a title which
// /// will be used to represent a command in the UI and, optionally,
// /// an array of arguments which will be passed to the command handler
// /// function when invoked.
// /// </summary>
// public class Command{

//     /// <summary>
//     /// Title of the command, like `save`.
//     /// </summary>
//     public string title { get; set; }

//     /// <summary>
//     /// The identifier of the actual command handler.
//     /// </summary>
//     public string command { get; set; }

//     /// <summary>
//     /// Arguments that the command handler should be
//     /// invoked with.
//     /// </summary>
//     public List<LSPAny>? arguments { get; set; }
// }

// /// <summary>
// /// A code action represents a change that can be performed in code, e.g. to fix a problem or
// /// to refactor code.
// /// 
// /// A CodeAction must set either `edit` and/or a `command`. If both are supplied, the `edit` is applied first, then the `command` is executed.
// /// </summary>
// public class CodeAction{

//     /// <summary>
//     /// A short, human-readable, title for this code action.
//     /// </summary>
//     public string title { get; set; }

//     /// <summary>
//     /// The kind of the code action.
//     /// 
//     /// Used to filter code actions.
//     /// </summary>
//     public string? kind { get; set; }

//     /// <summary>
//     /// The diagnostics that this code action resolves.
//     /// </summary>
//     public List<Diagnostic>? diagnostics { get; set; }

//     /// <summary>
//     /// Marks this as a preferred action. Preferred actions are used by the `auto fix` command and can be targeted
//     /// by keybindings.
//     /// 
//     /// A quick fix should be marked preferred if it properly addresses the underlying error.
//     /// A refactoring should be marked preferred if it is the most reasonable choice of actions to take.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public bool? isPreferred { get; set; }

//     /// <summary>
//     /// Marks that the code action cannot currently be applied.
//     /// 
//     /// Clients should follow the following guidelines regarding disabled code actions:
//     /// 
//     ///   - Disabled code actions are not shown in automatic [lightbulbs](https://code.visualstudio.com/docs/editor/editingevolved#_code-action)
//     ///     code action menus.
//     /// 
//     ///   - Disabled actions are shown as faded out in the code action menu when the user requests a more specific type
//     ///     of code action, such as refactorings.
//     /// 
//     ///   - If the user has a [keybinding](https://code.visualstudio.com/docs/editor/refactoring#_keybindings-for-code-actions)
//     ///     that auto applies a code action and only disabled code actions are returned, the client should show the user an
//     ///     error message with `reason` in the editor.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public MyNode? disabled { get; set; }

//     /// <summary>
//     /// The workspace edit this code action performs.
//     /// </summary>
//     public WorkspaceEdit? edit { get; set; }

//     /// <summary>
//     /// A command this code action executes. If a code action
//     /// provides an edit and a command, first the edit is
//     /// executed and then the command.
//     /// </summary>
//     public Command? command { get; set; }

//     /// <summary>
//     /// A data entry field that is preserved on a code action between
//     /// a `textDocument/codeAction` and a `codeAction/resolve` request.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public LSPAny? data { get; set; }
// }

// /// <summary>
// /// Registration options for a {@link CodeActionRequest}.
// /// </summary>
// public class CodeActionRegistrationOptions: ITextDocumentRegistrationOptions,ICodeActionOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from CodeActionOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// CodeActionKinds that this server may return.
//     /// 
//     /// The list of kinds may be generic, such as `CodeActionKind.Refactor`, or the server
//     /// may list out every specific kind they provide.
//     /// extend from CodeActionOptions
//     /// </summary>
//     public List<CodeActionKind>? codeActionKinds { get; set; }

//     /// <summary>
//     /// The server provides support to resolve additional
//     /// information for a code action.
//     /// 
//     /// @since 3.16.0
//     /// extend from CodeActionOptions
//     /// </summary>
//     public bool? resolveProvider { get; set; }
// }

// /// <summary>
// /// The parameters of a {@link WorkspaceSymbolRequest}.
// /// </summary>
// public class WorkspaceSymbolParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

//     /// <summary>
//     /// A query string to filter symbols by. Clients may send an empty
//     /// string here to request all symbols.
//     /// </summary>
//     public string query { get; set; }
// }

// /// <summary>
// /// A special workspace symbol that supports locations without a range.
// /// 
// /// See also SymbolInformation.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class WorkspaceSymbol: IBaseSymbolInformation{

//     /// <summary>
//     /// The name of this symbol.
//     /// extend from BaseSymbolInformation
//     /// </summary>
//     public string name { get; set; }

//     /// <summary>
//     /// The kind of this symbol.
//     /// extend from BaseSymbolInformation
//     /// </summary>
//     public uint kind { get; set; }

//     /// <summary>
//     /// Tags for this symbol.
//     /// 
//     /// @since 3.16.0
//     /// extend from BaseSymbolInformation
//     /// </summary>
//     public List<SymbolTag>? tags { get; set; }

//     /// <summary>
//     /// The name of the symbol containing this symbol. This information is for
//     /// user interface purposes (e.g. to render a qualifier in the user interface
//     /// if necessary). It can't be used to re-infer a hierarchy for the document
//     /// symbols.
//     /// extend from BaseSymbolInformation
//     /// </summary>
//     public string? containerName { get; set; }

//     /// <summary>
//     /// The location of the symbol. Whether a server is allowed to
//     /// return a location without a range depends on the client
//     /// capability `workspace.symbol.resolveSupport`.
//     /// 
//     /// See SymbolInformation#location for more details.
//     /// </summary>
//     public MyNode location { get; set; }

//     /// <summary>
//     /// A data entry field that is preserved on a workspace symbol between a
//     /// workspace symbol request and a workspace symbol resolve request.
//     /// </summary>
//     public LSPAny? data { get; set; }
// }

// /// <summary>
// /// Registration options for a {@link WorkspaceSymbolRequest}.
// /// </summary>
// public class WorkspaceSymbolRegistrationOptions: IWorkspaceSymbolOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from WorkspaceSymbolOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The server provides support to resolve additional
//     /// information for a workspace symbol.
//     /// 
//     /// @since 3.17.0
//     /// extend from WorkspaceSymbolOptions
//     /// </summary>
//     public bool? resolveProvider { get; set; }
// }

// /// <summary>
// /// The parameters of a {@link CodeLensRequest}.
// /// </summary>
// public class CodeLensParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

//     /// <summary>
//     /// The document to request code lens for.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }
// }

// /// <summary>
// /// A code lens represents a {@link Command command} that should be shown along with
// /// source text, like the number of references, a way to run tests, etc.
// /// 
// /// A code lens is _unresolved_ when no command is associated to it. For performance
// /// reasons the creation of a code lens and resolving should be done in two stages.
// /// </summary>
// public class CodeLens{

//     /// <summary>
//     /// The range in which this code lens is valid. Should only span a single line.
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// The command this code lens represents.
//     /// </summary>
//     public Command? command { get; set; }

//     /// <summary>
//     /// A data entry field that is preserved on a code lens item between
//     /// a {@link CodeLensRequest} and a [CodeLensResolveRequest]
//     /// (#CodeLensResolveRequest)
//     /// </summary>
//     public LSPAny? data { get; set; }
// }

// /// <summary>
// /// Registration options for a {@link CodeLensRequest}.
// /// </summary>
// public class CodeLensRegistrationOptions: ITextDocumentRegistrationOptions,ICodeLensOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from CodeLensOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// Code lens has a resolve provider as well.
//     /// extend from CodeLensOptions
//     /// </summary>
//     public bool? resolveProvider { get; set; }
// }

// /// <summary>
// /// The parameters of a {@link DocumentLinkRequest}.
// /// </summary>
// public class DocumentLinkParams: IWorkDoneProgressParams,IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// extend from PartialResultParams
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }

//     /// <summary>
//     /// The document to provide document links for.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }
// }

// /// <summary>
// /// A document link is a range in a text document that links to an internal or external resource, like another
// /// text document or a web site.
// /// </summary>
// public class DocumentLink{

//     /// <summary>
//     /// The range this link applies to.
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// The uri this link points to. If missing a resolve request is sent later.
//     /// </summary>
//     public string? target { get; set; }

//     /// <summary>
//     /// The tooltip text when you hover over this link.
//     /// 
//     /// If a tooltip is provided, is will be displayed in a string that includes instructions on how to
//     /// trigger the link, such as `{0} (ctrl + click)`. The specific instructions vary depending on OS,
//     /// user settings, and localization.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public string? tooltip { get; set; }

//     /// <summary>
//     /// A data entry field that is preserved on a document link between a
//     /// DocumentLinkRequest and a DocumentLinkResolveRequest.
//     /// </summary>
//     public LSPAny? data { get; set; }
// }

// /// <summary>
// /// Registration options for a {@link DocumentLinkRequest}.
// /// </summary>
// public class DocumentLinkRegistrationOptions: ITextDocumentRegistrationOptions,IDocumentLinkOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from DocumentLinkOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// Document links have a resolve provider as well.
//     /// extend from DocumentLinkOptions
//     /// </summary>
//     public bool? resolveProvider { get; set; }
// }

// /// <summary>
// /// The parameters of a {@link DocumentFormattingRequest}.
// /// </summary>
// public class DocumentFormattingParams: IWorkDoneProgressParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// The document to format.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The format options.
//     /// </summary>
//     public FormattingOptions options { get; set; }
// }

// /// <summary>
// /// Registration options for a {@link DocumentFormattingRequest}.
// /// </summary>
// public class DocumentFormattingRegistrationOptions: ITextDocumentRegistrationOptions,IDocumentFormattingOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from DocumentFormattingOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// The parameters of a {@link DocumentRangeFormattingRequest}.
// /// </summary>
// public class DocumentRangeFormattingParams: IWorkDoneProgressParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// The document to format.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The range to format
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// The format options
//     /// </summary>
//     public FormattingOptions options { get; set; }
// }

// /// <summary>
// /// Registration options for a {@link DocumentRangeFormattingRequest}.
// /// </summary>
// public class DocumentRangeFormattingRegistrationOptions: ITextDocumentRegistrationOptions,IDocumentRangeFormattingOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from DocumentRangeFormattingOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// The parameters of a {@link DocumentOnTypeFormattingRequest}.
// /// </summary>
// public class DocumentOnTypeFormattingParams{

//     /// <summary>
//     /// The document to format.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position around which the on type formatting should happen.
//     /// This is not necessarily the exact position where the character denoted
//     /// by the property `ch` got typed.
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// The character that has been typed that triggered the formatting
//     /// on type request. That is not necessarily the last character that
//     /// got inserted into the document since the client could auto insert
//     /// characters as well (e.g. like automatic brace completion).
//     /// </summary>
//     public string ch { get; set; }

//     /// <summary>
//     /// The formatting options.
//     /// </summary>
//     public FormattingOptions options { get; set; }
// }

// /// <summary>
// /// Registration options for a {@link DocumentOnTypeFormattingRequest}.
// /// </summary>
// public class DocumentOnTypeFormattingRegistrationOptions: ITextDocumentRegistrationOptions,IDocumentOnTypeFormattingOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// A character on which formatting should be triggered, like `{`.
//     /// extend from DocumentOnTypeFormattingOptions
//     /// </summary>
//     public string firstTriggerCharacter { get; set; }

//     /// <summary>
//     /// More trigger characters.
//     /// extend from DocumentOnTypeFormattingOptions
//     /// </summary>
//     public List<string>? moreTriggerCharacter { get; set; }
// }

// /// <summary>
// /// The parameters of a {@link RenameRequest}.
// /// </summary>
// public class RenameParams: IWorkDoneProgressParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// The document to rename.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position at which this request was sent.
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// The new name of the symbol. If the given name is not valid the
//     /// request must return a {@link ResponseError} with an
//     /// appropriate message set.
//     /// </summary>
//     public string newName { get; set; }
// }

// /// <summary>
// /// Registration options for a {@link RenameRequest}.
// /// </summary>
// public class RenameRegistrationOptions: ITextDocumentRegistrationOptions,IRenameOptions{

//     /// <summary>
//     /// A document selector to identify the scope of the registration. If set to null
//     /// the document selector provided on the client side will be used.
//     /// extend from TextDocumentRegistrationOptions
//     /// </summary>
//     public MyNode documentSelector { get; set; }

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from RenameOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// Renames should be checked and tested before being executed.
//     /// 
//     /// @since version 3.12.0
//     /// extend from RenameOptions
//     /// </summary>
//     public bool? prepareProvider { get; set; }
// }


// public class PrepareRenameParams: ITextDocumentPositionParams,IWorkDoneProgressParams{

//     /// <summary>
//     /// The text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position inside the text document.
//     /// extend from TextDocumentPositionParams
//     /// </summary>
//     public Position position { get; set; }

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }
// }

// /// <summary>
// /// The parameters of a {@link ExecuteCommandRequest}.
// /// </summary>
// public class ExecuteCommandParams: IWorkDoneProgressParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// The identifier of the actual command handler.
//     /// </summary>
//     public string command { get; set; }

//     /// <summary>
//     /// Arguments that the command should be invoked with.
//     /// </summary>
//     public List<LSPAny>? arguments { get; set; }
// }

// /// <summary>
// /// Registration options for a {@link ExecuteCommandRequest}.
// /// </summary>
// public class ExecuteCommandRegistrationOptions: IExecuteCommandOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// extend from ExecuteCommandOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The commands to be executed on the server
//     /// extend from ExecuteCommandOptions
//     /// </summary>
//     public List<string> commands { get; set; }
// }

// /// <summary>
// /// The parameters passed via an apply workspace edit request.
// /// </summary>
// public class ApplyWorkspaceEditParams{

//     /// <summary>
//     /// An optional label of the workspace edit. This label is
//     /// presented in the user interface for example on an undo
//     /// stack to undo the workspace edit.
//     /// </summary>
//     public string? label { get; set; }

//     /// <summary>
//     /// The edits to apply.
//     /// </summary>
//     public WorkspaceEdit edit { get; set; }
// }

// /// <summary>
// /// The result returned from the apply workspace edit request.
// /// 
// /// @since 3.17 renamed from ApplyWorkspaceEditResponse
// /// </summary>
// public class ApplyWorkspaceEditResult{

//     /// <summary>
//     /// Indicates whether the edit was applied or not.
//     /// </summary>
//     public bool applied { get; set; }

//     /// <summary>
//     /// An optional textual description for why the edit was not applied.
//     /// This may be used by the server for diagnostic logging or to provide
//     /// a suitable error for a request that triggered the edit.
//     /// </summary>
//     public string? failureReason { get; set; }

//     /// <summary>
//     /// Depending on the client's failure handling strategy `failedChange` might
//     /// contain the index of the change that failed. This property is only available
//     /// if the client signals a `failureHandlingStrategy` in its client capabilities.
//     /// </summary>
//     public uint? failedChange { get; set; }
// }


// public class WorkDoneProgressBegin{

    
//     public string kind { get; set; } = "begin"; 

//     /// <summary>
//     /// Mandatory title of the progress operation. Used to briefly inform about
//     /// the kind of operation being performed.
//     /// 
//     /// Examples: "Indexing" or "Linking dependencies".
//     /// </summary>
//     public string title { get; set; }

//     /// <summary>
//     /// Controls if a cancel button should show to allow the user to cancel the
//     /// long running operation. Clients that don't support cancellation are allowed
//     /// to ignore the setting.
//     /// </summary>
//     public bool? cancellable { get; set; }

//     /// <summary>
//     /// Optional, more detailed associated progress message. Contains
//     /// complementary information to the `title`.
//     /// 
//     /// Examples: "3/25 files", "project/src/module2", "node_modules/some_dep".
//     /// If unset, the previous progress message (if any) is still valid.
//     /// </summary>
//     public string? message { get; set; }

//     /// <summary>
//     /// Optional progress percentage to display (value 100 is considered 100%).
//     /// If not provided infinite progress is assumed and clients are allowed
//     /// to ignore the `percentage` value in subsequent in report notifications.
//     /// 
//     /// The value should be steadily rising. Clients are free to ignore values
//     /// that are not following this rule. The value range is [0, 100].
//     /// </summary>
//     public uint? percentage { get; set; }
// }


// public class WorkDoneProgressReport{

    
//     public string kind { get; set; } = "report"; 

//     /// <summary>
//     /// Controls enablement state of a cancel button.
//     /// 
//     /// Clients that don't support cancellation or don't support controlling the button's
//     /// enablement state are allowed to ignore the property.
//     /// </summary>
//     public bool? cancellable { get; set; }

//     /// <summary>
//     /// Optional, more detailed associated progress message. Contains
//     /// complementary information to the `title`.
//     /// 
//     /// Examples: "3/25 files", "project/src/module2", "node_modules/some_dep".
//     /// If unset, the previous progress message (if any) is still valid.
//     /// </summary>
//     public string? message { get; set; }

//     /// <summary>
//     /// Optional progress percentage to display (value 100 is considered 100%).
//     /// If not provided infinite progress is assumed and clients are allowed
//     /// to ignore the `percentage` value in subsequent in report notifications.
//     /// 
//     /// The value should be steadily rising. Clients are free to ignore values
//     /// that are not following this rule. The value range is [0, 100]
//     /// </summary>
//     public uint? percentage { get; set; }
// }


// public class WorkDoneProgressEnd{

    
//     public string kind { get; set; } = "end"; 

//     /// <summary>
//     /// Optional, a final message indicating to for example indicate the outcome
//     /// of the operation.
//     /// </summary>
//     public string? message { get; set; }
// }


// public class SetTraceParams{

    
//     public string value { get; set; }
// }


// public class LogTraceParams{

    
//     public string message { get; set; }

    
//     public string? verbose { get; set; }
// }


// public class CancelParams{

//     /// <summary>
//     /// The request id to cancel.
//     /// </summary>
//     public MyNode id { get; set; }
// }


// public class ProgressParams{

//     /// <summary>
//     /// The progress token provided by the client or server.
//     /// </summary>
//     public ProgressToken token { get; set; }

//     /// <summary>
//     /// The progress data.
//     /// </summary>
//     public LSPAny value { get; set; }
// }

// /// <summary>
// /// A parameter literal used in requests to pass a text document and a position inside that
// /// document.
// /// </summary>
// public interface ITextDocumentPositionParams {

//     /// <summary>
//     /// The text document.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position inside the text document.
//     /// </summary>
//     public Position position { get; set; }
// }

// /// <summary>
// /// A parameter literal used in requests to pass a text document and a position inside that
// /// document.
// /// </summary>
// public class TextDocumentPositionParams: ITextDocumentPositionParams{

//     /// <summary>
//     /// The text document.
//     /// </summary>
//     public TextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The position inside the text document.
//     /// </summary>
//     public Position position { get; set; }
// }


// public interface IWorkDoneProgressParams {

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }
// }


// public class WorkDoneProgressParams: IWorkDoneProgressParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }
// }


// public interface IPartialResultParams {

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }
// }


// public class PartialResultParams: IPartialResultParams{

//     /// <summary>
//     /// An optional token that a server can use to report partial results (e.g. streaming) to
//     /// the client.
//     /// </summary>
//     public ProgressToken? partialResultToken { get; set; }
// }

// /// <summary>
// /// Represents the connection of two locations. Provides additional metadata over normal {@link Location locations},
// /// including an origin range.
// /// </summary>
// public class LocationLink{

//     /// <summary>
//     /// Span of the origin of this link.
//     /// 
//     /// Used as the underlined span for mouse interaction. Defaults to the word range at
//     /// the definition position.
//     /// </summary>
//     public Range? originSelectionRange { get; set; }

//     /// <summary>
//     /// The target resource identifier of this link.
//     /// </summary>
//     public DocumentUri targetUri { get; set; }

//     /// <summary>
//     /// The full target range of this link. If the target for example is a symbol then target range is the
//     /// range enclosing this symbol not including leading/trailing whitespace but everything else
//     /// like comments. This information is typically used to highlight the range in the editor.
//     /// </summary>
//     public Range targetRange { get; set; }

//     /// <summary>
//     /// The range that should be selected and revealed when this link is being followed, e.g the name of a function.
//     /// Must be contained by the `targetRange`. See also `DocumentSymbol#range`
//     /// </summary>
//     public Range targetSelectionRange { get; set; }
// }

// /// <summary>
// /// A range in a text document expressed as (zero-based) start and end positions.
// /// 
// /// If you want to specify a range that contains a line including the line ending
// /// character(s) then use an end position denoting the start of the next line.
// /// For example:
// /// ```ts
// /// {
// ///     start: { line: 5, character: 23 }
// ///     end : { line 6, character : 0 }
// /// }
// /// ```
// /// </summary>
// public class Range{

//     /// <summary>
//     /// The range's start position.
//     /// </summary>
//     public Position start { get; set; }

//     /// <summary>
//     /// The range's end position.
//     /// </summary>
//     public Position end { get; set; }
// }


// public interface IImplementationOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }


// public class ImplementationOptions: IImplementationOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Static registration options to be returned in the initialize
// /// request.
// /// </summary>
// public interface IStaticRegistrationOptions {

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again. See also Registration#id.
//     /// </summary>
//     public string? id { get; set; }
// }

// /// <summary>
// /// Static registration options to be returned in the initialize
// /// request.
// /// </summary>
// public class StaticRegistrationOptions: IStaticRegistrationOptions{

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again. See also Registration#id.
//     /// </summary>
//     public string? id { get; set; }
// }


// public interface ITypeDefinitionOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }


// public class TypeDefinitionOptions: ITypeDefinitionOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// The workspace folder change event.
// /// </summary>
// public class WorkspaceFoldersChangeEvent{

//     /// <summary>
//     /// The array of added workspace folders
//     /// </summary>
//     public List<WorkspaceFolder> added { get; set; }

//     /// <summary>
//     /// The array of the removed workspace folders
//     /// </summary>
//     public List<WorkspaceFolder> removed { get; set; }
// }


// public class ConfigurationItem{

//     /// <summary>
//     /// The scope to get the configuration section for.
//     /// </summary>
//     public string? scopeUri { get; set; }

//     /// <summary>
//     /// The configuration section asked for.
//     /// </summary>
//     public string? section { get; set; }
// }

// /// <summary>
// /// A literal to identify a text document in the client.
// /// </summary>
// public interface ITextDocumentIdentifier {

//     /// <summary>
//     /// The text document's uri.
//     /// </summary>
//     public DocumentUri uri { get; set; }
// }

// /// <summary>
// /// A literal to identify a text document in the client.
// /// </summary>
// public class TextDocumentIdentifier: ITextDocumentIdentifier{

//     /// <summary>
//     /// The text document's uri.
//     /// </summary>
//     public DocumentUri uri { get; set; }
// }

// /// <summary>
// /// Represents a color in RGBA space.
// /// </summary>
// public class Color{

//     /// <summary>
//     /// The red component of this color in the range [0-1].
//     /// </summary>
//     public float red { get; set; }

//     /// <summary>
//     /// The green component of this color in the range [0-1].
//     /// </summary>
//     public float green { get; set; }

//     /// <summary>
//     /// The blue component of this color in the range [0-1].
//     /// </summary>
//     public float blue { get; set; }

//     /// <summary>
//     /// The alpha component of this color in the range [0-1].
//     /// </summary>
//     public float alpha { get; set; }
// }


// public interface IDocumentColorOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }


// public class DocumentColorOptions: IDocumentColorOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }


// public interface IFoldingRangeOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }


// public class FoldingRangeOptions: IFoldingRangeOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }


// public interface IDeclarationOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }


// public class DeclarationOptions: IDeclarationOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Position in a text document expressed as zero-based line and character
// /// offset. Prior to 3.17 the offsets were always based on a UTF-16 string
// /// representation. So a string of the form `a𐐀b` the character offset of the
// /// character `a` is 0, the character offset of `𐐀` is 1 and the character
// /// offset of b is 3 since `𐐀` is represented using two code units in UTF-16.
// /// Since 3.17 clients and servers can agree on a different string encoding
// /// representation (e.g. UTF-8). The client announces it's supported encoding
// /// via the client capability [`general.positionEncodings`](#clientCapabilities).
// /// The value is an array of position encodings the client supports, with
// /// decreasing preference (e.g. the encoding at index `0` is the most preferred
// /// one). To stay backwards compatible the only mandatory encoding is UTF-16
// /// represented via the string `utf-16`. The server can pick one of the
// /// encodings offered by the client and signals that encoding back to the
// /// client via the initialize result's property
// /// [`capabilities.positionEncoding`](#serverCapabilities). If the string value
// /// `utf-16` is missing from the client's capability `general.positionEncodings`
// /// servers can safely assume that the client supports UTF-16. If the server
// /// omits the position encoding in its initialize result the encoding defaults
// /// to the string value `utf-16`. Implementation considerations: since the
// /// conversion from one encoding into another requires the content of the
// /// file / line the conversion is best done where the file is read which is
// /// usually on the server side.
// /// 
// /// Positions are line end character agnostic. So you can not specify a position
// /// that denotes `\r|\n` or `\n|` where `|` represents the character offset.
// /// 
// /// @since 3.17.0 - support for negotiated position encoding.
// /// </summary>
// public class Position{

//     /// <summary>
//     /// Line position in a document (zero-based).
//     /// 
//     /// If a line number is greater than the number of lines in a document, it defaults back to the number of lines in the document.
//     /// If a line number is negative, it defaults to 0.
//     /// </summary>
//     public uint line { get; set; }

//     /// <summary>
//     /// Character offset on a line in a document (zero-based).
//     /// 
//     /// The meaning of this offset is determined by the negotiated
//     /// `PositionEncodingKind`.
//     /// 
//     /// If the character value is greater than the line length it defaults back to the
//     /// line length.
//     /// </summary>
//     public uint character { get; set; }
// }


// public interface ISelectionRangeOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }


// public class SelectionRangeOptions: ISelectionRangeOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Call hierarchy options used during static registration.
// /// 
// /// @since 3.16.0
// /// </summary>
// public interface ICallHierarchyOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Call hierarchy options used during static registration.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class CallHierarchyOptions: ICallHierarchyOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// @since 3.16.0
// /// </summary>
// public interface ISemanticTokensOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The legend used by the server
//     /// </summary>
//     public SemanticTokensLegend legend { get; set; }

//     /// <summary>
//     /// Server supports providing semantic tokens for a specific range
//     /// of a document.
//     /// </summary>
//     public MyNode? range { get; set; }

//     /// <summary>
//     /// Server supports providing semantic tokens for a full document.
//     /// </summary>
//     public MyNode? full { get; set; }
// }

// /// <summary>
// /// @since 3.16.0
// /// </summary>
// public class SemanticTokensOptions: ISemanticTokensOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The legend used by the server
//     /// </summary>
//     public SemanticTokensLegend legend { get; set; }

//     /// <summary>
//     /// Server supports providing semantic tokens for a specific range
//     /// of a document.
//     /// </summary>
//     public MyNode? range { get; set; }

//     /// <summary>
//     /// Server supports providing semantic tokens for a full document.
//     /// </summary>
//     public MyNode? full { get; set; }
// }

// /// <summary>
// /// @since 3.16.0
// /// </summary>
// public class SemanticTokensEdit{

//     /// <summary>
//     /// The start offset of the edit.
//     /// </summary>
//     public uint start { get; set; }

//     /// <summary>
//     /// The count of elements to remove.
//     /// </summary>
//     public uint deleteCount { get; set; }

//     /// <summary>
//     /// The elements to insert.
//     /// </summary>
//     public List<uint>? data { get; set; }
// }


// public interface ILinkedEditingRangeOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }


// public class LinkedEditingRangeOptions: ILinkedEditingRangeOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Represents information on a file/folder create.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class FileCreate{

//     /// <summary>
//     /// A file:// URI for the location of the file/folder being created.
//     /// </summary>
//     public string uri { get; set; }
// }

// /// <summary>
// /// Describes textual changes on a text document. A TextDocumentEdit describes all changes
// /// on a document version Si and after they are applied move the document to version Si+1.
// /// So the creator of a TextDocumentEdit doesn't need to sort the array of edits or do any
// /// kind of ordering. However the edits must be non overlapping.
// /// </summary>
// public class TextDocumentEdit{

//     /// <summary>
//     /// The text document to change.
//     /// </summary>
//     public OptionalVersionedTextDocumentIdentifier textDocument { get; set; }

//     /// <summary>
//     /// The edits to be applied.
//     /// 
//     /// @since 3.16.0 - support for AnnotatedTextEdit. This is guarded using a
//     /// client capability.
//     /// </summary>
//     public List<MyNode> edits { get; set; }
// }

// /// <summary>
// /// Create file operation.
// /// </summary>
// public class CreateFile: IResourceOperation{

//     /// <summary>
//     /// The resource operation kind.
//     /// extend from ResourceOperation
//     /// </summary>
//     public string kind { get; set; }

//     /// <summary>
//     /// An optional annotation identifier describing the operation.
//     /// 
//     /// @since 3.16.0
//     /// extend from ResourceOperation
//     /// </summary>
//     public ChangeAnnotationIdentifier? annotationId { get; set; }

//     /// <summary>
//     /// A create
//     /// </summary>
//     public string kind { get; set; } = "create"; 

//     /// <summary>
//     /// The resource to create.
//     /// </summary>
//     public DocumentUri uri { get; set; }

//     /// <summary>
//     /// Additional options
//     /// </summary>
//     public CreateFileOptions? options { get; set; }
// }

// /// <summary>
// /// Rename file operation
// /// </summary>
// public class RenameFile: IResourceOperation{

//     /// <summary>
//     /// The resource operation kind.
//     /// extend from ResourceOperation
//     /// </summary>
//     public string kind { get; set; }

//     /// <summary>
//     /// An optional annotation identifier describing the operation.
//     /// 
//     /// @since 3.16.0
//     /// extend from ResourceOperation
//     /// </summary>
//     public ChangeAnnotationIdentifier? annotationId { get; set; }

//     /// <summary>
//     /// A rename
//     /// </summary>
//     public string kind { get; set; } = "rename"; 

//     /// <summary>
//     /// The old (existing) location.
//     /// </summary>
//     public DocumentUri oldUri { get; set; }

//     /// <summary>
//     /// The new location.
//     /// </summary>
//     public DocumentUri newUri { get; set; }

//     /// <summary>
//     /// Rename options.
//     /// </summary>
//     public RenameFileOptions? options { get; set; }
// }

// /// <summary>
// /// Delete file operation
// /// </summary>
// public class DeleteFile: IResourceOperation{

//     /// <summary>
//     /// The resource operation kind.
//     /// extend from ResourceOperation
//     /// </summary>
//     public string kind { get; set; }

//     /// <summary>
//     /// An optional annotation identifier describing the operation.
//     /// 
//     /// @since 3.16.0
//     /// extend from ResourceOperation
//     /// </summary>
//     public ChangeAnnotationIdentifier? annotationId { get; set; }

//     /// <summary>
//     /// A delete
//     /// </summary>
//     public string kind { get; set; } = "delete"; 

//     /// <summary>
//     /// The file to delete.
//     /// </summary>
//     public DocumentUri uri { get; set; }

//     /// <summary>
//     /// Delete options.
//     /// </summary>
//     public DeleteFileOptions? options { get; set; }
// }

// /// <summary>
// /// Additional information that describes document changes.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class ChangeAnnotation{

//     /// <summary>
//     /// A human-readable string describing the actual change. The string
//     /// is rendered prominent in the user interface.
//     /// </summary>
//     public string label { get; set; }

//     /// <summary>
//     /// A flag which indicates that user confirmation is needed
//     /// before applying the change.
//     /// </summary>
//     public bool? needsConfirmation { get; set; }

//     /// <summary>
//     /// A human-readable string which is rendered less prominent in
//     /// the user interface.
//     /// </summary>
//     public string? description { get; set; }
// }

// /// <summary>
// /// A filter to describe in which file operation requests or notifications
// /// the server is interested in receiving.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class FileOperationFilter{

//     /// <summary>
//     /// A Uri scheme like `file` or `untitled`.
//     /// </summary>
//     public string? scheme { get; set; }

//     /// <summary>
//     /// The actual file operation pattern.
//     /// </summary>
//     public FileOperationPattern pattern { get; set; }
// }

// /// <summary>
// /// Represents information on a file/folder rename.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class FileRename{

//     /// <summary>
//     /// A file:// URI for the original location of the file/folder being renamed.
//     /// </summary>
//     public string oldUri { get; set; }

//     /// <summary>
//     /// A file:// URI for the new location of the file/folder being renamed.
//     /// </summary>
//     public string newUri { get; set; }
// }

// /// <summary>
// /// Represents information on a file/folder delete.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class FileDelete{

//     /// <summary>
//     /// A file:// URI for the location of the file/folder being deleted.
//     /// </summary>
//     public string uri { get; set; }
// }


// public interface IMonikerOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }


// public class MonikerOptions: IMonikerOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Type hierarchy options used during static registration.
// /// 
// /// @since 3.17.0
// /// </summary>
// public interface ITypeHierarchyOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Type hierarchy options used during static registration.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class TypeHierarchyOptions: ITypeHierarchyOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// @since 3.17.0
// /// </summary>
// public class InlineValueContext{

//     /// <summary>
//     /// The stack frame (as a DAP Id) where the execution has stopped.
//     /// </summary>
//     public int frameId { get; set; }

//     /// <summary>
//     /// The document range where execution has stopped.
//     /// Typically the end position of the range denotes the line where the inline values are shown.
//     /// </summary>
//     public Range stoppedLocation { get; set; }
// }

// /// <summary>
// /// Provide inline value as text.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class InlineValueText{

//     /// <summary>
//     /// The document range for which the inline value applies.
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// The text of the inline value.
//     /// </summary>
//     public string text { get; set; }
// }

// /// <summary>
// /// Provide inline value through a variable lookup.
// /// If only a range is specified, the variable name will be extracted from the underlying document.
// /// An optional variable name can be used to override the extracted name.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class InlineValueVariableLookup{

//     /// <summary>
//     /// The document range for which the inline value applies.
//     /// The range is used to extract the variable name from the underlying document.
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// If specified the name of the variable to look up.
//     /// </summary>
//     public string? variableName { get; set; }

//     /// <summary>
//     /// How to perform the lookup.
//     /// </summary>
//     public bool caseSensitiveLookup { get; set; }
// }

// /// <summary>
// /// Provide an inline value through an expression evaluation.
// /// If only a range is specified, the expression will be extracted from the underlying document.
// /// An optional expression can be used to override the extracted expression.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class InlineValueEvaluatableExpression{

//     /// <summary>
//     /// The document range for which the inline value applies.
//     /// The range is used to extract the evaluatable expression from the underlying document.
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// If specified the expression overrides the extracted expression.
//     /// </summary>
//     public string? expression { get; set; }
// }

// /// <summary>
// /// Inline value options used during static registration.
// /// 
// /// @since 3.17.0
// /// </summary>
// public interface IInlineValueOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Inline value options used during static registration.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class InlineValueOptions: IInlineValueOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// An inlay hint label part allows for interactive and composite labels
// /// of inlay hints.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class InlayHintLabelPart{

//     /// <summary>
//     /// The value of this label part.
//     /// </summary>
//     public string value { get; set; }

//     /// <summary>
//     /// The tooltip text when you hover over this label part. Depending on
//     /// the client capability `inlayHint.resolveSupport` clients might resolve
//     /// this property late using the resolve request.
//     /// </summary>
//     public MyNode? tooltip { get; set; }

//     /// <summary>
//     /// An optional source code location that represents this
//     /// label part.
//     /// 
//     /// The editor will use this location for the hover and for code navigation
//     /// features: This part will become a clickable link that resolves to the
//     /// definition of the symbol at the given location (not necessarily the
//     /// location itself), it shows the hover that shows at the given location,
//     /// and it shows a context menu with further code navigation commands.
//     /// 
//     /// Depending on the client capability `inlayHint.resolveSupport` clients
//     /// might resolve this property late using the resolve request.
//     /// </summary>
//     public Location? location { get; set; }

//     /// <summary>
//     /// An optional command for this label part.
//     /// 
//     /// Depending on the client capability `inlayHint.resolveSupport` clients
//     /// might resolve this property late using the resolve request.
//     /// </summary>
//     public Command? command { get; set; }
// }

// /// <summary>
// /// A `MarkupContent` literal represents a string value which content is interpreted base on its
// /// kind flag. Currently the protocol supports `plaintext` and `markdown` as markup kinds.
// /// 
// /// If the kind is `markdown` then the value can contain fenced code blocks like in GitHub issues.
// /// See https://help.github.com/articles/creating-and-highlighting-code-blocks/#syntax-highlighting
// /// 
// /// Here is an example how such a string can be constructed using JavaScript / TypeScript:
// /// ```ts
// /// let markdown: MarkdownContent = {
// ///  kind: MarkupKind.Markdown,
// ///  value: [
// ///    '# Header',
// ///    'Some text',
// ///    '```typescript',
// ///    'someCode();',
// ///    '```'
// ///  ].join('\n')
// /// };
// /// ```
// /// 
// /// *Please Note* that clients might sanitize the return markdown. A client could decide to
// /// remove HTML from the markdown to avoid script execution.
// /// </summary>
// public class MarkupContent{

//     /// <summary>
//     /// The type of the Markup
//     /// </summary>
//     public string kind { get; set; }

//     /// <summary>
//     /// The content itself
//     /// </summary>
//     public string value { get; set; }
// }

// /// <summary>
// /// Inlay hint options used during static registration.
// /// 
// /// @since 3.17.0
// /// </summary>
// public interface IInlayHintOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The server provides support to resolve additional
//     /// information for an inlay hint item.
//     /// </summary>
//     public bool? resolveProvider { get; set; }
// }

// /// <summary>
// /// Inlay hint options used during static registration.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class InlayHintOptions: IInlayHintOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The server provides support to resolve additional
//     /// information for an inlay hint item.
//     /// </summary>
//     public bool? resolveProvider { get; set; }
// }

// /// <summary>
// /// A full diagnostic report with a set of related documents.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class RelatedFullDocumentDiagnosticReport: IFullDocumentDiagnosticReport{

//     /// <summary>
//     /// A full document diagnostic report.
//     /// extend from FullDocumentDiagnosticReport
//     /// </summary>
//     public string kind { get; set; } = "full"; 

//     /// <summary>
//     /// An optional result id. If provided it will
//     /// be sent on the next diagnostic request for the
//     /// same document.
//     /// extend from FullDocumentDiagnosticReport
//     /// </summary>
//     public string? resultId { get; set; }

//     /// <summary>
//     /// The actual items.
//     /// extend from FullDocumentDiagnosticReport
//     /// </summary>
//     public List<Diagnostic> items { get; set; }

//     /// <summary>
//     /// Diagnostics of related documents. This information is useful
//     /// in programming languages where code in a file A can generate
//     /// diagnostics in a file B which A depends on. An example of
//     /// such a language is C/C++ where marco definitions in a file
//     /// a.cpp and result in errors in a header file b.hpp.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public Dictionary<DocumentUri,MyNode>? relatedDocuments { get; set; }
// }

// /// <summary>
// /// An unchanged diagnostic report with a set of related documents.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class RelatedUnchangedDocumentDiagnosticReport: IUnchangedDocumentDiagnosticReport{

//     /// <summary>
//     /// A document diagnostic report indicating
//     /// no changes to the last result. A server can
//     /// only return `unchanged` if result ids are
//     /// provided.
//     /// extend from UnchangedDocumentDiagnosticReport
//     /// </summary>
//     public string kind { get; set; } = "unchanged"; 

//     /// <summary>
//     /// A result id which will be sent on the next
//     /// diagnostic request for the same document.
//     /// extend from UnchangedDocumentDiagnosticReport
//     /// </summary>
//     public string resultId { get; set; }

//     /// <summary>
//     /// Diagnostics of related documents. This information is useful
//     /// in programming languages where code in a file A can generate
//     /// diagnostics in a file B which A depends on. An example of
//     /// such a language is C/C++ where marco definitions in a file
//     /// a.cpp and result in errors in a header file b.hpp.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public Dictionary<DocumentUri,MyNode>? relatedDocuments { get; set; }
// }

// /// <summary>
// /// A diagnostic report with a full set of problems.
// /// 
// /// @since 3.17.0
// /// </summary>
// public interface IFullDocumentDiagnosticReport {

//     /// <summary>
//     /// A full document diagnostic report.
//     /// </summary>
//     public string kind { get; set; }

//     /// <summary>
//     /// An optional result id. If provided it will
//     /// be sent on the next diagnostic request for the
//     /// same document.
//     /// </summary>
//     public string? resultId { get; set; }

//     /// <summary>
//     /// The actual items.
//     /// </summary>
//     public List<Diagnostic> items { get; set; }
// }

// /// <summary>
// /// A diagnostic report with a full set of problems.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class FullDocumentDiagnosticReport: IFullDocumentDiagnosticReport{

//     /// <summary>
//     /// A full document diagnostic report.
//     /// </summary>
//     public string kind { get; set; } = "full"; 

//     /// <summary>
//     /// An optional result id. If provided it will
//     /// be sent on the next diagnostic request for the
//     /// same document.
//     /// </summary>
//     public string? resultId { get; set; }

//     /// <summary>
//     /// The actual items.
//     /// </summary>
//     public List<Diagnostic> items { get; set; }
// }

// /// <summary>
// /// A diagnostic report indicating that the last returned
// /// report is still accurate.
// /// 
// /// @since 3.17.0
// /// </summary>
// public interface IUnchangedDocumentDiagnosticReport {

//     /// <summary>
//     /// A document diagnostic report indicating
//     /// no changes to the last result. A server can
//     /// only return `unchanged` if result ids are
//     /// provided.
//     /// </summary>
//     public string kind { get; set; }

//     /// <summary>
//     /// A result id which will be sent on the next
//     /// diagnostic request for the same document.
//     /// </summary>
//     public string resultId { get; set; }
// }

// /// <summary>
// /// A diagnostic report indicating that the last returned
// /// report is still accurate.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class UnchangedDocumentDiagnosticReport: IUnchangedDocumentDiagnosticReport{

//     /// <summary>
//     /// A document diagnostic report indicating
//     /// no changes to the last result. A server can
//     /// only return `unchanged` if result ids are
//     /// provided.
//     /// </summary>
//     public string kind { get; set; } = "unchanged"; 

//     /// <summary>
//     /// A result id which will be sent on the next
//     /// diagnostic request for the same document.
//     /// </summary>
//     public string resultId { get; set; }
// }

// /// <summary>
// /// Diagnostic options.
// /// 
// /// @since 3.17.0
// /// </summary>
// public interface IDiagnosticOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// An optional identifier under which the diagnostics are
//     /// managed by the client.
//     /// </summary>
//     public string? identifier { get; set; }

//     /// <summary>
//     /// Whether the language has inter file dependencies meaning that
//     /// editing code in one file can result in a different diagnostic
//     /// set in another file. Inter file dependencies are common for
//     /// most programming languages and typically uncommon for linters.
//     /// </summary>
//     public bool interFileDependencies { get; set; }

//     /// <summary>
//     /// The server provides support for workspace diagnostics as well.
//     /// </summary>
//     public bool workspaceDiagnostics { get; set; }
// }

// /// <summary>
// /// Diagnostic options.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class DiagnosticOptions: IDiagnosticOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// An optional identifier under which the diagnostics are
//     /// managed by the client.
//     /// </summary>
//     public string? identifier { get; set; }

//     /// <summary>
//     /// Whether the language has inter file dependencies meaning that
//     /// editing code in one file can result in a different diagnostic
//     /// set in another file. Inter file dependencies are common for
//     /// most programming languages and typically uncommon for linters.
//     /// </summary>
//     public bool interFileDependencies { get; set; }

//     /// <summary>
//     /// The server provides support for workspace diagnostics as well.
//     /// </summary>
//     public bool workspaceDiagnostics { get; set; }
// }

// /// <summary>
// /// A previous result id in a workspace pull request.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class PreviousResultId{

//     /// <summary>
//     /// The URI for which the client knowns a
//     /// result id.
//     /// </summary>
//     public DocumentUri uri { get; set; }

//     /// <summary>
//     /// The value of the previous result id.
//     /// </summary>
//     public string value { get; set; }
// }

// /// <summary>
// /// A notebook document.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class NotebookDocument{

//     /// <summary>
//     /// The notebook document's uri.
//     /// </summary>
//     public Uri uri { get; set; }

//     /// <summary>
//     /// The type of the notebook.
//     /// </summary>
//     public string notebookType { get; set; }

//     /// <summary>
//     /// The version number of this document (it will increase after each
//     /// change, including undo/redo).
//     /// </summary>
//     public int version { get; set; }

//     /// <summary>
//     /// Additional metadata stored with the notebook
//     /// document.
//     /// 
//     /// Note: should always be an object literal (e.g. LSPObject)
//     /// </summary>
//     public LSPObject? metadata { get; set; }

//     /// <summary>
//     /// The cells of a notebook.
//     /// </summary>
//     public List<NotebookCell> cells { get; set; }
// }

// /// <summary>
// /// An item to transfer a text document from the client to the
// /// server.
// /// </summary>
// public class TextDocumentItem{

//     /// <summary>
//     /// The text document's uri.
//     /// </summary>
//     public DocumentUri uri { get; set; }

//     /// <summary>
//     /// The text document's language identifier.
//     /// </summary>
//     public string languageId { get; set; }

//     /// <summary>
//     /// The version number of this document (it will increase after each
//     /// change, including undo/redo).
//     /// </summary>
//     public int version { get; set; }

//     /// <summary>
//     /// The content of the opened text document.
//     /// </summary>
//     public string text { get; set; }
// }

// /// <summary>
// /// A versioned notebook document identifier.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class VersionedNotebookDocumentIdentifier{

//     /// <summary>
//     /// The version number of this notebook document.
//     /// </summary>
//     public int version { get; set; }

//     /// <summary>
//     /// The notebook document's uri.
//     /// </summary>
//     public Uri uri { get; set; }
// }

// /// <summary>
// /// A change event for a notebook document.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class NotebookDocumentChangeEvent{

//     /// <summary>
//     /// The changed meta data if any.
//     /// 
//     /// Note: should always be an object literal (e.g. LSPObject)
//     /// </summary>
//     public LSPObject? metadata { get; set; }

//     /// <summary>
//     /// Changes to cells
//     /// </summary>
//     public MyNode? cells { get; set; }
// }

// /// <summary>
// /// A literal to identify a notebook document in the client.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class NotebookDocumentIdentifier{

//     /// <summary>
//     /// The notebook document's uri.
//     /// </summary>
//     public Uri uri { get; set; }
// }

// /// <summary>
// /// General parameters to register for a notification or to register a provider.
// /// </summary>
// public class Registration{

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again.
//     /// </summary>
//     public string id { get; set; }

//     /// <summary>
//     /// The method / capability to register for.
//     /// </summary>
//     public string method { get; set; }

//     /// <summary>
//     /// Options necessary for the registration.
//     /// </summary>
//     public LSPAny? registerOptions { get; set; }
// }

// /// <summary>
// /// General parameters to unregister a request or notification.
// /// </summary>
// public class Unregistration{

//     /// <summary>
//     /// The id used to unregister the request or notification. Usually an id
//     /// provided during the register request.
//     /// </summary>
//     public string id { get; set; }

//     /// <summary>
//     /// The method to unregister for.
//     /// </summary>
//     public string method { get; set; }
// }

// /// <summary>
// /// The initialize parameters
// /// </summary>
// public interface I_InitializeParams {

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// The process Id of the parent process that started
//     /// the server.
//     /// 
//     /// Is `null` if the process has not been started by another process.
//     /// If the parent process is not alive then the server should exit.
//     /// </summary>
//     public MyNode processId { get; set; }

//     /// <summary>
//     /// Information about the client
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public MyNode? clientInfo { get; set; }

//     /// <summary>
//     /// The locale the client is currently showing the user interface
//     /// in. This must not necessarily be the locale of the operating
//     /// system.
//     /// 
//     /// Uses IETF language tags as the value's syntax
//     /// (See https://en.wikipedia.org/wiki/IETF_language_tag)
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public string? locale { get; set; }

//     /// <summary>
//     /// The rootPath of the workspace. Is null
//     /// if no folder is open.
//     /// 
//     /// @deprecated in favour of rootUri.
//     /// </summary>
//     public MyNode? rootPath { get; set; }

//     /// <summary>
//     /// The rootUri of the workspace. Is null if no
//     /// folder is open. If both `rootPath` and `rootUri` are set
//     /// `rootUri` wins.
//     /// 
//     /// @deprecated in favour of workspaceFolders.
//     /// </summary>
//     public MyNode rootUri { get; set; }

//     /// <summary>
//     /// The capabilities provided by the client (editor or tool)
//     /// </summary>
//     public ClientCapabilities capabilities { get; set; }

//     /// <summary>
//     /// User provided initialization options.
//     /// </summary>
//     public LSPAny? initializationOptions { get; set; }

//     /// <summary>
//     /// The initial trace setting. If omitted trace is disabled ('off').
//     /// </summary>
//     public string? trace { get; set; }
// }

// /// <summary>
// /// The initialize parameters
// /// </summary>
// public class _InitializeParams: I_InitializeParams,IWorkDoneProgressParams{

//     /// <summary>
//     /// An optional token that a server can use to report work done progress.
//     /// extend from WorkDoneProgressParams
//     /// </summary>
//     public ProgressToken? workDoneToken { get; set; }

//     /// <summary>
//     /// The process Id of the parent process that started
//     /// the server.
//     /// 
//     /// Is `null` if the process has not been started by another process.
//     /// If the parent process is not alive then the server should exit.
//     /// </summary>
//     public MyNode processId { get; set; }

//     /// <summary>
//     /// Information about the client
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public MyNode? clientInfo { get; set; }

//     /// <summary>
//     /// The locale the client is currently showing the user interface
//     /// in. This must not necessarily be the locale of the operating
//     /// system.
//     /// 
//     /// Uses IETF language tags as the value's syntax
//     /// (See https://en.wikipedia.org/wiki/IETF_language_tag)
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public string? locale { get; set; }

//     /// <summary>
//     /// The rootPath of the workspace. Is null
//     /// if no folder is open.
//     /// 
//     /// @deprecated in favour of rootUri.
//     /// </summary>
//     public MyNode? rootPath { get; set; }

//     /// <summary>
//     /// The rootUri of the workspace. Is null if no
//     /// folder is open. If both `rootPath` and `rootUri` are set
//     /// `rootUri` wins.
//     /// 
//     /// @deprecated in favour of workspaceFolders.
//     /// </summary>
//     public MyNode rootUri { get; set; }

//     /// <summary>
//     /// The capabilities provided by the client (editor or tool)
//     /// </summary>
//     public ClientCapabilities capabilities { get; set; }

//     /// <summary>
//     /// User provided initialization options.
//     /// </summary>
//     public LSPAny? initializationOptions { get; set; }

//     /// <summary>
//     /// The initial trace setting. If omitted trace is disabled ('off').
//     /// </summary>
//     public string? trace { get; set; }
// }


// public interface IWorkspaceFoldersInitializeParams {

//     /// <summary>
//     /// The workspace folders configured in the client when the server starts.
//     /// 
//     /// This property is only available if the client supports workspace folders.
//     /// It can be `null` if the client supports workspace folders but none are
//     /// configured.
//     /// 
//     /// @since 3.6.0
//     /// </summary>
//     public MyNode? workspaceFolders { get; set; }
// }


// public class WorkspaceFoldersInitializeParams: IWorkspaceFoldersInitializeParams{

//     /// <summary>
//     /// The workspace folders configured in the client when the server starts.
//     /// 
//     /// This property is only available if the client supports workspace folders.
//     /// It can be `null` if the client supports workspace folders but none are
//     /// configured.
//     /// 
//     /// @since 3.6.0
//     /// </summary>
//     public MyNode? workspaceFolders { get; set; }
// }

// /// <summary>
// /// Defines the capabilities provided by a language
// /// server.
// /// </summary>
// public class ServerCapabilities{

//     /// <summary>
//     /// The position encoding the server picked from the encodings offered
//     /// by the client via the client capability `general.positionEncodings`.
//     /// 
//     /// If the client didn't provide any position encodings the only valid
//     /// value that a server can return is 'utf-16'.
//     /// 
//     /// If omitted it defaults to 'utf-16'.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public string? positionEncoding { get; set; }

//     /// <summary>
//     /// Defines how text documents are synced. Is either a detailed structure
//     /// defining each notification or for backwards compatibility the
//     /// TextDocumentSyncKind number.
//     /// </summary>
//     public MyNode? textDocumentSync { get; set; }

//     /// <summary>
//     /// Defines how notebook documents are synced.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public MyNode? notebookDocumentSync { get; set; }

//     /// <summary>
//     /// The server provides completion support.
//     /// </summary>
//     public CompletionOptions? completionProvider { get; set; }

//     /// <summary>
//     /// The server provides hover support.
//     /// </summary>
//     public MyNode? hoverProvider { get; set; }

//     /// <summary>
//     /// The server provides signature help support.
//     /// </summary>
//     public SignatureHelpOptions? signatureHelpProvider { get; set; }

//     /// <summary>
//     /// The server provides Goto Declaration support.
//     /// </summary>
//     public MyNode? declarationProvider { get; set; }

//     /// <summary>
//     /// The server provides goto definition support.
//     /// </summary>
//     public MyNode? definitionProvider { get; set; }

//     /// <summary>
//     /// The server provides Goto Type Definition support.
//     /// </summary>
//     public MyNode? typeDefinitionProvider { get; set; }

//     /// <summary>
//     /// The server provides Goto Implementation support.
//     /// </summary>
//     public MyNode? implementationProvider { get; set; }

//     /// <summary>
//     /// The server provides find references support.
//     /// </summary>
//     public MyNode? referencesProvider { get; set; }

//     /// <summary>
//     /// The server provides document highlight support.
//     /// </summary>
//     public MyNode? documentHighlightProvider { get; set; }

//     /// <summary>
//     /// The server provides document symbol support.
//     /// </summary>
//     public MyNode? documentSymbolProvider { get; set; }

//     /// <summary>
//     /// The server provides code actions. CodeActionOptions may only be
//     /// specified if the client states that it supports
//     /// `codeActionLiteralSupport` in its initial `initialize` request.
//     /// </summary>
//     public MyNode? codeActionProvider { get; set; }

//     /// <summary>
//     /// The server provides code lens.
//     /// </summary>
//     public CodeLensOptions? codeLensProvider { get; set; }

//     /// <summary>
//     /// The server provides document link support.
//     /// </summary>
//     public DocumentLinkOptions? documentLinkProvider { get; set; }

//     /// <summary>
//     /// The server provides color provider support.
//     /// </summary>
//     public MyNode? colorProvider { get; set; }

//     /// <summary>
//     /// The server provides workspace symbol support.
//     /// </summary>
//     public MyNode? workspaceSymbolProvider { get; set; }

//     /// <summary>
//     /// The server provides document formatting.
//     /// </summary>
//     public MyNode? documentFormattingProvider { get; set; }

//     /// <summary>
//     /// The server provides document range formatting.
//     /// </summary>
//     public MyNode? documentRangeFormattingProvider { get; set; }

//     /// <summary>
//     /// The server provides document formatting on typing.
//     /// </summary>
//     public DocumentOnTypeFormattingOptions? documentOnTypeFormattingProvider { get; set; }

//     /// <summary>
//     /// The server provides rename support. RenameOptions may only be
//     /// specified if the client states that it supports
//     /// `prepareSupport` in its initial `initialize` request.
//     /// </summary>
//     public MyNode? renameProvider { get; set; }

//     /// <summary>
//     /// The server provides folding provider support.
//     /// </summary>
//     public MyNode? foldingRangeProvider { get; set; }

//     /// <summary>
//     /// The server provides selection range support.
//     /// </summary>
//     public MyNode? selectionRangeProvider { get; set; }

//     /// <summary>
//     /// The server provides execute command support.
//     /// </summary>
//     public ExecuteCommandOptions? executeCommandProvider { get; set; }

//     /// <summary>
//     /// The server provides call hierarchy support.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public MyNode? callHierarchyProvider { get; set; }

//     /// <summary>
//     /// The server provides linked editing range support.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public MyNode? linkedEditingRangeProvider { get; set; }

//     /// <summary>
//     /// The server provides semantic tokens support.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public MyNode? semanticTokensProvider { get; set; }

//     /// <summary>
//     /// The server provides moniker support.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public MyNode? monikerProvider { get; set; }

//     /// <summary>
//     /// The server provides type hierarchy support.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public MyNode? typeHierarchyProvider { get; set; }

//     /// <summary>
//     /// The server provides inline values.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public MyNode? inlineValueProvider { get; set; }

//     /// <summary>
//     /// The server provides inlay hints.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public MyNode? inlayHintProvider { get; set; }

//     /// <summary>
//     /// The server has support for pull model diagnostics.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public MyNode? diagnosticProvider { get; set; }

//     /// <summary>
//     /// Workspace specific server capabilities.
//     /// </summary>
//     public MyNode? workspace { get; set; }

//     /// <summary>
//     /// Experimental server capabilities.
//     /// </summary>
//     public LSPAny? experimental { get; set; }
// }

// /// <summary>
// /// A text document identifier to denote a specific version of a text document.
// /// </summary>
// public class VersionedTextDocumentIdentifier: ITextDocumentIdentifier{

//     /// <summary>
//     /// The text document's uri.
//     /// extend from TextDocumentIdentifier
//     /// </summary>
//     public DocumentUri uri { get; set; }

//     /// <summary>
//     /// The version number of this document.
//     /// </summary>
//     public int version { get; set; }
// }

// /// <summary>
// /// Save options.
// /// </summary>
// public interface ISaveOptions {

//     /// <summary>
//     /// The client is supposed to include the content on save.
//     /// </summary>
//     public bool? includeText { get; set; }
// }

// /// <summary>
// /// Save options.
// /// </summary>
// public class SaveOptions: ISaveOptions{

//     /// <summary>
//     /// The client is supposed to include the content on save.
//     /// </summary>
//     public bool? includeText { get; set; }
// }

// /// <summary>
// /// An event describing a file change.
// /// </summary>
// public class FileEvent{

//     /// <summary>
//     /// The file's uri.
//     /// </summary>
//     public DocumentUri uri { get; set; }

//     /// <summary>
//     /// The change type.
//     /// </summary>
//     public uint type { get; set; }
// }


// public class FileSystemWatcher{

//     /// <summary>
//     /// The glob pattern to watch. See {@link GlobPattern glob pattern} for more detail.
//     /// 
//     /// @since 3.17.0 support for relative patterns.
//     /// </summary>
//     public GlobPattern globPattern { get; set; }

//     /// <summary>
//     /// The kind of events of interest. If omitted it defaults
//     /// to WatchKind.Create | WatchKind.Change | WatchKind.Delete
//     /// which is 7.
//     /// </summary>
//     public uint? kind { get; set; }
// }

// /// <summary>
// /// Represents a diagnostic, such as a compiler error or warning. Diagnostic objects
// /// are only valid in the scope of a resource.
// /// </summary>
// public class Diagnostic{

//     /// <summary>
//     /// The range at which the message applies
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// The diagnostic's severity. Can be omitted. If omitted it is up to the
//     /// client to interpret diagnostics as error, warning, info or hint.
//     /// </summary>
//     public uint? severity { get; set; }

//     /// <summary>
//     /// The diagnostic's code, which usually appear in the user interface.
//     /// </summary>
//     public MyNode? code { get; set; }

//     /// <summary>
//     /// An optional property to describe the error code.
//     /// Requires the code field (above) to be present/not null.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public CodeDescription? codeDescription { get; set; }

//     /// <summary>
//     /// A human-readable string describing the source of this
//     /// diagnostic, e.g. 'typescript' or 'super lint'. It usually
//     /// appears in the user interface.
//     /// </summary>
//     public string? source { get; set; }

//     /// <summary>
//     /// The diagnostic's message. It usually appears in the user interface
//     /// </summary>
//     public string message { get; set; }

//     /// <summary>
//     /// Additional metadata about the diagnostic.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public List<DiagnosticTag>? tags { get; set; }

//     /// <summary>
//     /// An array of related diagnostic information, e.g. when symbol-names within
//     /// a scope collide all definitions can be marked via this property.
//     /// </summary>
//     public List<DiagnosticRelatedInformation>? relatedInformation { get; set; }

//     /// <summary>
//     /// A data entry field that is preserved between a `textDocument/publishDiagnostics`
//     /// notification and `textDocument/codeAction` request.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public LSPAny? data { get; set; }
// }

// /// <summary>
// /// Contains additional information about the context in which a completion request is triggered.
// /// </summary>
// public class CompletionContext{

//     /// <summary>
//     /// How the completion was triggered.
//     /// </summary>
//     public uint triggerKind { get; set; }

//     /// <summary>
//     /// The trigger character (a single character) that has trigger code complete.
//     /// Is undefined if `triggerKind !== CompletionTriggerKind.TriggerCharacter`
//     /// </summary>
//     public string? triggerCharacter { get; set; }
// }

// /// <summary>
// /// Additional details for a completion item label.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class CompletionItemLabelDetails{

//     /// <summary>
//     /// An optional string which is rendered less prominently directly after {@link CompletionItem.label label},
//     /// without any spacing. Should be used for function signatures and type annotations.
//     /// </summary>
//     public string? detail { get; set; }

//     /// <summary>
//     /// An optional string which is rendered less prominently after {@link CompletionItem.detail}. Should be used
//     /// for fully qualified names and file paths.
//     /// </summary>
//     public string? description { get; set; }
// }

// /// <summary>
// /// A special text edit to provide an insert and a replace operation.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class InsertReplaceEdit{

//     /// <summary>
//     /// The string to be inserted.
//     /// </summary>
//     public string newText { get; set; }

//     /// <summary>
//     /// The range if the insert is requested
//     /// </summary>
//     public Range insert { get; set; }

//     /// <summary>
//     /// The range if the replace is requested.
//     /// </summary>
//     public Range replace { get; set; }
// }

// /// <summary>
// /// Completion options.
// /// </summary>
// public interface ICompletionOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// Most tools trigger completion request automatically without explicitly requesting
//     /// it using a keyboard shortcut (e.g. Ctrl+Space). Typically they do so when the user
//     /// starts to type an identifier. For example if the user types `c` in a JavaScript file
//     /// code complete will automatically pop up present `console` besides others as a
//     /// completion item. Characters that make up identifiers don't need to be listed here.
//     /// 
//     /// If code complete should automatically be trigger on characters not being valid inside
//     /// an identifier (for example `.` in JavaScript) list them in `triggerCharacters`.
//     /// </summary>
//     public List<string>? triggerCharacters { get; set; }

//     /// <summary>
//     /// The list of all possible characters that commit a completion. This field can be used
//     /// if clients don't support individual commit characters per completion item. See
//     /// `ClientCapabilities.textDocument.completion.completionItem.commitCharactersSupport`
//     /// 
//     /// If a server provides both `allCommitCharacters` and commit characters on an individual
//     /// completion item the ones on the completion item win.
//     /// 
//     /// @since 3.2.0
//     /// </summary>
//     public List<string>? allCommitCharacters { get; set; }

//     /// <summary>
//     /// The server provides support to resolve additional
//     /// information for a completion item.
//     /// </summary>
//     public bool? resolveProvider { get; set; }

//     /// <summary>
//     /// The server supports the following `CompletionItem` specific
//     /// capabilities.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public MyNode? completionItem { get; set; }
// }

// /// <summary>
// /// Completion options.
// /// </summary>
// public class CompletionOptions: ICompletionOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// Most tools trigger completion request automatically without explicitly requesting
//     /// it using a keyboard shortcut (e.g. Ctrl+Space). Typically they do so when the user
//     /// starts to type an identifier. For example if the user types `c` in a JavaScript file
//     /// code complete will automatically pop up present `console` besides others as a
//     /// completion item. Characters that make up identifiers don't need to be listed here.
//     /// 
//     /// If code complete should automatically be trigger on characters not being valid inside
//     /// an identifier (for example `.` in JavaScript) list them in `triggerCharacters`.
//     /// </summary>
//     public List<string>? triggerCharacters { get; set; }

//     /// <summary>
//     /// The list of all possible characters that commit a completion. This field can be used
//     /// if clients don't support individual commit characters per completion item. See
//     /// `ClientCapabilities.textDocument.completion.completionItem.commitCharactersSupport`
//     /// 
//     /// If a server provides both `allCommitCharacters` and commit characters on an individual
//     /// completion item the ones on the completion item win.
//     /// 
//     /// @since 3.2.0
//     /// </summary>
//     public List<string>? allCommitCharacters { get; set; }

//     /// <summary>
//     /// The server provides support to resolve additional
//     /// information for a completion item.
//     /// </summary>
//     public bool? resolveProvider { get; set; }

//     /// <summary>
//     /// The server supports the following `CompletionItem` specific
//     /// capabilities.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public MyNode? completionItem { get; set; }
// }

// /// <summary>
// /// Hover options.
// /// </summary>
// public interface IHoverOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Hover options.
// /// </summary>
// public class HoverOptions: IHoverOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Additional information about the context in which a signature help request was triggered.
// /// 
// /// @since 3.15.0
// /// </summary>
// public class SignatureHelpContext{

//     /// <summary>
//     /// Action that caused signature help to be triggered.
//     /// </summary>
//     public uint triggerKind { get; set; }

//     /// <summary>
//     /// Character that caused signature help to be triggered.
//     /// 
//     /// This is undefined when `triggerKind !== SignatureHelpTriggerKind.TriggerCharacter`
//     /// </summary>
//     public string? triggerCharacter { get; set; }

//     /// <summary>
//     /// `true` if signature help was already showing when it was triggered.
//     /// 
//     /// Retriggers occurs when the signature help is already active and can be caused by actions such as
//     /// typing a trigger character, a cursor move, or document content changes.
//     /// </summary>
//     public bool isRetrigger { get; set; }

//     /// <summary>
//     /// The currently active `SignatureHelp`.
//     /// 
//     /// The `activeSignatureHelp` has its `SignatureHelp.activeSignature` field updated based on
//     /// the user navigating through available signatures.
//     /// </summary>
//     public SignatureHelp? activeSignatureHelp { get; set; }
// }

// /// <summary>
// /// Represents the signature of something callable. A signature
// /// can have a label, like a function-name, a doc-comment, and
// /// a set of parameters.
// /// </summary>
// public class SignatureInformation{

//     /// <summary>
//     /// The label of this signature. Will be shown in
//     /// the UI.
//     /// </summary>
//     public string label { get; set; }

//     /// <summary>
//     /// The human-readable doc-comment of this signature. Will be shown
//     /// in the UI but can be omitted.
//     /// </summary>
//     public MyNode? documentation { get; set; }

//     /// <summary>
//     /// The parameters of this signature.
//     /// </summary>
//     public List<ParameterInformation>? parameters { get; set; }

//     /// <summary>
//     /// The index of the active parameter.
//     /// 
//     /// If provided, this is used in place of `SignatureHelp.activeParameter`.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public uint? activeParameter { get; set; }
// }

// /// <summary>
// /// Server Capabilities for a {@link SignatureHelpRequest}.
// /// </summary>
// public interface ISignatureHelpOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// List of characters that trigger signature help automatically.
//     /// </summary>
//     public List<string>? triggerCharacters { get; set; }

//     /// <summary>
//     /// List of characters that re-trigger signature help.
//     /// 
//     /// These trigger characters are only active when signature help is already showing. All trigger characters
//     /// are also counted as re-trigger characters.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public List<string>? retriggerCharacters { get; set; }
// }

// /// <summary>
// /// Server Capabilities for a {@link SignatureHelpRequest}.
// /// </summary>
// public class SignatureHelpOptions: ISignatureHelpOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// List of characters that trigger signature help automatically.
//     /// </summary>
//     public List<string>? triggerCharacters { get; set; }

//     /// <summary>
//     /// List of characters that re-trigger signature help.
//     /// 
//     /// These trigger characters are only active when signature help is already showing. All trigger characters
//     /// are also counted as re-trigger characters.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public List<string>? retriggerCharacters { get; set; }
// }

// /// <summary>
// /// Server Capabilities for a {@link DefinitionRequest}.
// /// </summary>
// public interface IDefinitionOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Server Capabilities for a {@link DefinitionRequest}.
// /// </summary>
// public class DefinitionOptions: IDefinitionOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Value-object that contains additional information when
// /// requesting references.
// /// </summary>
// public class ReferenceContext{

//     /// <summary>
//     /// Include the declaration of the current symbol.
//     /// </summary>
//     public bool includeDeclaration { get; set; }
// }

// /// <summary>
// /// Reference options.
// /// </summary>
// public interface IReferenceOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Reference options.
// /// </summary>
// public class ReferenceOptions: IReferenceOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Provider options for a {@link DocumentHighlightRequest}.
// /// </summary>
// public interface IDocumentHighlightOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Provider options for a {@link DocumentHighlightRequest}.
// /// </summary>
// public class DocumentHighlightOptions: IDocumentHighlightOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// A base for all symbol information.
// /// </summary>
// public interface IBaseSymbolInformation {

//     /// <summary>
//     /// The name of this symbol.
//     /// </summary>
//     public string name { get; set; }

//     /// <summary>
//     /// The kind of this symbol.
//     /// </summary>
//     public uint kind { get; set; }

//     /// <summary>
//     /// Tags for this symbol.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public List<SymbolTag>? tags { get; set; }

//     /// <summary>
//     /// The name of the symbol containing this symbol. This information is for
//     /// user interface purposes (e.g. to render a qualifier in the user interface
//     /// if necessary). It can't be used to re-infer a hierarchy for the document
//     /// symbols.
//     /// </summary>
//     public string? containerName { get; set; }
// }

// /// <summary>
// /// A base for all symbol information.
// /// </summary>
// public class BaseSymbolInformation: IBaseSymbolInformation{

//     /// <summary>
//     /// The name of this symbol.
//     /// </summary>
//     public string name { get; set; }

//     /// <summary>
//     /// The kind of this symbol.
//     /// </summary>
//     public uint kind { get; set; }

//     /// <summary>
//     /// Tags for this symbol.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public List<SymbolTag>? tags { get; set; }

//     /// <summary>
//     /// The name of the symbol containing this symbol. This information is for
//     /// user interface purposes (e.g. to render a qualifier in the user interface
//     /// if necessary). It can't be used to re-infer a hierarchy for the document
//     /// symbols.
//     /// </summary>
//     public string? containerName { get; set; }
// }

// /// <summary>
// /// Provider options for a {@link DocumentSymbolRequest}.
// /// </summary>
// public interface IDocumentSymbolOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// A human-readable string that is shown when multiple outlines trees
//     /// are shown for the same document.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public string? label { get; set; }
// }

// /// <summary>
// /// Provider options for a {@link DocumentSymbolRequest}.
// /// </summary>
// public class DocumentSymbolOptions: IDocumentSymbolOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// A human-readable string that is shown when multiple outlines trees
//     /// are shown for the same document.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public string? label { get; set; }
// }

// /// <summary>
// /// Contains additional diagnostic information about the context in which
// /// a {@link CodeActionProvider.provideCodeActions code action} is run.
// /// </summary>
// public class CodeActionContext{

//     /// <summary>
//     /// An array of diagnostics known on the client side overlapping the range provided to the
//     /// `textDocument/codeAction` request. They are provided so that the server knows which
//     /// errors are currently presented to the user for the given range. There is no guarantee
//     /// that these accurately reflect the error state of the resource. The primary parameter
//     /// to compute code actions is the provided range.
//     /// </summary>
//     public List<Diagnostic> diagnostics { get; set; }

//     /// <summary>
//     /// Requested kind of actions to return.
//     /// 
//     /// Actions not of this kind are filtered out by the client before being shown. So servers
//     /// can omit computing them.
//     /// </summary>
//     public List<CodeActionKind>? only { get; set; }

//     /// <summary>
//     /// The reason why code actions were requested.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public uint? triggerKind { get; set; }
// }

// /// <summary>
// /// Provider options for a {@link CodeActionRequest}.
// /// </summary>
// public interface ICodeActionOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// CodeActionKinds that this server may return.
//     /// 
//     /// The list of kinds may be generic, such as `CodeActionKind.Refactor`, or the server
//     /// may list out every specific kind they provide.
//     /// </summary>
//     public List<CodeActionKind>? codeActionKinds { get; set; }

//     /// <summary>
//     /// The server provides support to resolve additional
//     /// information for a code action.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public bool? resolveProvider { get; set; }
// }

// /// <summary>
// /// Provider options for a {@link CodeActionRequest}.
// /// </summary>
// public class CodeActionOptions: ICodeActionOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// CodeActionKinds that this server may return.
//     /// 
//     /// The list of kinds may be generic, such as `CodeActionKind.Refactor`, or the server
//     /// may list out every specific kind they provide.
//     /// </summary>
//     public List<CodeActionKind>? codeActionKinds { get; set; }

//     /// <summary>
//     /// The server provides support to resolve additional
//     /// information for a code action.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public bool? resolveProvider { get; set; }
// }

// /// <summary>
// /// Server capabilities for a {@link WorkspaceSymbolRequest}.
// /// </summary>
// public interface IWorkspaceSymbolOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The server provides support to resolve additional
//     /// information for a workspace symbol.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public bool? resolveProvider { get; set; }
// }

// /// <summary>
// /// Server capabilities for a {@link WorkspaceSymbolRequest}.
// /// </summary>
// public class WorkspaceSymbolOptions: IWorkspaceSymbolOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The server provides support to resolve additional
//     /// information for a workspace symbol.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public bool? resolveProvider { get; set; }
// }

// /// <summary>
// /// Code Lens provider options of a {@link CodeLensRequest}.
// /// </summary>
// public interface ICodeLensOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// Code lens has a resolve provider as well.
//     /// </summary>
//     public bool? resolveProvider { get; set; }
// }

// /// <summary>
// /// Code Lens provider options of a {@link CodeLensRequest}.
// /// </summary>
// public class CodeLensOptions: ICodeLensOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// Code lens has a resolve provider as well.
//     /// </summary>
//     public bool? resolveProvider { get; set; }
// }

// /// <summary>
// /// Provider options for a {@link DocumentLinkRequest}.
// /// </summary>
// public interface IDocumentLinkOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// Document links have a resolve provider as well.
//     /// </summary>
//     public bool? resolveProvider { get; set; }
// }

// /// <summary>
// /// Provider options for a {@link DocumentLinkRequest}.
// /// </summary>
// public class DocumentLinkOptions: IDocumentLinkOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// Document links have a resolve provider as well.
//     /// </summary>
//     public bool? resolveProvider { get; set; }
// }

// /// <summary>
// /// Value-object describing what options formatting should use.
// /// </summary>
// public class FormattingOptions{

//     /// <summary>
//     /// Size of a tab in spaces.
//     /// </summary>
//     public uint tabSize { get; set; }

//     /// <summary>
//     /// Prefer spaces over tabs.
//     /// </summary>
//     public bool insertSpaces { get; set; }

//     /// <summary>
//     /// Trim trailing whitespace on a line.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public bool? trimTrailingWhitespace { get; set; }

//     /// <summary>
//     /// Insert a newline character at the end of the file if one does not exist.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public bool? insertFinalNewline { get; set; }

//     /// <summary>
//     /// Trim all newlines after the final newline at the end of the file.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public bool? trimFinalNewlines { get; set; }
// }

// /// <summary>
// /// Provider options for a {@link DocumentFormattingRequest}.
// /// </summary>
// public interface IDocumentFormattingOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Provider options for a {@link DocumentFormattingRequest}.
// /// </summary>
// public class DocumentFormattingOptions: IDocumentFormattingOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Provider options for a {@link DocumentRangeFormattingRequest}.
// /// </summary>
// public interface IDocumentRangeFormattingOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Provider options for a {@link DocumentRangeFormattingRequest}.
// /// </summary>
// public class DocumentRangeFormattingOptions: IDocumentRangeFormattingOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }
// }

// /// <summary>
// /// Provider options for a {@link DocumentOnTypeFormattingRequest}.
// /// </summary>
// public interface IDocumentOnTypeFormattingOptions {

//     /// <summary>
//     /// A character on which formatting should be triggered, like `{`.
//     /// </summary>
//     public string firstTriggerCharacter { get; set; }

//     /// <summary>
//     /// More trigger characters.
//     /// </summary>
//     public List<string>? moreTriggerCharacter { get; set; }
// }

// /// <summary>
// /// Provider options for a {@link DocumentOnTypeFormattingRequest}.
// /// </summary>
// public class DocumentOnTypeFormattingOptions: IDocumentOnTypeFormattingOptions{

//     /// <summary>
//     /// A character on which formatting should be triggered, like `{`.
//     /// </summary>
//     public string firstTriggerCharacter { get; set; }

//     /// <summary>
//     /// More trigger characters.
//     /// </summary>
//     public List<string>? moreTriggerCharacter { get; set; }
// }

// /// <summary>
// /// Provider options for a {@link RenameRequest}.
// /// </summary>
// public interface IRenameOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// Renames should be checked and tested before being executed.
//     /// 
//     /// @since version 3.12.0
//     /// </summary>
//     public bool? prepareProvider { get; set; }
// }

// /// <summary>
// /// Provider options for a {@link RenameRequest}.
// /// </summary>
// public class RenameOptions: IRenameOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// Renames should be checked and tested before being executed.
//     /// 
//     /// @since version 3.12.0
//     /// </summary>
//     public bool? prepareProvider { get; set; }
// }

// /// <summary>
// /// The server capabilities of a {@link ExecuteCommandRequest}.
// /// </summary>
// public interface IExecuteCommandOptions {

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The commands to be executed on the server
//     /// </summary>
//     public List<string> commands { get; set; }
// }

// /// <summary>
// /// The server capabilities of a {@link ExecuteCommandRequest}.
// /// </summary>
// public class ExecuteCommandOptions: IExecuteCommandOptions,IWorkDoneProgressOptions{

//     /// <summary>
//     /// 
//     /// extend from WorkDoneProgressOptions
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// The commands to be executed on the server
//     /// </summary>
//     public List<string> commands { get; set; }
// }

// /// <summary>
// /// @since 3.16.0
// /// </summary>
// public class SemanticTokensLegend{

//     /// <summary>
//     /// The token types a server uses.
//     /// </summary>
//     public List<string> tokenTypes { get; set; }

//     /// <summary>
//     /// The token modifiers a server uses.
//     /// </summary>
//     public List<string> tokenModifiers { get; set; }
// }

// /// <summary>
// /// A text document identifier to optionally denote a specific version of a text document.
// /// </summary>
// public class OptionalVersionedTextDocumentIdentifier: ITextDocumentIdentifier{

//     /// <summary>
//     /// The text document's uri.
//     /// extend from TextDocumentIdentifier
//     /// </summary>
//     public DocumentUri uri { get; set; }

//     /// <summary>
//     /// The version number of this document. If a versioned text document identifier
//     /// is sent from the server to the client and the file is not open in the editor
//     /// (the server has not received an open notification before) the server can send
//     /// `null` to indicate that the version is unknown and the content on disk is the
//     /// truth (as specified with document content ownership).
//     /// </summary>
//     public MyNode version { get; set; }
// }

// /// <summary>
// /// A special text edit with an additional change annotation.
// /// 
// /// @since 3.16.0.
// /// </summary>
// public class AnnotatedTextEdit: ITextEdit{

//     /// <summary>
//     /// The range of the text document to be manipulated. To insert
//     /// text into a document create a range where start === end.
//     /// extend from TextEdit
//     /// </summary>
//     public Range range { get; set; }

//     /// <summary>
//     /// The string to be inserted. For delete operations use an
//     /// empty string.
//     /// extend from TextEdit
//     /// </summary>
//     public string newText { get; set; }

//     /// <summary>
//     /// The actual identifier of the change annotation
//     /// </summary>
//     public ChangeAnnotationIdentifier annotationId { get; set; }
// }

// /// <summary>
// /// A generic resource operation.
// /// </summary>
// public interface IResourceOperation {

//     /// <summary>
//     /// The resource operation kind.
//     /// </summary>
//     public string kind { get; set; }

//     /// <summary>
//     /// An optional annotation identifier describing the operation.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public ChangeAnnotationIdentifier? annotationId { get; set; }
// }

// /// <summary>
// /// A generic resource operation.
// /// </summary>
// public class ResourceOperation: IResourceOperation{

//     /// <summary>
//     /// The resource operation kind.
//     /// </summary>
//     public string kind { get; set; }

//     /// <summary>
//     /// An optional annotation identifier describing the operation.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public ChangeAnnotationIdentifier? annotationId { get; set; }
// }

// /// <summary>
// /// Options to create a file.
// /// </summary>
// public class CreateFileOptions{

//     /// <summary>
//     /// Overwrite existing file. Overwrite wins over `ignoreIfExists`
//     /// </summary>
//     public bool? overwrite { get; set; }

//     /// <summary>
//     /// Ignore if exists.
//     /// </summary>
//     public bool? ignoreIfExists { get; set; }
// }

// /// <summary>
// /// Rename file options
// /// </summary>
// public class RenameFileOptions{

//     /// <summary>
//     /// Overwrite target if existing. Overwrite wins over `ignoreIfExists`
//     /// </summary>
//     public bool? overwrite { get; set; }

//     /// <summary>
//     /// Ignores if target exists.
//     /// </summary>
//     public bool? ignoreIfExists { get; set; }
// }

// /// <summary>
// /// Delete file options
// /// </summary>
// public class DeleteFileOptions{

//     /// <summary>
//     /// Delete the content recursively if a folder is denoted.
//     /// </summary>
//     public bool? recursive { get; set; }

//     /// <summary>
//     /// Ignore the operation if the file doesn't exist.
//     /// </summary>
//     public bool? ignoreIfNotExists { get; set; }
// }

// /// <summary>
// /// A pattern to describe in which file operation requests or notifications
// /// the server is interested in receiving.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class FileOperationPattern{

//     /// <summary>
//     /// The glob pattern to match. Glob patterns can have the following syntax:
//     /// - `*` to match one or more characters in a path segment
//     /// - `?` to match on one character in a path segment
//     /// - `**` to match any number of path segments, including none
//     /// - `{}` to group sub patterns into an OR expression. (e.g. `**​/*.{ts,js}` matches all TypeScript and JavaScript files)
//     /// - `[]` to declare a range of characters to match in a path segment (e.g., `example.[0-9]` to match on `example.0`, `example.1`, …)
//     /// - `[!...]` to negate a range of characters to match in a path segment (e.g., `example.[!0-9]` to match on `example.a`, `example.b`, but not `example.0`)
//     /// </summary>
//     public string glob { get; set; }

//     /// <summary>
//     /// Whether to match files or folders with this pattern.
//     /// 
//     /// Matches both if undefined.
//     /// </summary>
//     public string? matches { get; set; }

//     /// <summary>
//     /// Additional options used during matching.
//     /// </summary>
//     public FileOperationPatternOptions? options { get; set; }
// }

// /// <summary>
// /// A full document diagnostic report for a workspace diagnostic result.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class WorkspaceFullDocumentDiagnosticReport: IFullDocumentDiagnosticReport{

//     /// <summary>
//     /// A full document diagnostic report.
//     /// extend from FullDocumentDiagnosticReport
//     /// </summary>
//     public string kind { get; set; } = "full"; 

//     /// <summary>
//     /// An optional result id. If provided it will
//     /// be sent on the next diagnostic request for the
//     /// same document.
//     /// extend from FullDocumentDiagnosticReport
//     /// </summary>
//     public string? resultId { get; set; }

//     /// <summary>
//     /// The actual items.
//     /// extend from FullDocumentDiagnosticReport
//     /// </summary>
//     public List<Diagnostic> items { get; set; }

//     /// <summary>
//     /// The URI for which diagnostic information is reported.
//     /// </summary>
//     public DocumentUri uri { get; set; }

//     /// <summary>
//     /// The version number for which the diagnostics are reported.
//     /// If the document is not marked as open `null` can be provided.
//     /// </summary>
//     public MyNode version { get; set; }
// }

// /// <summary>
// /// An unchanged document diagnostic report for a workspace diagnostic result.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class WorkspaceUnchangedDocumentDiagnosticReport: IUnchangedDocumentDiagnosticReport{

//     /// <summary>
//     /// A document diagnostic report indicating
//     /// no changes to the last result. A server can
//     /// only return `unchanged` if result ids are
//     /// provided.
//     /// extend from UnchangedDocumentDiagnosticReport
//     /// </summary>
//     public string kind { get; set; } = "unchanged"; 

//     /// <summary>
//     /// A result id which will be sent on the next
//     /// diagnostic request for the same document.
//     /// extend from UnchangedDocumentDiagnosticReport
//     /// </summary>
//     public string resultId { get; set; }

//     /// <summary>
//     /// The URI for which diagnostic information is reported.
//     /// </summary>
//     public DocumentUri uri { get; set; }

//     /// <summary>
//     /// The version number for which the diagnostics are reported.
//     /// If the document is not marked as open `null` can be provided.
//     /// </summary>
//     public MyNode version { get; set; }
// }

// /// <summary>
// /// A notebook cell.
// /// 
// /// A cell's document URI must be unique across ALL notebook
// /// cells and can therefore be used to uniquely identify a
// /// notebook cell or the cell's text document.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class NotebookCell{

//     /// <summary>
//     /// The cell's kind
//     /// </summary>
//     public uint kind { get; set; }

//     /// <summary>
//     /// The URI of the cell's text document
//     /// content.
//     /// </summary>
//     public DocumentUri document { get; set; }

//     /// <summary>
//     /// Additional metadata stored with the cell.
//     /// 
//     /// Note: should always be an object literal (e.g. LSPObject)
//     /// </summary>
//     public LSPObject? metadata { get; set; }

//     /// <summary>
//     /// Additional execution summary information
//     /// if supported by the client.
//     /// </summary>
//     public ExecutionSummary? executionSummary { get; set; }
// }

// /// <summary>
// /// A change describing how to move a `NotebookCell`
// /// array from state S to S'.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class NotebookCellArrayChange{

//     /// <summary>
//     /// The start oftest of the cell that changed.
//     /// </summary>
//     public uint start { get; set; }

//     /// <summary>
//     /// The deleted cells
//     /// </summary>
//     public uint deleteCount { get; set; }

//     /// <summary>
//     /// The new cells, if any
//     /// </summary>
//     public List<NotebookCell>? cells { get; set; }
// }

// /// <summary>
// /// Defines the capabilities provided by the client.
// /// </summary>
// public class ClientCapabilities{

//     /// <summary>
//     /// Workspace specific client capabilities.
//     /// </summary>
//     public WorkspaceClientCapabilities? workspace { get; set; }

//     /// <summary>
//     /// Text document specific client capabilities.
//     /// </summary>
//     public TextDocumentClientCapabilities? textDocument { get; set; }

//     /// <summary>
//     /// Capabilities specific to the notebook document support.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public NotebookDocumentClientCapabilities? notebookDocument { get; set; }

//     /// <summary>
//     /// Window specific client capabilities.
//     /// </summary>
//     public WindowClientCapabilities? window { get; set; }

//     /// <summary>
//     /// General client capabilities.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public GeneralClientCapabilities? general { get; set; }

//     /// <summary>
//     /// Experimental client capabilities.
//     /// </summary>
//     public LSPAny? experimental { get; set; }
// }


// public class TextDocumentSyncOptions{

//     /// <summary>
//     /// Open and close notifications are sent to the server. If omitted open close notification should not
//     /// be sent.
//     /// </summary>
//     public bool? openClose { get; set; }

//     /// <summary>
//     /// Change notifications are sent to the server. See TextDocumentSyncKind.None, TextDocumentSyncKind.Full
//     /// and TextDocumentSyncKind.Incremental. If omitted it defaults to TextDocumentSyncKind.None.
//     /// </summary>
//     public uint? change { get; set; }

//     /// <summary>
//     /// If present will save notifications are sent to the server. If omitted the notification should not be
//     /// sent.
//     /// </summary>
//     public bool? willSave { get; set; }

//     /// <summary>
//     /// If present will save wait until requests are sent to the server. If omitted the request should not be
//     /// sent.
//     /// </summary>
//     public bool? willSaveWaitUntil { get; set; }

//     /// <summary>
//     /// If present save notifications are sent to the server. If omitted the notification should not be
//     /// sent.
//     /// </summary>
//     public MyNode? save { get; set; }
// }

// /// <summary>
// /// Options specific to a notebook plus its cells
// /// to be synced to the server.
// /// 
// /// If a selector provides a notebook document
// /// filter but no cell selector all cells of a
// /// matching notebook document will be synced.
// /// 
// /// If a selector provides no notebook document
// /// filter but only a cell selector all notebook
// /// document that contain at least one matching
// /// cell will be synced.
// /// 
// /// @since 3.17.0
// /// </summary>
// public interface INotebookDocumentSyncOptions {

//     /// <summary>
//     /// The notebooks to be synced
//     /// </summary>
//     public List<MyNode> notebookSelector { get; set; }

//     /// <summary>
//     /// Whether save notification should be forwarded to
//     /// the server. Will only be honored if mode === `notebook`.
//     /// </summary>
//     public bool? save { get; set; }
// }

// /// <summary>
// /// Options specific to a notebook plus its cells
// /// to be synced to the server.
// /// 
// /// If a selector provides a notebook document
// /// filter but no cell selector all cells of a
// /// matching notebook document will be synced.
// /// 
// /// If a selector provides no notebook document
// /// filter but only a cell selector all notebook
// /// document that contain at least one matching
// /// cell will be synced.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class NotebookDocumentSyncOptions: INotebookDocumentSyncOptions{

//     /// <summary>
//     /// The notebooks to be synced
//     /// </summary>
//     public List<MyNode> notebookSelector { get; set; }

//     /// <summary>
//     /// Whether save notification should be forwarded to
//     /// the server. Will only be honored if mode === `notebook`.
//     /// </summary>
//     public bool? save { get; set; }
// }

// /// <summary>
// /// Registration options specific to a notebook.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class NotebookDocumentSyncRegistrationOptions: INotebookDocumentSyncOptions,IStaticRegistrationOptions{

//     /// <summary>
//     /// The notebooks to be synced
//     /// extend from NotebookDocumentSyncOptions
//     /// </summary>
//     public List<MyNode> notebookSelector { get; set; }

//     /// <summary>
//     /// Whether save notification should be forwarded to
//     /// the server. Will only be honored if mode === `notebook`.
//     /// extend from NotebookDocumentSyncOptions
//     /// </summary>
//     public bool? save { get; set; }

//     /// <summary>
//     /// The id used to register the request. The id can be used to deregister
//     /// the request again. See also Registration#id.
//     /// extend from StaticRegistrationOptions
//     /// </summary>
//     public string? id { get; set; }
// }


// public class WorkspaceFoldersServerCapabilities{

//     /// <summary>
//     /// The server has support for workspace folders
//     /// </summary>
//     public bool? supported { get; set; }

//     /// <summary>
//     /// Whether the server wants to receive workspace folder
//     /// change notifications.
//     /// 
//     /// If a string is provided the string is treated as an ID
//     /// under which the notification is registered on the client
//     /// side. The ID can be used to unregister for these events
//     /// using the `client/unregisterCapability` request.
//     /// </summary>
//     public MyNode? changeNotifications { get; set; }
// }

// /// <summary>
// /// Options for notifications/requests for user operations on files.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class FileOperationOptions{

//     /// <summary>
//     /// The server is interested in receiving didCreateFiles notifications.
//     /// </summary>
//     public FileOperationRegistrationOptions? didCreate { get; set; }

//     /// <summary>
//     /// The server is interested in receiving willCreateFiles requests.
//     /// </summary>
//     public FileOperationRegistrationOptions? willCreate { get; set; }

//     /// <summary>
//     /// The server is interested in receiving didRenameFiles notifications.
//     /// </summary>
//     public FileOperationRegistrationOptions? didRename { get; set; }

//     /// <summary>
//     /// The server is interested in receiving willRenameFiles requests.
//     /// </summary>
//     public FileOperationRegistrationOptions? willRename { get; set; }

//     /// <summary>
//     /// The server is interested in receiving didDeleteFiles file notifications.
//     /// </summary>
//     public FileOperationRegistrationOptions? didDelete { get; set; }

//     /// <summary>
//     /// The server is interested in receiving willDeleteFiles file requests.
//     /// </summary>
//     public FileOperationRegistrationOptions? willDelete { get; set; }
// }

// /// <summary>
// /// Structure to capture a description for an error code.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class CodeDescription{

//     /// <summary>
//     /// An URI to open with more information about the diagnostic error.
//     /// </summary>
//     public Uri href { get; set; }
// }

// /// <summary>
// /// Represents a related message and source code location for a diagnostic. This should be
// /// used to point to code locations that cause or related to a diagnostics, e.g when duplicating
// /// a symbol in a scope.
// /// </summary>
// public class DiagnosticRelatedInformation{

//     /// <summary>
//     /// The location of this related diagnostic information.
//     /// </summary>
//     public Location location { get; set; }

//     /// <summary>
//     /// The message of this related diagnostic information.
//     /// </summary>
//     public string message { get; set; }
// }

// /// <summary>
// /// Represents a parameter of a callable-signature. A parameter can
// /// have a label and a doc-comment.
// /// </summary>
// public class ParameterInformation{

//     /// <summary>
//     /// The label of this parameter information.
//     /// 
//     /// Either a string or an inclusive start and exclusive end offsets within its containing
//     /// signature label. (see SignatureInformation.label). The offsets are based on a UTF-16
//     /// string representation as `Position` and `Range` does.
//     /// 
//     /// *Note*: a label of type string should be a substring of its containing signature label.
//     /// Its intended use case is to highlight the parameter label part in the `SignatureInformation.label`.
//     /// </summary>
//     public MyNode label { get; set; }

//     /// <summary>
//     /// The human-readable doc-comment of this parameter. Will be shown
//     /// in the UI but can be omitted.
//     /// </summary>
//     public MyNode? documentation { get; set; }
// }

// /// <summary>
// /// A notebook cell text document filter denotes a cell text
// /// document by different properties.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class NotebookCellTextDocumentFilter{

//     /// <summary>
//     /// A filter that matches against the notebook
//     /// containing the notebook cell. If a string
//     /// value is provided it matches against the
//     /// notebook type. '*' matches every notebook.
//     /// </summary>
//     public MyNode notebook { get; set; }

//     /// <summary>
//     /// A language id like `python`.
//     /// 
//     /// Will be matched against the language id of the
//     /// notebook cell document. '*' matches every language.
//     /// </summary>
//     public string? language { get; set; }
// }

// /// <summary>
// /// Matching options for the file operation pattern.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class FileOperationPatternOptions{

//     /// <summary>
//     /// The pattern should be matched ignoring casing.
//     /// </summary>
//     public bool? ignoreCase { get; set; }
// }


// public class ExecutionSummary{

//     /// <summary>
//     /// A strict monotonically increasing value
//     /// indicating the execution order of a cell
//     /// inside a notebook.
//     /// </summary>
//     public uint executionOrder { get; set; }

//     /// <summary>
//     /// Whether the execution was successful or
//     /// not if known by the client.
//     /// </summary>
//     public bool? success { get; set; }
// }

// /// <summary>
// /// Workspace specific client capabilities.
// /// </summary>
// public class WorkspaceClientCapabilities{

//     /// <summary>
//     /// The client supports applying batch edits
//     /// to the workspace by supporting the request
//     /// 'workspace/applyEdit'
//     /// </summary>
//     public bool? applyEdit { get; set; }

//     /// <summary>
//     /// Capabilities specific to `WorkspaceEdit`s.
//     /// </summary>
//     public WorkspaceEditClientCapabilities? workspaceEdit { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `workspace/didChangeConfiguration` notification.
//     /// </summary>
//     public DidChangeConfigurationClientCapabilities? didChangeConfiguration { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `workspace/didChangeWatchedFiles` notification.
//     /// </summary>
//     public DidChangeWatchedFilesClientCapabilities? didChangeWatchedFiles { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `workspace/symbol` request.
//     /// </summary>
//     public WorkspaceSymbolClientCapabilities? symbol { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `workspace/executeCommand` request.
//     /// </summary>
//     public ExecuteCommandClientCapabilities? executeCommand { get; set; }

//     /// <summary>
//     /// The client has support for workspace folders.
//     /// 
//     /// @since 3.6.0
//     /// </summary>
//     public bool? workspaceFolders { get; set; }

//     /// <summary>
//     /// The client supports `workspace/configuration` requests.
//     /// 
//     /// @since 3.6.0
//     /// </summary>
//     public bool? configuration { get; set; }

//     /// <summary>
//     /// Capabilities specific to the semantic token requests scoped to the
//     /// workspace.
//     /// 
//     /// @since 3.16.0.
//     /// </summary>
//     public SemanticTokensWorkspaceClientCapabilities? semanticTokens { get; set; }

//     /// <summary>
//     /// Capabilities specific to the code lens requests scoped to the
//     /// workspace.
//     /// 
//     /// @since 3.16.0.
//     /// </summary>
//     public CodeLensWorkspaceClientCapabilities? codeLens { get; set; }

//     /// <summary>
//     /// The client has support for file notifications/requests for user operations on files.
//     /// 
//     /// Since 3.16.0
//     /// </summary>
//     public FileOperationClientCapabilities? fileOperations { get; set; }

//     /// <summary>
//     /// Capabilities specific to the inline values requests scoped to the
//     /// workspace.
//     /// 
//     /// @since 3.17.0.
//     /// </summary>
//     public InlineValueWorkspaceClientCapabilities? inlineValue { get; set; }

//     /// <summary>
//     /// Capabilities specific to the inlay hint requests scoped to the
//     /// workspace.
//     /// 
//     /// @since 3.17.0.
//     /// </summary>
//     public InlayHintWorkspaceClientCapabilities? inlayHint { get; set; }

//     /// <summary>
//     /// Capabilities specific to the diagnostic requests scoped to the
//     /// workspace.
//     /// 
//     /// @since 3.17.0.
//     /// </summary>
//     public DiagnosticWorkspaceClientCapabilities? diagnostics { get; set; }
// }

// /// <summary>
// /// Text document specific client capabilities.
// /// </summary>
// public class TextDocumentClientCapabilities{

//     /// <summary>
//     /// Defines which synchronization capabilities the client supports.
//     /// </summary>
//     public TextDocumentSyncClientCapabilities? synchronization { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/completion` request.
//     /// </summary>
//     public CompletionClientCapabilities? completion { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/hover` request.
//     /// </summary>
//     public HoverClientCapabilities? hover { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/signatureHelp` request.
//     /// </summary>
//     public SignatureHelpClientCapabilities? signatureHelp { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/declaration` request.
//     /// 
//     /// @since 3.14.0
//     /// </summary>
//     public DeclarationClientCapabilities? declaration { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/definition` request.
//     /// </summary>
//     public DefinitionClientCapabilities? definition { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/typeDefinition` request.
//     /// 
//     /// @since 3.6.0
//     /// </summary>
//     public TypeDefinitionClientCapabilities? typeDefinition { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/implementation` request.
//     /// 
//     /// @since 3.6.0
//     /// </summary>
//     public ImplementationClientCapabilities? implementation { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/references` request.
//     /// </summary>
//     public ReferenceClientCapabilities? references { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/documentHighlight` request.
//     /// </summary>
//     public DocumentHighlightClientCapabilities? documentHighlight { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/documentSymbol` request.
//     /// </summary>
//     public DocumentSymbolClientCapabilities? documentSymbol { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/codeAction` request.
//     /// </summary>
//     public CodeActionClientCapabilities? codeAction { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/codeLens` request.
//     /// </summary>
//     public CodeLensClientCapabilities? codeLens { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/documentLink` request.
//     /// </summary>
//     public DocumentLinkClientCapabilities? documentLink { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/documentColor` and the
//     /// `textDocument/colorPresentation` request.
//     /// 
//     /// @since 3.6.0
//     /// </summary>
//     public DocumentColorClientCapabilities? colorProvider { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/formatting` request.
//     /// </summary>
//     public DocumentFormattingClientCapabilities? formatting { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/rangeFormatting` request.
//     /// </summary>
//     public DocumentRangeFormattingClientCapabilities? rangeFormatting { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/onTypeFormatting` request.
//     /// </summary>
//     public DocumentOnTypeFormattingClientCapabilities? onTypeFormatting { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/rename` request.
//     /// </summary>
//     public RenameClientCapabilities? rename { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/foldingRange` request.
//     /// 
//     /// @since 3.10.0
//     /// </summary>
//     public FoldingRangeClientCapabilities? foldingRange { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/selectionRange` request.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public SelectionRangeClientCapabilities? selectionRange { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/publishDiagnostics` notification.
//     /// </summary>
//     public PublishDiagnosticsClientCapabilities? publishDiagnostics { get; set; }

//     /// <summary>
//     /// Capabilities specific to the various call hierarchy requests.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public CallHierarchyClientCapabilities? callHierarchy { get; set; }

//     /// <summary>
//     /// Capabilities specific to the various semantic token request.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public SemanticTokensClientCapabilities? semanticTokens { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/linkedEditingRange` request.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public LinkedEditingRangeClientCapabilities? linkedEditingRange { get; set; }

//     /// <summary>
//     /// Client capabilities specific to the `textDocument/moniker` request.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public MonikerClientCapabilities? moniker { get; set; }

//     /// <summary>
//     /// Capabilities specific to the various type hierarchy requests.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public TypeHierarchyClientCapabilities? typeHierarchy { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/inlineValue` request.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public InlineValueClientCapabilities? inlineValue { get; set; }

//     /// <summary>
//     /// Capabilities specific to the `textDocument/inlayHint` request.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public InlayHintClientCapabilities? inlayHint { get; set; }

//     /// <summary>
//     /// Capabilities specific to the diagnostic pull model.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public DiagnosticClientCapabilities? diagnostic { get; set; }
// }

// /// <summary>
// /// Capabilities specific to the notebook document support.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class NotebookDocumentClientCapabilities{

//     /// <summary>
//     /// Capabilities specific to notebook document synchronization
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public NotebookDocumentSyncClientCapabilities synchronization { get; set; }
// }


// public class WindowClientCapabilities{

//     /// <summary>
//     /// It indicates whether the client supports server initiated
//     /// progress using the `window/workDoneProgress/create` request.
//     /// 
//     /// The capability also controls Whether client supports handling
//     /// of progress notifications. If set servers are allowed to report a
//     /// `workDoneProgress` property in the request specific server
//     /// capabilities.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public bool? workDoneProgress { get; set; }

//     /// <summary>
//     /// Capabilities specific to the showMessage request.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public ShowMessageRequestClientCapabilities? showMessage { get; set; }

//     /// <summary>
//     /// Capabilities specific to the showDocument request.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public ShowDocumentClientCapabilities? showDocument { get; set; }
// }

// /// <summary>
// /// General client capabilities.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class GeneralClientCapabilities{

//     /// <summary>
//     /// Client capability that signals how the client
//     /// handles stale requests (e.g. a request
//     /// for which the client will not process the response
//     /// anymore since the information is outdated).
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public MyNode? staleRequestSupport { get; set; }

//     /// <summary>
//     /// Client capabilities specific to regular expressions.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public RegularExpressionsClientCapabilities? regularExpressions { get; set; }

//     /// <summary>
//     /// Client capabilities specific to the client's markdown parser.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public MarkdownClientCapabilities? markdown { get; set; }

//     /// <summary>
//     /// The position encodings supported by the client. Client and server
//     /// have to agree on the same position encoding to ensure that offsets
//     /// (e.g. character position in a line) are interpreted the same on both
//     /// sides.
//     /// 
//     /// To keep the protocol backwards compatible the following applies: if
//     /// the value 'utf-16' is missing from the array of position encodings
//     /// servers can assume that the client supports UTF-16. UTF-16 is
//     /// therefore a mandatory encoding.
//     /// 
//     /// If omitted it defaults to ['utf-16'].
//     /// 
//     /// Implementation considerations: since the conversion from one encoding
//     /// into another requires the content of the file / line the conversion
//     /// is best done where the file is read which is usually on the server
//     /// side.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public List<PositionEncodingKind>? positionEncodings { get; set; }
// }

// /// <summary>
// /// A relative pattern is a helper to construct glob patterns that are matched
// /// relatively to a base URI. The common value for a `baseUri` is a workspace
// /// folder root, but it can be another absolute URI as well.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class RelativePattern{

//     /// <summary>
//     /// A workspace folder or a base URI to which this pattern will be matched
//     /// against relatively.
//     /// </summary>
//     public MyNode baseUri { get; set; }

//     /// <summary>
//     /// The actual glob pattern;
//     /// </summary>
//     public Pattern pattern { get; set; }
// }


// public class WorkspaceEditClientCapabilities{

//     /// <summary>
//     /// The client supports versioned document changes in `WorkspaceEdit`s
//     /// </summary>
//     public bool? documentChanges { get; set; }

//     /// <summary>
//     /// The resource operations the client supports. Clients should at least
//     /// support 'create', 'rename' and 'delete' files and folders.
//     /// 
//     /// @since 3.13.0
//     /// </summary>
//     public List<ResourceOperationKind>? resourceOperations { get; set; }

//     /// <summary>
//     /// The failure handling strategy of a client if applying the workspace edit
//     /// fails.
//     /// 
//     /// @since 3.13.0
//     /// </summary>
//     public string? failureHandling { get; set; }

//     /// <summary>
//     /// Whether the client normalizes line endings to the client specific
//     /// setting.
//     /// If set to `true` the client will normalize line ending characters
//     /// in a workspace edit to the client-specified new line
//     /// character.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public bool? normalizesLineEndings { get; set; }

//     /// <summary>
//     /// Whether the client in general supports change annotations on text edits,
//     /// create file, rename file and delete file changes.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public MyNode? changeAnnotationSupport { get; set; }
// }


// public class DidChangeConfigurationClientCapabilities{

//     /// <summary>
//     /// Did change configuration notification supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }
// }


// public class DidChangeWatchedFilesClientCapabilities{

//     /// <summary>
//     /// Did change watched files notification supports dynamic registration. Please note
//     /// that the current protocol doesn't support static configuration for file changes
//     /// from the server side.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// Whether the client has support for {@link  RelativePattern relative pattern}
//     /// or not.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public bool? relativePatternSupport { get; set; }
// }

// /// <summary>
// /// Client capabilities for a {@link WorkspaceSymbolRequest}.
// /// </summary>
// public class WorkspaceSymbolClientCapabilities{

//     /// <summary>
//     /// Symbol request supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// Specific capabilities for the `SymbolKind` in the `workspace/symbol` request.
//     /// </summary>
//     public MyNode? symbolKind { get; set; }

//     /// <summary>
//     /// The client supports tags on `SymbolInformation`.
//     /// Clients supporting tags have to handle unknown tags gracefully.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public MyNode? tagSupport { get; set; }

//     /// <summary>
//     /// The client support partial workspace symbols. The client will send the
//     /// request `workspaceSymbol/resolve` to the server to resolve additional
//     /// properties.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public MyNode? resolveSupport { get; set; }
// }

// /// <summary>
// /// The client capabilities of a {@link ExecuteCommandRequest}.
// /// </summary>
// public class ExecuteCommandClientCapabilities{

//     /// <summary>
//     /// Execute command supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }
// }

// /// <summary>
// /// @since 3.16.0
// /// </summary>
// public class SemanticTokensWorkspaceClientCapabilities{

//     /// <summary>
//     /// Whether the client implementation supports a refresh request sent from
//     /// the server to the client.
//     /// 
//     /// Note that this event is global and will force the client to refresh all
//     /// semantic tokens currently shown. It should be used with absolute care
//     /// and is useful for situation where a server for example detects a project
//     /// wide change that requires such a calculation.
//     /// </summary>
//     public bool? refreshSupport { get; set; }
// }

// /// <summary>
// /// @since 3.16.0
// /// </summary>
// public class CodeLensWorkspaceClientCapabilities{

//     /// <summary>
//     /// Whether the client implementation supports a refresh request sent from the
//     /// server to the client.
//     /// 
//     /// Note that this event is global and will force the client to refresh all
//     /// code lenses currently shown. It should be used with absolute care and is
//     /// useful for situation where a server for example detect a project wide
//     /// change that requires such a calculation.
//     /// </summary>
//     public bool? refreshSupport { get; set; }
// }

// /// <summary>
// /// Capabilities relating to events from file operations by the user in the client.
// /// 
// /// These events do not come from the file system, they come from user operations
// /// like renaming a file in the UI.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class FileOperationClientCapabilities{

//     /// <summary>
//     /// Whether the client supports dynamic registration for file requests/notifications.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// The client has support for sending didCreateFiles notifications.
//     /// </summary>
//     public bool? didCreate { get; set; }

//     /// <summary>
//     /// The client has support for sending willCreateFiles requests.
//     /// </summary>
//     public bool? willCreate { get; set; }

//     /// <summary>
//     /// The client has support for sending didRenameFiles notifications.
//     /// </summary>
//     public bool? didRename { get; set; }

//     /// <summary>
//     /// The client has support for sending willRenameFiles requests.
//     /// </summary>
//     public bool? willRename { get; set; }

//     /// <summary>
//     /// The client has support for sending didDeleteFiles notifications.
//     /// </summary>
//     public bool? didDelete { get; set; }

//     /// <summary>
//     /// The client has support for sending willDeleteFiles requests.
//     /// </summary>
//     public bool? willDelete { get; set; }
// }

// /// <summary>
// /// Client workspace capabilities specific to inline values.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class InlineValueWorkspaceClientCapabilities{

//     /// <summary>
//     /// Whether the client implementation supports a refresh request sent from the
//     /// server to the client.
//     /// 
//     /// Note that this event is global and will force the client to refresh all
//     /// inline values currently shown. It should be used with absolute care and is
//     /// useful for situation where a server for example detects a project wide
//     /// change that requires such a calculation.
//     /// </summary>
//     public bool? refreshSupport { get; set; }
// }

// /// <summary>
// /// Client workspace capabilities specific to inlay hints.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class InlayHintWorkspaceClientCapabilities{

//     /// <summary>
//     /// Whether the client implementation supports a refresh request sent from
//     /// the server to the client.
//     /// 
//     /// Note that this event is global and will force the client to refresh all
//     /// inlay hints currently shown. It should be used with absolute care and
//     /// is useful for situation where a server for example detects a project wide
//     /// change that requires such a calculation.
//     /// </summary>
//     public bool? refreshSupport { get; set; }
// }

// /// <summary>
// /// Workspace client capabilities specific to diagnostic pull requests.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class DiagnosticWorkspaceClientCapabilities{

//     /// <summary>
//     /// Whether the client implementation supports a refresh request sent from
//     /// the server to the client.
//     /// 
//     /// Note that this event is global and will force the client to refresh all
//     /// pulled diagnostics currently shown. It should be used with absolute care and
//     /// is useful for situation where a server for example detects a project wide
//     /// change that requires such a calculation.
//     /// </summary>
//     public bool? refreshSupport { get; set; }
// }


// public class TextDocumentSyncClientCapabilities{

//     /// <summary>
//     /// Whether text document synchronization supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// The client supports sending will save notifications.
//     /// </summary>
//     public bool? willSave { get; set; }

//     /// <summary>
//     /// The client supports sending a will save request and
//     /// waits for a response providing text edits which will
//     /// be applied to the document before it is saved.
//     /// </summary>
//     public bool? willSaveWaitUntil { get; set; }

//     /// <summary>
//     /// The client supports did save notifications.
//     /// </summary>
//     public bool? didSave { get; set; }
// }

// /// <summary>
// /// Completion client capabilities
// /// </summary>
// public class CompletionClientCapabilities{

//     /// <summary>
//     /// Whether completion supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// The client supports the following `CompletionItem` specific
//     /// capabilities.
//     /// </summary>
//     public MyNode? completionItem { get; set; }

    
//     public MyNode? completionItemKind { get; set; }

//     /// <summary>
//     /// Defines how the client handles whitespace and indentation
//     /// when accepting a completion item that uses multi line
//     /// text in either `insertText` or `textEdit`.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public uint? insertTextMode { get; set; }

//     /// <summary>
//     /// The client supports to send additional context information for a
//     /// `textDocument/completion` request.
//     /// </summary>
//     public bool? contextSupport { get; set; }

//     /// <summary>
//     /// The client supports the following `CompletionList` specific
//     /// capabilities.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public MyNode? completionList { get; set; }
// }


// public class HoverClientCapabilities{

//     /// <summary>
//     /// Whether hover supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// Client supports the following content formats for the content
//     /// property. The order describes the preferred format of the client.
//     /// </summary>
//     public List<MarkupKind>? contentFormat { get; set; }
// }

// /// <summary>
// /// Client Capabilities for a {@link SignatureHelpRequest}.
// /// </summary>
// public class SignatureHelpClientCapabilities{

//     /// <summary>
//     /// Whether signature help supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// The client supports the following `SignatureInformation`
//     /// specific properties.
//     /// </summary>
//     public MyNode? signatureInformation { get; set; }

//     /// <summary>
//     /// The client supports to send additional context information for a
//     /// `textDocument/signatureHelp` request. A client that opts into
//     /// contextSupport will also support the `retriggerCharacters` on
//     /// `SignatureHelpOptions`.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public bool? contextSupport { get; set; }
// }

// /// <summary>
// /// @since 3.14.0
// /// </summary>
// public class DeclarationClientCapabilities{

//     /// <summary>
//     /// Whether declaration supports dynamic registration. If this is set to `true`
//     /// the client supports the new `DeclarationRegistrationOptions` return value
//     /// for the corresponding server capability as well.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// The client supports additional metadata in the form of declaration links.
//     /// </summary>
//     public bool? linkSupport { get; set; }
// }

// /// <summary>
// /// Client Capabilities for a {@link DefinitionRequest}.
// /// </summary>
// public class DefinitionClientCapabilities{

//     /// <summary>
//     /// Whether definition supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// The client supports additional metadata in the form of definition links.
//     /// 
//     /// @since 3.14.0
//     /// </summary>
//     public bool? linkSupport { get; set; }
// }

// /// <summary>
// /// Since 3.6.0
// /// </summary>
// public class TypeDefinitionClientCapabilities{

//     /// <summary>
//     /// Whether implementation supports dynamic registration. If this is set to `true`
//     /// the client supports the new `TypeDefinitionRegistrationOptions` return value
//     /// for the corresponding server capability as well.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// The client supports additional metadata in the form of definition links.
//     /// 
//     /// Since 3.14.0
//     /// </summary>
//     public bool? linkSupport { get; set; }
// }

// /// <summary>
// /// @since 3.6.0
// /// </summary>
// public class ImplementationClientCapabilities{

//     /// <summary>
//     /// Whether implementation supports dynamic registration. If this is set to `true`
//     /// the client supports the new `ImplementationRegistrationOptions` return value
//     /// for the corresponding server capability as well.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// The client supports additional metadata in the form of definition links.
//     /// 
//     /// @since 3.14.0
//     /// </summary>
//     public bool? linkSupport { get; set; }
// }

// /// <summary>
// /// Client Capabilities for a {@link ReferencesRequest}.
// /// </summary>
// public class ReferenceClientCapabilities{

//     /// <summary>
//     /// Whether references supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }
// }

// /// <summary>
// /// Client Capabilities for a {@link DocumentHighlightRequest}.
// /// </summary>
// public class DocumentHighlightClientCapabilities{

//     /// <summary>
//     /// Whether document highlight supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }
// }

// /// <summary>
// /// Client Capabilities for a {@link DocumentSymbolRequest}.
// /// </summary>
// public class DocumentSymbolClientCapabilities{

//     /// <summary>
//     /// Whether document symbol supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// Specific capabilities for the `SymbolKind` in the
//     /// `textDocument/documentSymbol` request.
//     /// </summary>
//     public MyNode? symbolKind { get; set; }

//     /// <summary>
//     /// The client supports hierarchical document symbols.
//     /// </summary>
//     public bool? hierarchicalDocumentSymbolSupport { get; set; }

//     /// <summary>
//     /// The client supports tags on `SymbolInformation`. Tags are supported on
//     /// `DocumentSymbol` if `hierarchicalDocumentSymbolSupport` is set to true.
//     /// Clients supporting tags have to handle unknown tags gracefully.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public MyNode? tagSupport { get; set; }

//     /// <summary>
//     /// The client supports an additional label presented in the UI when
//     /// registering a document symbol provider.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public bool? labelSupport { get; set; }
// }

// /// <summary>
// /// The Client Capabilities of a {@link CodeActionRequest}.
// /// </summary>
// public class CodeActionClientCapabilities{

//     /// <summary>
//     /// Whether code action supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// The client support code action literals of type `CodeAction` as a valid
//     /// response of the `textDocument/codeAction` request. If the property is not
//     /// set the request can only return `Command` literals.
//     /// 
//     /// @since 3.8.0
//     /// </summary>
//     public MyNode? codeActionLiteralSupport { get; set; }

//     /// <summary>
//     /// Whether code action supports the `isPreferred` property.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public bool? isPreferredSupport { get; set; }

//     /// <summary>
//     /// Whether code action supports the `disabled` property.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public bool? disabledSupport { get; set; }

//     /// <summary>
//     /// Whether code action supports the `data` property which is
//     /// preserved between a `textDocument/codeAction` and a
//     /// `codeAction/resolve` request.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public bool? dataSupport { get; set; }

//     /// <summary>
//     /// Whether the client supports resolving additional code action
//     /// properties via a separate `codeAction/resolve` request.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public MyNode? resolveSupport { get; set; }

//     /// <summary>
//     /// Whether the client honors the change annotations in
//     /// text edits and resource operations returned via the
//     /// `CodeAction#edit` property by for example presenting
//     /// the workspace edit in the user interface and asking
//     /// for confirmation.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public bool? honorsChangeAnnotations { get; set; }
// }

// /// <summary>
// /// The client capabilities  of a {@link CodeLensRequest}.
// /// </summary>
// public class CodeLensClientCapabilities{

//     /// <summary>
//     /// Whether code lens supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }
// }

// /// <summary>
// /// The client capabilities of a {@link DocumentLinkRequest}.
// /// </summary>
// public class DocumentLinkClientCapabilities{

//     /// <summary>
//     /// Whether document link supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// Whether the client supports the `tooltip` property on `DocumentLink`.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public bool? tooltipSupport { get; set; }
// }


// public class DocumentColorClientCapabilities{

//     /// <summary>
//     /// Whether implementation supports dynamic registration. If this is set to `true`
//     /// the client supports the new `DocumentColorRegistrationOptions` return value
//     /// for the corresponding server capability as well.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }
// }

// /// <summary>
// /// Client capabilities of a {@link DocumentFormattingRequest}.
// /// </summary>
// public class DocumentFormattingClientCapabilities{

//     /// <summary>
//     /// Whether formatting supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }
// }

// /// <summary>
// /// Client capabilities of a {@link DocumentRangeFormattingRequest}.
// /// </summary>
// public class DocumentRangeFormattingClientCapabilities{

//     /// <summary>
//     /// Whether range formatting supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }
// }

// /// <summary>
// /// Client capabilities of a {@link DocumentOnTypeFormattingRequest}.
// /// </summary>
// public class DocumentOnTypeFormattingClientCapabilities{

//     /// <summary>
//     /// Whether on type formatting supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }
// }


// public class RenameClientCapabilities{

//     /// <summary>
//     /// Whether rename supports dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// Client supports testing for validity of rename operations
//     /// before execution.
//     /// 
//     /// @since 3.12.0
//     /// </summary>
//     public bool? prepareSupport { get; set; }

//     /// <summary>
//     /// Client supports the default behavior result.
//     /// 
//     /// The value indicates the default behavior used by the
//     /// client.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public uint? prepareSupportDefaultBehavior { get; set; }

//     /// <summary>
//     /// Whether the client honors the change annotations in
//     /// text edits and resource operations returned via the
//     /// rename request's workspace edit by for example presenting
//     /// the workspace edit in the user interface and asking
//     /// for confirmation.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public bool? honorsChangeAnnotations { get; set; }
// }


// public class FoldingRangeClientCapabilities{

//     /// <summary>
//     /// Whether implementation supports dynamic registration for folding range
//     /// providers. If this is set to `true` the client supports the new
//     /// `FoldingRangeRegistrationOptions` return value for the corresponding
//     /// server capability as well.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// The maximum number of folding ranges that the client prefers to receive
//     /// per document. The value serves as a hint, servers are free to follow the
//     /// limit.
//     /// </summary>
//     public uint? rangeLimit { get; set; }

//     /// <summary>
//     /// If set, the client signals that it only supports folding complete lines.
//     /// If set, client will ignore specified `startCharacter` and `endCharacter`
//     /// properties in a FoldingRange.
//     /// </summary>
//     public bool? lineFoldingOnly { get; set; }

//     /// <summary>
//     /// Specific options for the folding range kind.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public MyNode? foldingRangeKind { get; set; }

//     /// <summary>
//     /// Specific options for the folding range.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public MyNode? foldingRange { get; set; }
// }


// public class SelectionRangeClientCapabilities{

//     /// <summary>
//     /// Whether implementation supports dynamic registration for selection range providers. If this is set to `true`
//     /// the client supports the new `SelectionRangeRegistrationOptions` return value for the corresponding server
//     /// capability as well.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }
// }

// /// <summary>
// /// The publish diagnostic client capabilities.
// /// </summary>
// public class PublishDiagnosticsClientCapabilities{

//     /// <summary>
//     /// Whether the clients accepts diagnostics with related information.
//     /// </summary>
//     public bool? relatedInformation { get; set; }

//     /// <summary>
//     /// Client supports the tag property to provide meta data about a diagnostic.
//     /// Clients supporting tags have to handle unknown tags gracefully.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public MyNode? tagSupport { get; set; }

//     /// <summary>
//     /// Whether the client interprets the version property of the
//     /// `textDocument/publishDiagnostics` notification's parameter.
//     /// 
//     /// @since 3.15.0
//     /// </summary>
//     public bool? versionSupport { get; set; }

//     /// <summary>
//     /// Client supports a codeDescription property
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public bool? codeDescriptionSupport { get; set; }

//     /// <summary>
//     /// Whether code action supports the `data` property which is
//     /// preserved between a `textDocument/publishDiagnostics` and
//     /// `textDocument/codeAction` request.
//     /// 
//     /// @since 3.16.0
//     /// </summary>
//     public bool? dataSupport { get; set; }
// }

// /// <summary>
// /// @since 3.16.0
// /// </summary>
// public class CallHierarchyClientCapabilities{

//     /// <summary>
//     /// Whether implementation supports dynamic registration. If this is set to `true`
//     /// the client supports the new `(TextDocumentRegistrationOptions & StaticRegistrationOptions)`
//     /// return value for the corresponding server capability as well.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }
// }

// /// <summary>
// /// @since 3.16.0
// /// </summary>
// public class SemanticTokensClientCapabilities{

//     /// <summary>
//     /// Whether implementation supports dynamic registration. If this is set to `true`
//     /// the client supports the new `(TextDocumentRegistrationOptions & StaticRegistrationOptions)`
//     /// return value for the corresponding server capability as well.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// Which requests the client supports and might send to the server
//     /// depending on the server's capability. Please note that clients might not
//     /// show semantic tokens or degrade some of the user experience if a range
//     /// or full request is advertised by the client but not provided by the
//     /// server. If for example the client capability `requests.full` and
//     /// `request.range` are both set to true but the server only provides a
//     /// range provider the client might not render a minimap correctly or might
//     /// even decide to not show any semantic tokens at all.
//     /// </summary>
//     public MyNode requests { get; set; }

//     /// <summary>
//     /// The token types that the client supports.
//     /// </summary>
//     public List<string> tokenTypes { get; set; }

//     /// <summary>
//     /// The token modifiers that the client supports.
//     /// </summary>
//     public List<string> tokenModifiers { get; set; }

//     /// <summary>
//     /// The token formats the clients supports.
//     /// </summary>
//     public List<TokenFormat> formats { get; set; }

//     /// <summary>
//     /// Whether the client supports tokens that can overlap each other.
//     /// </summary>
//     public bool? overlappingTokenSupport { get; set; }

//     /// <summary>
//     /// Whether the client supports tokens that can span multiple lines.
//     /// </summary>
//     public bool? multilineTokenSupport { get; set; }

//     /// <summary>
//     /// Whether the client allows the server to actively cancel a
//     /// semantic token request, e.g. supports returning
//     /// LSPErrorCodes.ServerCancelled. If a server does the client
//     /// needs to retrigger the request.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public bool? serverCancelSupport { get; set; }

//     /// <summary>
//     /// Whether the client uses semantic tokens to augment existing
//     /// syntax tokens. If set to `true` client side created syntax
//     /// tokens and semantic tokens are both used for colorization. If
//     /// set to `false` the client only uses the returned semantic tokens
//     /// for colorization.
//     /// 
//     /// If the value is `undefined` then the client behavior is not
//     /// specified.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public bool? augmentsSyntaxTokens { get; set; }
// }

// /// <summary>
// /// Client capabilities for the linked editing range request.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class LinkedEditingRangeClientCapabilities{

//     /// <summary>
//     /// Whether implementation supports dynamic registration. If this is set to `true`
//     /// the client supports the new `(TextDocumentRegistrationOptions & StaticRegistrationOptions)`
//     /// return value for the corresponding server capability as well.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }
// }

// /// <summary>
// /// Client capabilities specific to the moniker request.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class MonikerClientCapabilities{

//     /// <summary>
//     /// Whether moniker supports dynamic registration. If this is set to `true`
//     /// the client supports the new `MonikerRegistrationOptions` return value
//     /// for the corresponding server capability as well.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }
// }

// /// <summary>
// /// @since 3.17.0
// /// </summary>
// public class TypeHierarchyClientCapabilities{

//     /// <summary>
//     /// Whether implementation supports dynamic registration. If this is set to `true`
//     /// the client supports the new `(TextDocumentRegistrationOptions & StaticRegistrationOptions)`
//     /// return value for the corresponding server capability as well.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }
// }

// /// <summary>
// /// Client capabilities specific to inline values.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class InlineValueClientCapabilities{

//     /// <summary>
//     /// Whether implementation supports dynamic registration for inline value providers.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }
// }

// /// <summary>
// /// Inlay hint client capabilities.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class InlayHintClientCapabilities{

//     /// <summary>
//     /// Whether inlay hints support dynamic registration.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// Indicates which properties a client can resolve lazily on an inlay
//     /// hint.
//     /// </summary>
//     public MyNode? resolveSupport { get; set; }
// }

// /// <summary>
// /// Client capabilities specific to diagnostic pull requests.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class DiagnosticClientCapabilities{

//     /// <summary>
//     /// Whether implementation supports dynamic registration. If this is set to `true`
//     /// the client supports the new `(TextDocumentRegistrationOptions & StaticRegistrationOptions)`
//     /// return value for the corresponding server capability as well.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// Whether the clients supports related documents for document diagnostic pulls.
//     /// </summary>
//     public bool? relatedDocumentSupport { get; set; }
// }

// /// <summary>
// /// Notebook specific client capabilities.
// /// 
// /// @since 3.17.0
// /// </summary>
// public class NotebookDocumentSyncClientCapabilities{

//     /// <summary>
//     /// Whether implementation supports dynamic registration. If this is
//     /// set to `true` the client supports the new
//     /// `(TextDocumentRegistrationOptions & StaticRegistrationOptions)`
//     /// return value for the corresponding server capability as well.
//     /// </summary>
//     public bool? dynamicRegistration { get; set; }

//     /// <summary>
//     /// The client supports sending execution summary data per cell.
//     /// </summary>
//     public bool? executionSummarySupport { get; set; }
// }

// /// <summary>
// /// Show message request client capabilities
// /// </summary>
// public class ShowMessageRequestClientCapabilities{

//     /// <summary>
//     /// Capabilities specific to the `MessageActionItem` type.
//     /// </summary>
//     public MyNode? messageActionItem { get; set; }
// }

// /// <summary>
// /// Client capabilities for the showDocument request.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class ShowDocumentClientCapabilities{

//     /// <summary>
//     /// The client has support for the showDocument
//     /// request.
//     /// </summary>
//     public bool support { get; set; }
// }

// /// <summary>
// /// Client capabilities specific to regular expressions.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class RegularExpressionsClientCapabilities{

//     /// <summary>
//     /// The engine's name.
//     /// </summary>
//     public string engine { get; set; }

//     /// <summary>
//     /// The engine's version.
//     /// </summary>
//     public string? version { get; set; }
// }

// /// <summary>
// /// Client capabilities specific to the used markdown parser.
// /// 
// /// @since 3.16.0
// /// </summary>
// public class MarkdownClientCapabilities{

//     /// <summary>
//     /// The name of the parser.
//     /// </summary>
//     public string parser { get; set; }

//     /// <summary>
//     /// The version of the parser.
//     /// </summary>
//     public string? version { get; set; }

//     /// <summary>
//     /// A list of HTML tags that the client allows / supports in
//     /// Markdown.
//     /// 
//     /// @since 3.17.0
//     /// </summary>
//     public List<string>? allowedTags { get; set; }
// }

