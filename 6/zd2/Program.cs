using System;

public delegate void DataAnalyze(int[] data);

public class Program
{
    public static void Main()
    {
        int[] sampleData = { 17, 13, 4, 1, 52 };

        AnalyzeData(sampleData, CalculateAverage);

        AnalyzeData(sampleData, FindMaximum);
    }

    public static void AnalyzeData(int[] data, DataAnalyze analyzer)
    {
        Console.WriteLine("Data: " + string.Join(", ", data));
        analyzer(data);
    }

    public static void CalculateAverage(int[] data)
    {
        double average = 0;
        foreach (var number in data)
        {
            average += number;
        }
        average /= data.Length;
        Console.WriteLine($"Average: {average}");
    }

    public static void FindMaximum(int[] data)
    {
        int max = data[0];
        foreach (var number in data)
        {
            if (number > max)
            {
                max = number;
            }
        }
        Console.WriteLine($"Maximum:{max}");
    }
}