import * as vscode from 'vscode';

export class DebugLogger {
    private static Ainfo;
    private static Dinfo;

    public static init() {
        DebugLogger.Ainfo = vscode.window.createOutputChannel("mylua Adapter");
        // DebugLogger.Ainfo.show();

        DebugLogger.Dinfo = vscode.window.createOutputChannel("mylua Debugger");
        // DebugLogger.Dinfo.show();
        DebugLogger.Dinfo.appendLine("mylua initializing...");
    }

    public static DebuggerInfo(str: string) {
        if (str != "" && str != null) {
            DebugLogger.Dinfo.appendLine(str);
        }
    }
    public static AdapterInfo(str: string) {
        if (str != "" && str != null) {
            DebugLogger.Ainfo.appendLine(str);
        }
    }

    public static showTips(str:string ,  level?:number){
        if(level === 2 ){
            vscode.window.showErrorMessage(str);
        }else if(level === 1 ){
            vscode.window.showWarningMessage(str);
        }else{
            vscode.window.showInformationMessage(str);
        }
        
    }
}
