using MyServer.Misc;
using MyServer.Protocol;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyServer.Compiler;

public class MyFile{
    public List<MyLine> m_lines = [];

    public MyFile(string[] lines){
        _Ctor(lines);
    }

    public MyLine GetLine(int index){
        return (index >= 0 && m_lines.Count > index) ? m_lines[index] : null;
    }
    
    public MyFile(string filepath){
        var lines = File.ReadAllLines(filepath);
        _Ctor(lines);
    }

    private void _Ctor(string[] lines){
        m_lines.EnsureCapacity(lines.Length);
        for (int i = 0; i < lines.Length; i++){
            var line = new MyLine(lines[i], i);
            line.m_file = this;
            m_lines.Add(line);
        }
    }

    // 测试下
    public void ParseTokens(){
        MyLine pre_line = null;
        var lex = new LuaLex();
        foreach (var line in m_lines){
            lex.ParseOneLine(line, pre_line);
            pre_line = line;
        }
    }

    public Token GetFirstToken(){
        foreach (var line in m_lines){
            if (line.Tokens.Count > 0) return line.Tokens[0];
        }
        return Token.None;
    }
}

/*
行信息。
词法解析是以行为单位的。开始解析时，有多种初始状态。
1. 正常模式
2. " or ' 的字符串触发了 \ 结尾换行。
    - 以 $ 开头的 Dollar 模式
    - \z 触发的 Zip 模式
3. [=*[ string ]=*] 的后续行。
4. --[=*[ comment ]=*] 的后续行。
*/
public class MyLine{
    public MyLine(string row_line, int row_idx){
        m_content = row_line;
        m_row_idx = row_idx;
        _CalTabSize();
    }

    public MyFile m_file = default;

    public MyLine PreLine => m_file.GetLine(m_row_idx - 1);
    public MyLine NextLine => m_file.GetLine(m_row_idx + 1);
    public TokenStrFlag m_parse_flag = TokenStrFlag.Normal;
    public int m_equal_sep_num = 0;

    public string Content => m_content;
    public int Length => m_content.Length;
    public int RowIdx{
        get { return m_row_idx;}
        set { m_row_idx = value;}
    }
    public int TabSize => m_tab_size;
    public List<Token> Tokens => m_tokens;

    // 获取 lua token, 会过滤掉 commnet
    public Token GetToken(int idx){
        if(m_tokens.Count > idx){
            return m_tokens[idx];
        }
        return Token.None;
    }

    // 获取最后的token，如果是空的返回 None
    public Token LastToken{
        get{
            if (m_tokens.Count == 0){
                return Token.None;
            }
            return m_tokens.Last();
        }
    }

    public void ClearTokens(){
        m_tokens.Clear();
    }

    public void AddToken(Token tk){
        tk.tok_idx = m_tokens.Count;
        tk.src_line = this;
        m_tokens.Add(tk);
    }

    // 在行尾之后读取，统一都返回 \n
    public char this[int index]
    {
        get { return index < Length ? m_content[index] : '\n'; }
    }

    void _CalTabSize(){
        m_tab_size = 0;
        foreach (char c in m_content){
            if (c == ' ') m_tab_size++;
            else if (c == '\t') m_tab_size += MyConfig.TabSize;
            else break;
        }
    }

        // just for debug
    public override string ToString() {
        return m_content;
    }

    List<Token> m_tokens = new List<Token>();
    
    string m_content = default;
    int m_tab_size = 0;
    int m_row_idx = 0;
}
