using System;
using System.Collections.Generic;

public class MyMultiMap<TKey, TValue>
{
    private readonly Dictionary<TKey, List<TValue>> _map;

    public MyMultiMap()
    {
        _map = new Dictionary<TKey, List<TValue>>();
    }

    public void Add(TKey key, TValue value)
    {
        if (!_map.ContainsKey(key))
        {
            _map[key] = new List<TValue>();
        }
        _map[key].Add(value);
        Console.WriteLine($"Добавлено значение {value} для ключа {key}.");
    }

    public bool Remove(TKey key, TValue value)
    {
        if (_map.ContainsKey(key))
        {
            var values = _map[key];
            if (values.Remove(value))
            {
                Console.WriteLine($"Удалено значение {value} для ключа {key}.");
                if (values.Count == 0) 
                {
                    _map.Remove(key);
                }
                return true;
            }
        }
        Console.WriteLine($"Значение {value} не найдено для ключа {key}.");
        return false;
    }

    public List<TValue> Find(TKey key)
    {
        if (_map.TryGetValue(key, out var values))
        {
            return values;
        }
        Console.WriteLine($"Ключ {key} не найден.");
        return new List<TValue>(); 
    }

    public void ShowAll()
    {
        foreach (var pair in _map)
        {
            Console.WriteLine($"Ключ: {pair.Key}, Значения: [{string.Join(", ", pair.Value)}]");
        }
    }
}

public class MultiMapManager<TKey, TValue>
{
    private readonly MyMultiMap<TKey, TValue> _multiMap;

    public MultiMapManager()
    {
        _multiMap = new MyMultiMap<TKey, TValue>();
    }

    public void AddValue(TKey key, TValue value)
    {
        _multiMap.Add(key, value);
    }

    public void RemoveValue(TKey key, TValue value)
    {
        _multiMap.Remove(key, value);
    }

    public List<TValue> FindValues(TKey key)
    {
        return _multiMap.Find(key);
    }

    public void DisplayAll()
    {
        _multiMap.ShowAll();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        MultiMapManager<int, string> multiMapManager = new MultiMapManager<int, string>();

        multiMapManager.AddValue(1, "Apple");
        multiMapManager.AddValue(1, "Banana");
        multiMapManager.AddValue(2, "Cherry");
        multiMapManager.AddValue(1, "Mango");
        multiMapManager.AddValue(2, "Coconut");

        multiMapManager.DisplayAll();

        var values = multiMapManager.FindValues(1);
        Console.WriteLine($"Значения для ключа 1: {string.Join(", ", values)}");

        multiMapManager.RemoveValue(1, "Banana");

        multiMapManager.DisplayAll();
    }
}