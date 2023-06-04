import * as path from 'path';
import * as os from 'os';
import * as fs from 'fs';
import * as net from 'net';
import * as vscode from 'vscode';
import {
    workspace as Workspace,
    ExtensionContext,
    commands as Commands,
    TextDocument,
    Uri,
    window,
    Disposable,
} from 'vscode';
import {
    LanguageClient,
    LanguageClientOptions,
    ServerOptions,
    DocumentSelector,
    ClientCapabilities,
    TextDocumentClientCapabilities,
    StreamInfo,
} from 'vscode-languageclient/node';

// 控制配置。todo 具体怎么用还要看
function registerCustomCommands(context: ExtensionContext) {
    context.subscriptions.push(Commands.registerCommand('mylua.config', (changes) => {
        let propMap: Map<string, Map<string, any>> = new Map();
        for (const data of changes) {
            let config = Workspace.getConfiguration(undefined, Uri.parse(data.uri));
            if (data.action === 'add') {
                let value: any[] = config.get(data.key) || [];
                value.push(data.value);
                config.update(data.key, value, data.global);
                continue;
            }
            if (data.action === 'set') {
                config.update(data.key, data.value, data.global);
                continue;
            }
            if (data.action === 'prop') {
                if (!propMap.has(data.key)) {
                    propMap[data.key] = config.get(data.key);
                }
                propMap[data.key][data.prop] = data.value;
                config.update(data.key, propMap[data.key], data.global);
                continue;
            }
        }
    }));
}


class MyLuaClient {
    public client!: LanguageClient;
    private disposables = new Array<Disposable>();

    constructor(private context: ExtensionContext,
        private documentSelector: DocumentSelector) {
    }

    async start() {
        // Options to control the language client
        let clientOptions: LanguageClientOptions = {
            // Register the server for plain text documents
            documentSelector: this.documentSelector,
            progressOnInitialization: true,
            markdown: {
                isTrusted: true,
            },
            initializationOptions: {
                changeConfiguration: true,
            }
        };

        let config = Workspace.getConfiguration(undefined, vscode.workspace.workspaceFolders?.[0]);
        let commandParam: string[] = config.get("mylua.misc.parameters") as [];
        commandParam = ['-h'];
        let command:string =  "E:\\ExtGit\\open.sources\\vscode-lua\\server\\bin\\lua-language-server.exe";
        let serverOptions: ServerOptions = {
            command: command,
            args: commandParam,
        };

        // learn from https://github.com/microsoft/vscode-languageserver-node/issues/662
        serverOptions = ()=>{
            // Connect to language server via socket
            let socket = net.connect({
                port:40080
            });
            let result: StreamInfo = {
                writer: socket,
                reader: socket
            };
            return Promise.resolve(result);
        };

        this.client = new LanguageClient(
            'mylua',
            'mylua',
            serverOptions,
            clientOptions
        );

        //client.registerProposedFeatures();
        await this.client.start();
        this.onCommand();
        this.statusBar();
    }

    async stop() {
        this.client.stop();
        for (const disposable of this.disposables) {
            disposable.dispose();
        }
    }
    statusBar() {
        let client = this.client;
        let bar = window.createStatusBarItem(vscode.StatusBarAlignment.Left);
        bar.text = 'mylua';
        bar.command = 'mylua.helloWorld';
        this.disposables.push(Commands.registerCommand(bar.command, () => {
            client.sendNotification('$/status/click');
        }));
        this.disposables.push(client.onNotification('$/status/show', (params) => {
            bar.show();
        }));
        this.disposables.push(client.onNotification('$/status/hide', (params) => {
            bar.hide();
        }));
        this.disposables.push(client.onNotification('$/status/report', (params) => {
            bar.text = params.text;
            bar.tooltip = params.tooltip;
        }));
        client.sendNotification('$/status/refresh');
        this.disposables.push(bar);
    }

    onCommand() {
        this.disposables.push(this.client.onNotification('$/command', (params) => {
            Commands.executeCommand(params.command, params.data);
        }));
    }
}

let defaultClient: MyLuaClient;

export function activate(context: ExtensionContext) {
    registerCustomCommands(context);
    function didOpenTextDocument(document: TextDocument) {
        // We are only interested in language mode text
        if (document.languageId !== 'mylua' || (document.uri.scheme !== 'file' && document.uri.scheme !== 'untitled')) {
            return;
        }

        // Untitled files go to a default client.
        if (!defaultClient) {
            defaultClient = new MyLuaClient(context, [
                { language: 'mylua' }
            ]);
            defaultClient.start();
            return;
        }
    }

    Workspace.onDidOpenTextDocument(didOpenTextDocument);
    Workspace.textDocuments.forEach(didOpenTextDocument);
    Workspace.onDidChangeWorkspaceFolders(() => {
        if (defaultClient) {
            defaultClient.stop();
            defaultClient = new MyLuaClient(context, [
                { language: 'mylua' }
            ]);
            defaultClient.start();
        }
    });
}

export async function deactivate() {
    if (defaultClient) {
        defaultClient.stop();
        defaultClient = null as any;
    }
    return undefined;
}
