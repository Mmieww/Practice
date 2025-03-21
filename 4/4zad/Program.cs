using System;

public static class StringExtensions
{
    public static string ReplaceVowelsWithAsterisk(this string input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input), "Input string cannot be null.");
        }

        char[] chars = input.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            if ("aeiouAEIOU".IndexOf(chars[i]) >= 0)
            {
                chars[i] = '*'; 
            }
        }
        return new string(chars);
    }
}

public class Program
{
    public static void Main()
    {
        string original = "Hello World";
        string modified = original.ReplaceVowelsWithAsterisk();

        Console.WriteLine($"Исходная строка: {original}");
        Console.WriteLine($"Модифицированная строка: {modified}");
    }
}