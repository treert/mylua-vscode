import * as vscode from 'vscode';
import { StatusBarItem } from 'vscode';

export class StatusBarManager {

    private static MemStateBar:StatusBarItem;
    private static MainBar:StatusBarItem;

    public static init() {
        this.MemStateBar = vscode.window.createStatusBarItem(vscode.StatusBarAlignment.Left, 5.0);
        this.MemStateBar.tooltip = "Click to collect garbage";
        this.MemStateBar.command = 'mylua.LuaGarbageCollect';


        let bar = vscode.window.createStatusBarItem(vscode.StatusBarAlignment.Left,7);
        bar.tooltip = 'mylua tip';
        bar.text = "💚mylua";
        bar.command = 'mylua.status.bar.click';
        this.MainBar = bar;
    }

    //刷新内存数据显示区的值
    public static refreshLuaMemNum(num: Number) {
        this.MemStateBar.text = String(num) + "KB";
        this.MemStateBar.show();
    }
    
    public static ShowMain(message: string) {
        this.MainBar.text = message;
        this.MainBar.show();
    }

    //按钮恢复到初始状态
    public static reset() {
        this.MemStateBar.hide();
    }
}
