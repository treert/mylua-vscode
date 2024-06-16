using MyServer.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Model;
public class MyLuaFile
{
    /// <summary>
    /// 数据结构以后再重新设计。这次关注功能
    /// </summary>
    List<char[]> m_contents = [];

    public void Init(string full_content)
    {

    }

    public void Edit(TextDocContentChangeEvent textDocContentChangeEvent) {
    
    }

    public void GetSymbolTree()
    {

    }

    public void GetSyntaxTokens(Protocol.Range? range)
    {

    }

    public void GetFolderTree()
    {

    }
}
