// The module 'vscode' contains the VS Code extensibility API
// Import the module and reference it with the alias vscode in your code below
import * as vscode from 'vscode';

import * as myluaserver from './myluaserver';
import * as mydebuger from './debug/mydebuger';
import { DebugLogger } from './common/LogManager';
import { StatusBarManager } from './common/StatusBarManager';
import { workspace } from 'vscode';
import { PathManager } from './common/PathManager';

// This method is called when your extension is activated
// Your extension is activated the very first time the command is executed
export function activate(context: vscode.ExtensionContext) {
    // init log
    DebugLogger.init();
    StatusBarManager.init();
    PathManager.init();
    
    workspace.onDidChangeWorkspaceFolders(_event => {
		// 在工程中增删文件夹的回调
        console.log('Workspace folder change event received.');
        if(_event.added.length > 0){
            PathManager.addOpenedFolder(_event.added);
        }

        if(_event.removed.length > 0){
            PathManager.removeOpenedFolder(_event.removed);
        }
    });

    myluaserver.activate(context);
    mydebuger.activate(context);

	// Use the console to output diagnostic information (console.log) and errors (console.error)
	// This line of code will only be executed once when your extension is activated
	console.log('Congratulations, your extension "mylua" is now active!');

	// The command has been defined in the package.json file
	// Now provide the implementation of the command with registerCommand
	// The commandId parameter must match the command field in package.json
	let disposable = vscode.commands.registerCommand('mylua.helloWorld', () => {
		// The code you place here will be executed every time your command is executed
		// Display a message box to the user
		vscode.window.showInformationMessage('Hello World from mylua!');
	});

	context.subscriptions.push(disposable);
}

// This method is called when your extension is deactivated
export function deactivate() {
    myluaserver.deactivate();
    mydebuger.deactivate();
}
