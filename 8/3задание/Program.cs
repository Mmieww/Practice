using System;
using System.Collections.Generic;

public interface IListManager<T>
{
    void Add(T item);                       
    void Remove(T item);                   
    T GetAt(int index);                    
    IEnumerable<T> GetAll();                 
}

public class SimpleListManager<T> : IListManager<T>
{
    private readonly List<T> _items = new List<T>();

    public void Add(T item)
    {
        _items.Add(item);
        Console.WriteLine($"Элемент {item} добавлен в список.");
    }

    public void Remove(T item)
    {
        if (_items.Remove(item))
        {
            Console.WriteLine($"Элемент {item} удален из списка.");
        }
        else
        {
            Console.WriteLine($"Элемент {item} не найден в списке.");
        }
    }

    public T GetAt(int index)
    {
        if (index < 0 || index >= _items.Count)
            throw new IndexOutOfRangeException("Индекс выходит за пределы списка.");

        return _items[index];
    }

    public IEnumerable<T> GetAll()
    {
        return _items;
    }
}

public class ListManager<T>
{
    private readonly IListManager<T> _listManager;

    public ListManager(IListManager<T> listManager)
    {
        _listManager = listManager;
    }

    public void PrintAll()
    {
        Console.WriteLine("Все элементы в списке:");
        foreach (var item in _listManager.GetAll())
        {
            Console.WriteLine(item);
        }
    }

    public bool Contains(T item)
    {
        foreach (var existingItem in _listManager.GetAll())
        {
            if (EqualityComparer<T>.Default.Equals(existingItem, item))
            {
                return true;
            }
        }
        return false;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        SimpleListManager<string> stringListManager = new SimpleListManager<string>();
        ListManager<string> listManager = new ListManager<string>(stringListManager);

        listManager.PrintAll();
        stringListManager.Add("Apple");
        stringListManager.Add("Banana");
        stringListManager.Add("Cherry");

        listManager.PrintAll();
        Console.WriteLine($"Содержит ли 'Banana'? {listManager.Contains("Banana")}");

        stringListManager.Remove("Banana");
        listManager.PrintAll();
    }
}