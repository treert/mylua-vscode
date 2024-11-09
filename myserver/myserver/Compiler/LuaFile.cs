using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Compiler;

/**
 * 以文件为基础单元。
 * 一些特殊说明：
 * 1. vscode 是按行列定位字符的。插件内部用offset定位，额外多一个行数索引数组。
 * 
 * 补充说明：
 * 1. export const EOL: string[] = ['\n', '\r\n', '\r'];
 * 2. '\0' 是特殊的截断字符， EOF
 */
public class LuaFile
{
    
}
