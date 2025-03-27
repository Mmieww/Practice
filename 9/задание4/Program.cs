using System;
using System.IO;

public class FileWatcher
{
    private readonly string _directoryToWatch;
    private readonly FileSystemWatcher _fileSystemWatcher;

    public FileWatcher(string directoryToWatch)
    {
        _directoryToWatch = directoryToWatch;
        _fileSystemWatcher = new FileSystemWatcher(_directoryToWatch);

        _fileSystemWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.LastAccess;
        _fileSystemWatcher.Filter = "*.*"; 

        _fileSystemWatcher.Created += OnCreated;
        _fileSystemWatcher.Deleted += OnDeleted;
        _fileSystemWatcher.Changed += OnChanged;
        _fileSystemWatcher.Renamed += OnRenamed;
    }

    public void Start()
    {
        _fileSystemWatcher.EnableRaisingEvents = true;
        Console.WriteLine($"Начато отслеживание изменений в каталоге: {_directoryToWatch}");
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"Файл создан: {e.FullPath}");
        LogEvent("Created", e.FullPath);
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"Файл удален: {e.FullPath}");
        LogEvent("Deleted", e.FullPath);
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"Файл изменен: {e.FullPath}");
        LogEvent("Changed", e.FullPath);
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        Console.WriteLine($"Файл переименован: {e.OldFullPath} -> {e.FullPath}");
        LogEvent("Renamed", e.FullPath, e.OldFullPath);
    }

    private void LogEvent(string eventType, string fullPath, string oldFullPath = null)
    {
        string logFilePath = "file_events.csv"; // Путь к файлу логов
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            if (new FileInfo(logFilePath).Length == 0)
            {
                writer.WriteLine("EventType,FilePath,OldFilePath,DateTime");
            }
            writer.WriteLine($"{eventType},{fullPath},{oldFullPath},{DateTime.Now}");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        string directoryToWatch = @"C:\Path\To\Your\Directory"; // указать путь к директории

        FileWatcher fileWatcher = new FileWatcher(directoryToWatch);
        fileWatcher.Start();

        Console.WriteLine("Нажмите Enter для выхода...");
        Console.ReadLine();
    }
}