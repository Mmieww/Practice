using System;
using System.Collections.Generic;

public class FontManager
{
    private static FontManager _instance;

    private Dictionary<string, string> _fonts;

    private FontManager()
    {
        _fonts = new Dictionary<string, string>();
    }

    public static FontManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FontManager();
            }
            return _instance;
        }
    }

    public void LoadFont(string fontName)
    {
        if (!_fonts.ContainsKey(fontName))
        {
            _fonts[fontName] = $"Шрифт {fontName} загружен";
            Console.WriteLine(_fonts[fontName]);
        }
        else
        {
            Console.WriteLine($"Шрифт {fontName} уже загружен.");
        }
    }

    public string GetFont(string fontName)
    {
        if (_fonts.TryGetValue(fontName, out string font))
        {
            return font;
        }
        else
        {
            return $"Шрифт {fontName} не найден.";
        }
    }
}

class Program
{
    static void Main()
    {
        FontManager fontManager = FontManager.Instance;

        fontManager.LoadFont("Arial");
        fontManager.LoadFont("Times New Roman");
        fontManager.LoadFont("Arial"); 

        Console.WriteLine(fontManager.GetFont("Arial"));
        Console.WriteLine(fontManager.GetFont("Times New Roman"));
        Console.WriteLine(fontManager.GetFont("Helvetica")); 
    }
}