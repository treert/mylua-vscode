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
/// 类似 List<char> 方便后期支持一些定制的需求
/// </summary>
public class MyString
{
    private char[] _items = s_emptyArray;
    private int _size;
    private static readonly char[] s_emptyArray = new char[0];
    public const int MaxSupportCount = 0x7FFFFFC7;//  2,147,483,591 from dotnet

    public int Count
    {
        get { return _size; }
        set
        {
            EnsureCapacity(value);
            _size = value;
        }
    }

    public int Capacity
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

    public void Insert(int index, T item)
    {
        if ((uint)index > (uint)_size)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "index need in [0,Count]");
        }
        if (_size == _items.Length)
        {
            Grow(_size + 1);
        }
        if (index < _size)
        {
            Array.Copy(_items, index, _items, index + 1, _size - index);
        }
        _items[index] = item;
        _size++;
    }

    public void InsertRange(int index, ICollection<T> collection)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }
        if ((uint)index > (uint)_size)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "index need in [0,Count]");
        }
        if (collection is ICollection<T> collection2)
        {
            int count = collection2.Count;
            if (count > 0)
            {
                if (_items.Length - _size < count)
                {
                    Grow(_size + count);
                }
                if (index < _size)
                {
                    Array.Copy(_items, index, _items, index + count, _size - index);
                }
                if (this == collection2)
                {
                    Array.Copy(_items, 0, _items, index, index);
                    Array.Copy(_items, index + count, _items, index * 2, _size - index);
                }
                else
                {
                    collection2.CopyTo(_items, index);
                }
                _size += count;
            }
        }
        else
        {
            using IEnumerator<T> enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Insert(index++, enumerator.Current);
            }
        }
    }

    /*
     * 确保容量够，会使用倍增方案增加数组容量
     */
    public int EnsureCapacity(int capacity)
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

