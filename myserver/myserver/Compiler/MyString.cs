using MyServer.Protocol;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyServer.Compiler;

/// <summary>
/// 专用于
/// 类似 List<char> 方便后期支持一些定制的需求。
/// </summary>
public class MyString
{
    private char[] _items = s_emptyArray;
    private int _size = 0;
    private static readonly char[] s_emptyArray = new char[0];
    public const int MaxSupportCount = 0x7FFFFFC7;//  2,147,483,591 from dotnet

    public char this[int index]
    {
        get
        {
            return index < _size ? _items[index]: '\0';
        }
    }

    public override string ToString()
    {
        return new string(_items, 0 , _size);
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
        private set
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

    /// <summary>
    /// this[range_start,range_end-1] = str
    /// </summary>
    public void ReplaceRange(int range_start, int range_end, string str)
    {
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
            throw new ArgumentOutOfRangeException(nameof(capacity), "MyList overflow");
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

