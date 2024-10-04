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

/// <summary>
/// 专用于MyServer,支持修改。
/// 类似 List<char> 方便后期支持一些定制的需求。
/// 1. 换行符在最底层全部替换成 \n
/// 2. vscode 是按行列定位字符的。插件内部用offset定位，额外多一个行数索引数组。
/// 3. '\0' 是特殊的截断字符， EOF
/// </summary>
public class MyString
{
    public class Range
    {
        private int m_start;
        private int m_length;
        private MyString m_str;
        public Range(MyString str,int start, int length)
        {
            m_start = start;
            m_length = length;
            m_str = str;
        }
        public int RawStart => m_start;
        public int RawEnd => m_start + m_length;
        public int Length => m_length;
        public char this[int index]
        {
            get { return index < Length ? m_str[index + m_start] : '\0'; }
        }
        public override string ToString()
        {
            return new string(m_str._items, m_start, m_length);
        }

        // public override int GetHashCode()
        // {
        //     ReadOnlySpan<char> chars = new ReadOnlySpan<char>(m_str._items, m_start, m_length);
        //     return string.GetHashCode(chars);
        // }

        public Range SubRange(int start, int length)
        {
            Debug.Assert(start + length <= Length);
            return new Range(m_str, m_start + start, length);
        }

        public MyString RawString => m_str;
    }

    private char[] _items = s_emptyArray;
    private List<int> _line_offsets = [];
    private int _size = 0;
    private static readonly char[] s_emptyArray = new char[0];
    public const int MaxSupportCount = 0x7FFFFFC7;//  2,147,483,591 from dotnet

    public char this[int index]
    {
        get
        {
            return index < Length ? _items[index]: '\0';
        }
    }

    public override string ToString()
    {
        return new string(_items, 0 , _size);
    }

    public Range ToRange()
    {
        return new Range(this, 0, Length);
    }

    public Protocol.Position GetPosByOffset(int offset)
    {
        Debug.Assert(offset >= 0);
        // get aa[idx] <= offset
        int idx = _line_offsets.BinarySearch(offset);
        if (idx < 0)
        {
            idx = -(idx + 1)-1;
            Debug.Assert(idx >= 0 && idx < _line_offsets.Count);
        }
        int line = idx;
        int cloumn = offset - _line_offsets[idx];
        return new Protocol.Position
        {
            line = (uint)line,
            character = (uint)cloumn,
        };
    }

    public int Length
    {
        get { return _size; }
    }

    private int Capacity
    {
        get
        {
            return _items.Length;
        }
        set
        {
            if (value == _items.Length)
            {
                return;
            }
            if (value > 0)
            {
                char[] array = new char[value];
                if (_size > 0)
                {
                    Array.Copy(_items, array, Math.Min(_size, value));// 缩小。直接丢弃数据
                }
                _items = array;
            }
            else
            {
                _items = s_emptyArray;
            }
        }
    }

    public void ResetString(string value)
    {
        ReplaceRange(0, Length, value);
    }

    /// <summary>
    /// this[range_start,range_end-1] = str
    /// </summary>
    public void ReplaceRange(int range_start, int range_end, string str)
    {
        str = str.ReplaceLineEndings("\n");
        int add_len = str.Length - (range_end - range_start);
        if (add_len > 0)
        {
            EnsureCapacity(_size + add_len);
        }

        if(add_len != 0)
        {
            Array.Copy(_items, range_end, _items, range_start + str.Length, _size - range_end);
        }

        str.CopyTo(0, _items, range_start, str.Length);
        _size += add_len;
        ReBuildLineOffsets();
    }

    private void ReBuildLineOffsets()
    {
        _line_offsets.Clear();
        _line_offsets.Add(0);

        int cur = 0;
        while (cur < _items.Length)
        {
            cur = Array.IndexOf(_items, '\n', cur);
            if (cur == -1)
            {
                break;
            }
            cur = cur + 1;
            _line_offsets.Add(cur);
        }
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void Empty()
    {
        _size = 0;
    }

    public int IndexOfAny(char[] anyOf, int startIndex = 0, int count = -1)
    {
        if(count < 0)
        {
            count = _size - startIndex;
        }
        int num = new ReadOnlySpan<char>(_items, startIndex, count).IndexOfAny(anyOf);
        return num >= 0 ? num + startIndex : -1;
    }

    public int IndexOf(char ch, int startIndex = 0, int count = -1)
    {
        if (count < 0)
        {
            count = _size - startIndex;
        }
        int num = Array.IndexOf(_items, ch, startIndex, count);
        return num >= 0 ? num + startIndex : -1;
    }

    /*
     * 确保容量够，会使用倍增方案增加数组容量
     */
    private int EnsureCapacity(int capacity)
    {
        if (_items.Length < capacity)
        {
            Grow(capacity);
        }
        return _items.Length;
    }

    private void Grow(int capacity)
    {
        if ((uint)capacity > (uint)MaxSupportCount)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Too Long String. MyString overflow.");
        }
        int num = ((_items.Length == 0) ? 4 : (2 * _items.Length));
        if ((long)(uint)num > MaxSupportCount)
        {
            num = MaxSupportCount;
        }
        if (num < capacity)
        {
            num = capacity;
        }
        Capacity = num;
    }
}


class MyFile{
    public List<MyLine> m_lines = [];

    public MyFile(string[] lines){
        _Ctor(lines);
    }
    
    public MyFile(string filepath){
        var lines = File.ReadAllLines(filepath);
        _Ctor(lines);
    }

    private void _Ctor(string[] lines){
        m_lines.EnsureCapacity(lines.Length);
        for (int i = 0; i < lines.Length; i++){
            var line = new MyLine(lines[i], i);
            
            m_lines.Add(line);
        }
    }

    // 测试下
    public void ParseTokens(){
        MyLine pre_line = null;
        var lex = new LuaLex();
        foreach (var line in m_lines){
            lex.ParseOneLine(line, pre_line);
        }
    }
}

/*
行信息。
词法解析是以行为单位的。开始解析时，有多种初始状态。
1. 正常模式
2. " or ' 的字符串触发了 \ 结尾换行。
    - 以 $ 开头
3. [=*[ string ]=*] 的后续行。
4. --[=*[ comment ]=*] 的后续行。
*/
public class MyLine{
    public MyLine(string row_line, int row_idx){
        m_chars = row_line.ToArray();
        m_row_idx = row_idx;
        _CalTabSize();
    }

    public TokenStrFlag m_parse_flag = TokenStrFlag.Normal;
    public int m_equal_sep_num = 0;

    public int Length => m_chars.Length;
    public int RowIdx{
        get { return m_row_idx;}
        set { m_row_idx = value;}
    }
    public int TabSize => m_tab_size;
    public List<Token> Tokens => m_tokens;

    // 在行尾之后读取，统一都返回 \n
    public char this[int index]
    {
        get { return index < Length ? m_chars[index] : '\n'; }
    }

    void _CalTabSize(){
        m_tab_size = 0;
        foreach (char c in m_chars){
            if (c == ' ') m_tab_size++;
            else if (c == '\t') m_tab_size += MyConfig.TabSize;
            else break;
        }
    }

    List<Token> m_tokens = new List<Token>();


    readonly char[] m_chars = default;
    int m_tab_size = 0;
    int m_row_idx = 0;
}
