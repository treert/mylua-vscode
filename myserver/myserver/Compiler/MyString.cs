using MyServer.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Compiler;
/// <summary>
/// 还是没想好。以后再说吧。
/// </summary>
public class MyString
{
    private string[] contents;
}

public class MyStringRange
{
    public Position start { get; set; }
    public Position end { get; set; }
    private MyString _inner_str;
}
