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
 * 
 * 补充说明：
 * 1. export const EOL: string[] = ['\n', '\r\n', '\r'];
 * 2. '\0' 是特殊的截断字符， EOF
 */
public class LuaFile
{
    private LuaFile(string content)
    {
        m_content = content;
        BuildLineOffsets();
    }

    public char this[int idx]
    {
        get
        {
            return idx < m_content.Length ? m_content[idx] : '\0';
        }
    }

    public static LuaFile CreateFromFile(string path)
    {
        var content = File.ReadAllText(path).ReplaceLineEndings();
        var file = new LuaFile(content);
        return file;
    }

    private void BuildLineOffsets()
    {
        m_line_offsets.Clear();
        m_line_offsets.Add(0);

        int cur = 0;
        while(cur < m_content.Length)
        {
            cur = m_content.IndexOfAny(s_eol_chs, cur);
            if (cur == -1)
            {
                break;
            }
            if (m_content[cur] == '\r' && this[cur + 1] == '\n')
            {
                cur = cur + 1;// \r\n
            }
            cur = cur + 1;
            m_line_offsets.Add(cur);
        }
    }

    private static readonly char[] s_eol_chs = new char[] { '\n', '\r'};

    private string m_content = string.Empty;
    private List<int> m_line_offsets = new List<int>();// 每一行开头字符的偏移

}
