using MyServer.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyServer.Compiler;

/// <summary>
/// 方便实现类似 golang 的 slice str。
/// 相当于 ReadOnlyMemory<char>
/// </summary>
public readonly struct MyString : IEquatable<MyString>
{
    public MyString(string content)
    {
        _content = content;
        _start = 0;
        _length = content.Length;
    }

    public MyString(string content, int start, int length)
    {
        _content = content;
        _start = start;
        _length = length;
    }

    public int Length => _length;

    public char this[int index]{
        get
        {
            if (index >= _length)
            {
                throw new ArgumentOutOfRangeException();
}
            return _content[_start + index];
        }
    }

    public MyString Sub(int offset, int length)
    {
        if (offset + length > _length)
        {
            throw new ArgumentOutOfRangeException();
        }
        return new MyString(_content, _start + offset, length);
    }

    public override bool Equals(object? obj)
    {
        if(obj is MyString t)
        {
            return this.Equals(t);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return string.GetHashCode(_content.AsSpan().Slice(_start, _length));
    }

    public override string ToString()
    {
        return _content.Substring(_start, _length);
    }

    public bool Equals(MyString other)
    {
        if (_length != other._length) return false;

        if (_start == other._start && _content == other._content) return true;

        var cmp = String.Compare(_content, _start, other._content, other._start, _length);
        return cmp == 0;
    }

    private readonly string _content;
    private readonly int _start;
    private readonly int _length;
}

