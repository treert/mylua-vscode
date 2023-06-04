// 可视化配置部分
import { Tools } from '../common/Tools';
import * as fs from "fs";
import * as vscode from 'vscode';
import { DebugLogger } from '../common/LogManager';

export class VisualSetting {
    private static ADBRevTerminal;

    // 修改launch.json中的一项
    public static setLaunchjson(rootFolder, key, value, tag = ''){
        let settings = this.readLaunchjson(rootFolder);
        for (const keyLaunch in settings.configurations) {
            let valueLaunch = settings.configurations[keyLaunch]
            if(tag === '' ||  valueLaunch["tag"] === tag){
                valueLaunch[key] = value;
            }
        }

        //序列化并写入
        let launchJson = JSON.stringify(settings, null,  4);
        Tools.writeFileContent(rootFolder + "/.vscode/launch.json" ,launchJson);
    }

    // 获取launch.json中的一项
    public static getLaunchjson(rootFolder, key, tag = ''){
        let settings = this.readLaunchjson(rootFolder);
        for (const keyLaunch in settings.configurations) {
            let valueLaunch = settings.configurations[keyLaunch]
            if(tag === '' || valueLaunch["tag"] === tag){
                return valueLaunch[key];
            }
        }
    }

    public static readLaunchjson(rootFolder){
        let launchPath = rootFolder + "/.vscode/launch.json";
        //如果文件不存在，就创建一个
        let launchExist = fs.existsSync(launchPath);
        let jsonStr;
        if(!launchExist){
            let dotVScodeDirExist = fs.existsSync(rootFolder + "/.vscode");
            if(!dotVScodeDirExist){
                //创建.vscode目录
                fs.mkdirSync(rootFolder + "/.vscode");
            }
            // 文件不存在，读取预制文件，创建launch
            let launchTemplate = Tools.readFileContent(Tools.VSCodeExtensionPath + "/res/others/launch.json");
            Tools.writeFileContent(rootFolder + "/.vscode/launch.json" ,launchTemplate);
            jsonStr = launchTemplate;
        }else{
            // 文件存在，读取launch.json的信息
            jsonStr = Tools.readFileContent(launchPath);
        }

        if(jsonStr == null || jsonStr == ''){
            // 没有找到launch.json 文件，生成一份（读取预制内容，拷贝到其中）
            return null;
        }

        //去除注释行
        let reg = /[^:]((\/\/.*)|(\/\*[\s\S]*?\*\/))/g;// 正则表达式
        jsonStr = jsonStr.replace(reg, '');
        let launchSettings = JSON.parse(jsonStr);
        return launchSettings;
    }

    // 读取launch.json中的信息，并序列化
    public static getLaunchData(rootFolderArray:string[]){
        let jsonObj = new Object();

        jsonObj["command"] = "init_setting";
        jsonObj["configs"] = [];
        let index = 0;
        for (const rootFolder of rootFolderArray) {
            jsonObj["configs"][index] = {"path": rootFolder, "launch.json": {}};
            // jsonObj["configs"][index]["path"] = rootFolder;
            // jsonObj["configs"][index]["launch.json"] = new Object(); 
            let settings = this.readLaunchjson(rootFolder);
            for (const key in settings.configurations) {
                const v = settings.configurations[key];
    
                if(v["tag"] === "normal" || v["name"] === "LuaPanda" ){
                    jsonObj["configs"][index]["launch.json"][v["name"]] = v;
                }
                else if(v["tag"] === "attach" || v["name"] === "LuaPanda-Attach"){
                    jsonObj["configs"][index]["launch.json"][v["name"]] = v;
                }
                else if(v["tag"] === "independent_file" || v["name"] === "LuaPanda-IndependentFile"){
                    jsonObj["configs"][index]["launch.json"][v["name"]] = v;
                }
            }
    
            if(Object.keys(jsonObj["configs"][index]["launch.json"]).length === 0 ){
                //读取预制内容，传给页面
                let launchTemplate = Tools.readFileContent(Tools.VSCodeExtensionPath + "/res/others/launch.json");
                let settings = JSON.parse(launchTemplate);
                for (const key in settings.configurations) {
                    const v = settings.configurations[key];
                    if(v["tag"] === "normal" || v["name"] === "LuaPanda"){
                        jsonObj["configs"][index]["launch.json"][v["name"]] = v;
                    }

                    if(v["tag"] === "attach" || v["name"] === "LuaPanda-Attach"){
                        jsonObj["configs"][index]["launch.json"][v["name"]] = v;
                    }
                    
                    if(v["tag"] === "independent_file" || v["name"] === "LuaPanda-IndependentFile"){
                        jsonObj["configs"][index]["launch.json"][v["name"]] = v;
                    }
                }
            }
            index ++;
        }

        //setting反馈到html中
        return JSON.stringify(jsonObj);
    }

    public static getWebMessage(message) {
        let messageObj = JSON.parse(message.webInfo);
        switch (messageObj.command) {
            case 'save_settings':
            	this.processSaveSettings(messageObj);
                break;
            case 'adb_reverse':
                this.processADBReverse(messageObj);
                break;
            case 'clearPreProcessFile':
                //清除文件夹
                let removePath = messageObj.rootFolder + "/.vscode/LuaPanda/";
                let res =Tools.removeDir(removePath);
                if(res){
                    DebugLogger.showTips("文件夹已经清除");
                }else{
                    DebugLogger.showTips("文件不存在", 2);
                }
                break;
        }
    }

    private static processADBReverse(messageObj) {
        let connectionPort = messageObj["connectionPort"];
        if(this.ADBRevTerminal){
            this.ADBRevTerminal.dispose();
        }
        this.ADBRevTerminal = vscode.window.createTerminal({
            name: "ADB Reverse (LuaPanda)",
            env: {}, 
        });

        let cmd = "adb reverse tcp:" + connectionPort + " tcp:" + connectionPort;       
        this.ADBRevTerminal.sendText(cmd , true);
        this.ADBRevTerminal.show(); 
    }

    // 可视化界面保存配置
    private static processSaveSettings(messageObj) {
        try {        
            const element = messageObj.configs;
            let rootFolder = element.path;
            let settings = this.readLaunchjson(rootFolder);
            let newConfig = element["launch.json"];
            // let alreadyWriteIn = false;
            for (const key in settings.configurations) {
                let target_name = settings.configurations[key]["name"];
                if(newConfig[target_name]){
                    settings.configurations[key] = newConfig[target_name];
                }
    
            }
            // if(!alreadyWriteIn){
            //     //launch.json中不存在luapanda项目
            //     settings.configurations.push(newConfig);
            // }

            //序列化并写入
            let launchJson = JSON.stringify(settings, null,  4);
            Tools.writeFileContent(rootFolder + "/.vscode/launch.json" ,launchJson);
      
            DebugLogger.showTips("配置保存成功!");
        } catch (error) {
            DebugLogger.showTips("配置保存失败, 可能是由于 launch.json 文件无法写入. 请手动修改 launch.json 中的配置项来完成配置!", 2);
        }
    }   
}