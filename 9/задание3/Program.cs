using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CategoryItem
{
    public string Category { get; set; }
    public string Name { get; set; }

    public CategoryItem(string category, string name)
    {
        Category = category;
        Name = name;
    }
}

public class CategoryFileReader
{
    private readonly string _filePath;

    public CategoryFileReader(string filePath)
    {
        _filePath = filePath;
    }

    public List<CategoryItem> ReadItems()
    {
        List<CategoryItem> items = new List<CategoryItem>();

        using (StreamReader reader = new StreamReader(_filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(';');
                if (parts.Length == 2)
                {
                    string category = parts[0].Trim();
                    string name = parts[1].Trim();
                    items.Add(new CategoryItem(category, name));
                }
            }
        }

        return items;
    }
}

public class CategoryProcessor
{
    public Dictionary<string, int> CountByCategory(List<CategoryItem> items)
    {
        return items
            .GroupBy(item => item.Category)
            .ToDictionary(group => group.Key, group => group.Count());
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        string filePath = "file.data"; // указать путь к файлу

        CategoryFileReader categoryFileReader = new CategoryFileReader(filePath);
        List<CategoryItem> items = categoryFileReader.ReadItems();

        CategoryProcessor categoryProcessor = new CategoryProcessor();
        Dictionary<string, int> categoryCounts = categoryProcessor.CountByCategory(items);

        Console.WriteLine("Количество записей по категориям:");
        foreach (var category in categoryCounts)
        {
            Console.WriteLine($"{category.Key}: {category.Value}");
        }
    }
}
