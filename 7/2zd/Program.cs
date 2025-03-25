using System;
using System.IO;

public class FileSecurity
{
    public void OpenSecureFile(string path)
    {
        throw new UnauthorizedAccessException("Доступ к файлу запрещен.");
    }
}

public class SecurityException : Exception
{
    public SecurityException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class FileAccessManager
{
    private readonly FileSecurity _fileSecurity;

    public FileAccessManager()
    {
        _fileSecurity = new FileSecurity();
    }

    public void AccessFile(string filePath)
    {
        try
        {
            _fileSecurity.OpenSecureFile(filePath);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new SecurityException("Ошибка доступа к файлу", ex);
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        FileAccessManager fileAccessManager = new FileAccessManager();

        try
        {
            Console.Write("Введите путь к файлу: ");
            string filePath = Console.ReadLine();
            fileAccessManager.AccessFile(filePath);
        }
        catch (SecurityException ex)
        {
            Console.WriteLine($"Произошла ошибка безопасности: {ex.Message}");
            Console.WriteLine($"Внутреннее исключение: {ex.InnerException.Message}");
            Console.WriteLine($"Стек вызовов: {ex.StackTrace}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
        }
    }
}