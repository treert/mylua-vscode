## 概述
零散的整理下 lsp 相关的资料

## 协议数据的基本结构。
```json
Content-Length: ...\r\n
\r\n
{
	"jsonrpc": "2.0",
	"id": 1,
	"method": "textDocument/didOpen",
	"params": {
		...
	}
}
```

## Request & Response Message
```ts
interface Message {
	jsonrpc: string;
}

interface RequestMessage extends Message {
	id: integer | string;
	method: string;
	params?: array | object;
}

interface ResponseMessage extends Message {
	id: integer | string | null;

	/**
	 * The result of a request. This member is REQUIRED on success.
	 * This member MUST NOT exist if there was an error invoking the method.
	 */
	result?: string | number | boolean | object | null;

	/**
	 * The error object in case a request fails.
	 */
	error?: ResponseError;
}
```
## Notification Message
```typescript
interface NotificationMessage extends Message {
	/**
	 * The method to be invoked.
	 */
	method: string;

	/**
	 * The notification's params.
	 */
	params?: array | object;
}
```

## 特殊规则
- `$/`开头的 method ，不要求实现。因为可能支持不了。
  - 例如`$/cancelRequest`，在单线程的lsp上，支持不了。
  - 返回处理：
    - notify 直接忽略
    - request 返回 error code `MethodNotFound = -32601`
- `$/cancelRequest`
  - params: `{id:integer|string}`
- `$/progress`


## Capability
- client
  - workspace.workspaceEdit.changeAnnotationSupport
    - 编辑时代注释。大概是像git记录一样吧。需要客户端支持才行
  - workspace.workspaceEdit.failureHandling
  - workspace.workspaceEdit.resourceOperations
  - workspace.workspaceEdit.documentChanges
    - Whether a client supports versioned document edits is expressed via this capability
  - window.workDoneProgress 是否支持服务器主动推送的进度条
  - textDocument.synchronization.dynamicRegistration
  - textDocument.synchronization.willSave
- 能力选项
  - MarkdownClientCapabilities
  - WorkspaceEditClientCapabilities
- Token
  - workDoneToken The token used to report work done progress.
  - partialResultToken The token used to report partial result progress.
- 结构
  - TextDocumentClientCapabilities 里面定义了一堆字段，一个request一个
  - NotebookDocumentClientCapabilities
  - ClientCapabilities
  - ServerCapabilities


## 零碎
基础结构
- DocumentUri string
- Position 
- Range {start:Position,end:Position}
- Location {uri:DocumentUri, range:Range}
- LocationLink {originSelectionRange:Range, targetUri:DocumentUri, targetRange:Range, targetSelectionRange:Range}
关键的结构
- TextDocumentItem
- TextDocumentEdit
- Diagnostic

## method
- request
  - initialize
  - textDocument/codeAction
  - textDocument/reference
  - workspace/symbol
- Client Request
  - initialize
  - shutdown
  - textDocument/willSaveWaitUntil
  - workspace/willRenameFiles
- Client Notify
  - exit
  - textDocument/didOpen
  - textDocument/didChange
  - textDocument/didClose
  - textDocument/didSave
  - workspace/didRenameFiles
- Server Request
  - client/registerCapability
  - client/unregisterCapability
  - window/workDoneProgress/create
- notify
  - textDocument/publishDiagnostics

- diagnostic 诊断 error warning information hint
- command
- MarkupContent
- File Resource changes
  - CreateFile
  - RenameFile
  - DeleteFile
- WorkspaceEdit
- Work Done Progress
  - notify `$/progress`
  - 特殊结构：WorkDoneProgressOptions
- Partial Result Progress

## Server Side LifeCircle
server端请求
- window/showMessage
- window/logMessage
- telemetry/event
- window/showMessageRequest

- Initialize Request
- Initialized Notification
  - 客户端发服务器，紧跟着 InitRequest
- Register Capability
  - 服务器主动通知客户端，动态注册能力。可以取消已经注册的能力
  - Unregister Capability
- LogTrace Notification
  - $/setTrace 客户端发服务器
  - $/logTrace 服务器发客户端
- ShutDown Request
  - 客户端发关闭请求之后，只能再发 Exit Notfication，通知服务器正式关闭

## BaseProtocl
- Header Part
- Content Part
- Base Types
- Request Message
- Response Message
- Notification Message
- Cancellation Support (:arrow_right: :arrow_left:)
- Progress Support (:arrow_right: :arrow_left:)
## Server lifecycle
- Initialize Request (:leftwards_arrow_with_hook:)
- Initialized Notification (:arrow_right:)
- Register Capability (:arrow_right_hook:)
- Unregister Capability (:arrow_right_hook:)
- SetTrace Notification (:arrow_right:)
- LogTrace Notification (:arrow_left:)
- Shutdown Request (:leftwards_arrow_with_hook:)
- Exit Notification (:arrow_right:)

## Text Document Synchronization
- DidOpenTextDocument Notification (:arrow_right:)
- DidChangeTextDocument Notification (:arrow_right:)
- WillSaveTextDocument Notification (:arrow_right:)
- WillSaveWaitUntilTextDocument Request (:leftwards_arrow_with_hook:)
- DidSaveTextDocument Notification (:arrow_right:)
- DidCloseTextDocument Notification (:arrow_right:)
- Renaming a document (close then open)
- Notebook Document Synchronization
  - DidOpenNotebookDocument Notification (:arrow_right:)
  - DidChangeNotebookDocument Notification (:arrow_right:)
  - DidSaveNotebookDocument Notification (:arrow_right:)
  - DidCloseNotebookDocument Notification (:arrow_right:)



## Language Features
- Goto Declaration Request (:leftwards_arrow_with_hook:)
- Goto Definition Request (:leftwards_arrow_with_hook:)
- Goto Type Definition Request (:leftwards_arrow_with_hook:)
- Goto Implementation Request (:leftwards_arrow_with_hook:)
- Find References Request (:leftwards_arrow_with_hook:)
- Prepare Call Hierarchy Request (:leftwards_arrow_with_hook:)
- Call Hierarchy Incoming Calls (:leftwards_arrow_with_hook:)
- Call Hierarchy Outgoing Calls (:leftwards_arrow_with_hook:)
- Prepare Type Hierarchy Request (:leftwards_arrow_with_hook:)
- Type Hierarchy Supertypes (:leftwards_arrow_with_hook:)
- Type Hierarchy Subtypes (:leftwards_arrow_with_hook:)
- Document Highlights Request (:leftwards_arrow_with_hook:)
- Document Link Request (:leftwards_arrow_with_hook:)
- Document Link Resolve Request (:leftwards_arrow_with_hook:)
- Hover Request (:leftwards_arrow_with_hook:)
- Code Lens Request (:leftwards_arrow_with_hook:)
- Code Lens Resolve Request (:leftwards_arrow_with_hook:)
- Code Lens Refresh Request (:arrow_right_hook:)
- Folding Range Request (:leftwards_arrow_with_hook:)
- Selection Range Request (:leftwards_arrow_with_hook:)
- Document Symbols Request (:leftwards_arrow_with_hook:)
- Semantic Tokens (:leftwards_arrow_with_hook:)
  - 官方文档说明的比较多
- Inlay Hint Request (:leftwards_arrow_with_hook:)
- Inlay Hint Resolve Request (:leftwards_arrow_with_hook:)
- Inlay Hint Refresh Request (:arrow_right_hook:)
- Inline Value Request (:leftwards_arrow_with_hook:)
- Inline Value Refresh Request (:arrow_right_hook:)
- Monikers (:leftwards_arrow_with_hook:)
  - LSIF 引入的
- Completion Request (:leftwards_arrow_with_hook:)
  - 官方文档非常多
  - Snippet Syntax 有些复杂的样子
- Completion Item Resolve Request (:leftwards_arrow_with_hook:)
- PublishDiagnostics Notification (:arrow_left:)
- Pull Diagnostics
- Document Diagnostics(:leftwards_arrow_with_hook:)
- Workspace Diagnostics(:leftwards_arrow_with_hook:)
- Diagnostics Refresh(:arrow_right_hook:)
- Signature Help Request (:leftwards_arrow_with_hook:)
- Code Action Request (:leftwards_arrow_with_hook:)
- Code Action Resolve Request (:leftwards_arrow_with_hook:)
- Document Color Request (:leftwards_arrow_with_hook:)
- Color Presentation Request (:leftwards_arrow_with_hook:)
- Document Formatting Request (:leftwards_arrow_with_hook:)
- Document Range Formatting Request (:leftwards_arrow_with_hook:)
- Document on Type Formatting Request (:leftwards_arrow_with_hook:)
- Rename Request (:leftwards_arrow_with_hook:)
- Prepare Rename Request (:leftwards_arrow_with_hook:)
  - 文档没说清怎么做
- Linked Editing Range(:leftwards_arrow_with_hook:)

## Workspace Feature
- Workspace Symbols Request (:leftwards_arrow_with_hook:)
- Workspace Symbol Resolve Request (:leftwards_arrow_with_hook:)
- Configuration Request (:arrow_right_hook:)
- DidChangeConfiguration Notification (:arrow_right:)
- Workspace folders request (:arrow_right_hook:)
- DidChangeWorkspaceFolders Notification (:arrow_right:)
- WillCreateFiles Request (:leftwards_arrow_with_hook:)
- DidCreateFiles Notification (:arrow_right:)
- WillRenameFiles Request (:leftwards_arrow_with_hook:)
- DidRenameFiles Notification (:arrow_right:)
- WillDeleteFiles Request (:leftwards_arrow_with_hook:)
- DidDeleteFiles Notification (:arrow_right:)
- DidChangeWatchedFiles Notification (:arrow_right:)
- Execute a command (:leftwards_arrow_with_hook:)
- Applies a WorkspaceEdit (:arrow_right_hook:)


## Window Features
- ShowMessage Notification (:arrow_left:)
- ShowMessage Request (:arrow_right_hook:)
- Show Document Request (:arrow_right_hook:)
- LogMessage Notification (:arrow_left:)
- Create Work Done Progress (:arrow_right_hook:)
- Cancel a Work Done Progress (:arrow_right:)
- Telemetry Notification (:arrow_left:)

## Miscellaneous
- Meta Model
  - https://microsoft.github.io/language-server-protocol/specifications/lsp/3.17/metaModel/metaModel.json