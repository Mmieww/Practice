using System;
using System.IO;
using System.Linq;


public class FileManager
{
    private readonly string _fileName;

    public FileManager(string fileName)
    {
        _fileName = fileName;
    }

    public void CreateFile(string content)
    {
        File.WriteAllText(_fileName, content);
        Console.WriteLine($"Файл '{_fileName}' создан. Текст записан в файл.");
    }

    public void DeleteFile()
    {
        if (File.Exists( _fileName ))
        {
            File.Delete( _fileName );
            Console.WriteLine("Файл удален.");
        }
        else
        {
            Console.WriteLine($"Файл '{_fileName}' не существует.");
        }
    }

    public void CopyFile(string destinationPath)
    {
        File.Copy( _fileName, destinationPath, true );
        Console.WriteLine($"Файл скопирован в '{destinationPath}'.");
    }

    public void MoveFile(string destinationPath)
    {
        File.Move(_fileName, destinationPath);
        Console.WriteLine($"Файл перенесен в '{destinationPath}.");
    }

    public void RenameFile(string newFileName)
    {
        File.Move(_fileName, newFileName);
        Console.WriteLine($"Файл переименован в '{newFileName}'.");
    }

    public void DeleteFilesByPuttern(string destinationPath, string searchPattern)
    {
        var files = Directory.GetFiles( destinationPath, searchPattern );
        foreach ( var file in files )
        {
            File.Delete(file);
            Console.WriteLine("Файл удален.");
        }
    }

    public long GetFileSize()
    {
        return new FileInfo(_fileName).Length;
    }

    public bool CompareFileSize(string otherFileName)
    {
        long SizeA = this.GetFileSize();
        long SizeB = new FileInfo(otherFileName).Length;
        return SizeA == SizeB;
    }
}

public class FileInfoProvider
{
    private readonly string _fileName;

    public FileInfoProvider(string fileName)
    {
        _fileName = fileName;
    }

    public void GetFileInfo()
    {
        if (File.Exists(_fileName))
        {
            FileInfo fileInfo = new FileInfo(_fileName);
            Console.WriteLine($"Размер файла: {fileInfo.Length} байт.");
            Console.WriteLine($"Дата создания: {fileInfo.CreationTime}");
            Console.WriteLine($"Дата последнего изменения:{fileInfo.LastWriteTime}");
        }
        else
        {
            Console.WriteLine("Файл не найден");
        }
    }

    public void CheckFileAccess()
    {
        if (File.Exists(_fileName))
        {
            Console.WriteLine($"Права доступа к файлу '{_fileName}':");
            var attributes = File.GetAttributes(_fileName);
            Console.WriteLine($"Чтение: {(attributes & FileAttributes.ReadOnly) == 0}");
            Console.WriteLine($"Запись: {!(attributes.HasFlag(FileAttributes.ReadOnly))}");
        }
        else
        {
            Console.WriteLine("Файл не найден.");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        string fileName = "kiselova.lo";
        FileManager fileManager = new FileManager(fileName);
        FileInfoProvider fileInfoProvider = new FileInfoProvider(fileName);

        fileManager.CreateFile("Hello world!");

        fileManager.DeleteFile();

        fileInfoProvider.GetFileInfo();

        fileManager.CreateFile("Hello world!");
        string copyFileName = "kiselova_copy.lo";
        fileManager.CopyFile(copyFileName);

        string newDirectory = "C://Users//miew//OneDrive//Desktop//Практика С#/";
        if (!Directory.Exists(newDirectory))
        {
            Directory.CreateDirectory(newDirectory); 
        }

        string movedFileName = Path.Combine(newDirectory, fileName);
        fileManager.MoveFile(movedFileName);

        string renamedFileName = Path.ChangeExtension(movedFileName, ".io");
        fileManager.RenameFile(renamedFileName);

        fileManager.DeleteFile();

        fileInfoProvider = new FileInfoProvider(renamedFileName);
        bool isSameSize = fileManager.CompareFileSize(copyFileName);
        Console.WriteLine($"Файлы имеют одинаковый размер: {isSameSize}");

        string pattern = "*.lo"; 
        fileManager.DeleteFilesByPuttern(newDirectory, pattern);

        var files = Directory.GetFiles(newDirectory);
        Console.WriteLine("Все файлы в директории: ");
        foreach(var file in files)
        {
            Console.WriteLine(file);
        }

        File.SetAttributes(renamedFileName, FileAttributes.ReadOnly);
        try
        {
            File.AppendAllText(renamedFileName, "Новая запись.");
            Console.WriteLine("Запись успешна.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Ошибка: доступк файлу запрещен.");
        }

        fileInfoProvider.CheckFileAccess();
    }
}