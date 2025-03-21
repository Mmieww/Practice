using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        string input = "abc";
        Console.WriteLine($"Перестановки строки \"{input}\":");
        List<string> permutations = Permute(input);
        foreach (var perm in permutations)
        {
            Console.WriteLine(perm);
        }
    }

    public static List<string> Permute(string str)
    {
        List<string> result = new List<string>();
        PermuteHelper(str.ToCharArray(), 0, result);
        return result;
    }

    private static void PermuteHelper(char[] chars, int index, List<string> result)
    {
        if (index == chars.Length - 1)
        {
            result.Add(new string(chars));
            return;
        }

        for (int i = index; i < chars.Length; i++)
        {
            Swap(chars, index, i); 
            PermuteHelper(chars, index + 1, result); 
            Swap(chars, index, i); 
        }
    }

    private static void Swap(char[] chars, int i, int j)
    {
        char temp = chars[i];
        chars[i] = chars[j];
        chars[j] = temp;
    }
}