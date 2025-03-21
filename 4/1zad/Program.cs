using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        int[] inputArray = { 1, 2, 3, 4, 3, 2, 1, 5 };
        int[] uniqueArray = GetUniqueElements(inputArray);

        Console.WriteLine("Уникальные элементы:");
        foreach (var item in uniqueArray)
        {
            Console.Write(item + " ");
        }
    }

    public static int[] GetUniqueElements(int[] inputArray)
    {
        HashSet<int> uniqueElements = new HashSet<int>(); 

        foreach (var item in inputArray)
        {
            uniqueElements.Add(item);
        }

        return uniqueElements.ToArray();
    }
}