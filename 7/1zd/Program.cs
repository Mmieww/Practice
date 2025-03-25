using System;

public class OverweightLuggageException : Exception
{
    public OverweightLuggageException() : base("Вес багажа превышает допустимый лимит.")
    {
    }

    public OverweightLuggageException(string message) : base(message)
    {
    }

    public OverweightLuggageException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class Luggage
{
    private const int MaxWeight = 23;

    public void CheckWeight(int weight)
    {
        if (weight > MaxWeight)
        {
            throw new OverweightLuggageException($"Вес багажа: {weight} кг. Превышает лимит в {MaxWeight} кг.");
        }
        else
        {
            Console.WriteLine($"Вес багажа: {weight} кг. В пределах допустимого.");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Luggage luggage = new Luggage();

        try
        {
            Console.Write("Введите вес багажа: ");
            int weight = Convert.ToInt32(Console.ReadLine());
            luggage.CheckWeight(weight);
        }
        catch (OverflowException ex)
        {
            Console.WriteLine ($"Ошибка: {ex.Message}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: Введите корректное числовое значение.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}