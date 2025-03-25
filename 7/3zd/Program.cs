using System;

public class AccessDeniedException : Exception
{
    public AccessDeniedException()
        : base("Доступ запрещен: время доступа вне разрешенного диапазона.")
    {
    }

    public AccessDeniedException(string message)
        : base(message)
    {
    }
}

public class AccessControl
{
    public void CheckAccessTime(int hour)
    {
        if (hour < 9 || hour > 18)
        {
            throw new AccessDeniedException($"Доступ запрещён в {hour}:00. Разрешенное время доступа: с 9:00 до 18:00.");
        }
        else
        {
            Console.WriteLine($"Доступ разрешён в {hour}:00.");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        AccessControl accessControl = new AccessControl();

        try
        {
            Console.Write("Введите час доступа (0-23): ");
            int hour = Convert.ToInt32(Console.ReadLine());
            accessControl.CheckAccessTime(hour);
        }
        catch (AccessDeniedException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: Введите корректное числовое значение для часа.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
        }
    }
}