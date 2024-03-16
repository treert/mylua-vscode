using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Compiler;

/**
 * 以文件为基础单元。
 * 一些特殊说明：
 * 1. vscode 是按行列定位字符的。插件内部用offset定位，额外多一个行数索引数组。
 */
public class LuaFile
{
    private List<char> m_content = new List<char>(1024*64);// 每个文件预留
    private List<int> m_line_offsets = new List<int>(1024);
}
