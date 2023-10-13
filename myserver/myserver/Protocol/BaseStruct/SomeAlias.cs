global using ProgressToken = MyServer.Protocol.MyId;
global using DocumentSelector = MyServer.Protocol.DocumentFilter[];

namespace MyServer.Protocol;

/// <summary>
/// 有不少这种结构懒得一个个处理了。【不知为何，lsp定义的valueSet有事还会为null】【valueSet永远不为null】
/// </summary>
/// <typeparam name="T"></typeparam>
public class ValueSet<T>
{
    public T[] valueSet { get; set; } = new T[0];
}

public class Properties
{
    public string[] properties { get; set; } = new string[0];
}
