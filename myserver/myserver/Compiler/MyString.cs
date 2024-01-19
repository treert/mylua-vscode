using MyServer.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Compiler;
/// <summary>
/// 方便实现类似 golang 的 slice str
/// </summary>
public readonly struct MyString
{
    private readonly string _content;
    private readonly int _start;
    private readonly int _length;

    public MyString(string content, int start, int length)
    {
        _content = content;
        _start = start;
        _length = length;
    }

    public MyString this[int offset,int length]
    {
        get
        {
            if(offset + length > _length)
            {
                throw new ArgumentOutOfRangeException();
            }
            return new MyString(_content, _start + offset, length);
        }
    }

    public char this[int offset]
    {
        get
        {
            if (offset >= _length)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _content[_start + offset];
        }
    }
}

public class MyStringRange
{
    public Position start { get; set; }
    public Position end { get; set; }
    private MyString _inner_str;
}
