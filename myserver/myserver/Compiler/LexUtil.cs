using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
简单梳理：
- set
    - _G.xx = 
    - aa.bb = 
    - aa.xx.bb = 
    - aa[bb] = 
    - aa.xx.[bb] = 
- get
    - xx.bb
    - _G.xx 
- call
    - f(x,y,z): tt
- assign
    - local x,y = 
    - x,y = 
        - global
        - upvalue

特殊考虑 _G _ENV 

- class
    - new field
        - @field xxx ttt
        - set
            - tt.bb = 
    - new method
        - t.f() end
        - t.f = ${}
- enum 常量表。特殊处理


*/

namespace MyServer.Compiler;
/// <summary>
/// 一些静态工具
/// </summary>
public static class LexUtil
{
    public static ReadOnlySpan<byte> CharToHexLookup => new byte[]
    {
        255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255, 0, 1,
        2, 3, 4, 5, 6, 7, 8, 9, 255, 255,
        255, 255, 255, 255, 255, 10, 11, 12, 13, 14,
        15, 255, 255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
        255, 255, 255, 255, 255, 255, 255, 10, 11, 12,
        13, 14, 15
    };
    public static uint HexToInt(char c)
    {
        int v = c;
        if (v < CharToHexLookup.Length)
        {
            return CharToHexLookup[v];
        }
        return 255;
    }
}
