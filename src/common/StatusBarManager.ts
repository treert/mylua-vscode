import * as vscode from 'vscode';
import { StatusBarItem } from 'vscode';

export class StatusBarManager {

    private static MemStateBar:StatusBarItem;
    private static Setting:StatusBarItem;

    public static init() {
        this.MemStateBar = vscode.window.createStatusBarItem(vscode.StatusBarAlignment.Left, 5.0);
        this.MemStateBar.tooltip = "Click to collect garbage";
        this.MemStateBar.command = 'mylua.LuaGarbageCollect';

        this.Setting = vscode.window.createStatusBarItem(vscode.StatusBarAlignment.Left, 6.0);
        this.Setting.text = "mylua-debug";
        this.Setting.tooltip = "Click open setting page";
        this.Setting.command = 'mylua.openSettingsPage';
        // this.Setting.hide();
        this.Setting.show();

        let bar = vscode.window.createStatusBarItem(vscode.StatusBarAlignment.Left,7);
        bar.text = "💚mylua";
        bar.command = 'mylua.helloWorld';
        bar.show();
    }

    //刷新内存数据显示区的值
    public static refreshLuaMemNum(num: Number) {
        this.MemStateBar.text = String(num) + "KB";
        this.MemStateBar.show();
    }

    //刷新内存数据显示区的值
    public static showSetting(message: string) {
        this.Setting.text = message;
        this.Setting.show();
    }

    //按钮恢复到初始状态
    public static reset() {

    }
}
