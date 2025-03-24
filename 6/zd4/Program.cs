using System;

public delegate void DownloadProgressChangedHandler(object sender, DownloadProgressEventArgs e);
public class FileDownloader
{
    public event DownloadProgressChangedHandler DownloadProgressChanged;

    public void DownloadFile(string filePath)
    {
        Console.WriteLine($"String download for : {filePath}");

        for (int progress  = 0; progress <= 100; progress += 10)
        {
            System.Threading.Thread.Sleep(500);

            OnDownloadProgressChanged(progress);
        }
        Console.WriteLine("Download completed");
    }

    protected virtual void OnDownloadProgressChanged(int progress)
    {
        DownloadProgressChanged?.Invoke(this, new DownloadProgressEventArgs(progress));
    }
}

public class DownloadProgressEventArgs : EventArgs
{
    public int Progress { get; }

    public DownloadProgressEventArgs(int progress)
    {
        Progress = progress;
    }
}

public class DownloadMonitor
{
    private readonly FileDownloader _fileDownloader;

    public DownloadMonitor(FileDownloader fileDownloader)
    {
        _fileDownloader = fileDownloader;

        _fileDownloader.DownloadProgressChanged += UpdateProgressBar;
        _fileDownloader.DownloadProgressChanged += LogProgress;
    }

    private void UpdateProgressBar(object sender, DownloadProgressEventArgs e)
    {
        Console.WriteLine($"ProgressBar Updated: {e.Progress}%");
    }

    private void LogProgress(object sender, DownloadProgressEventArgs e)
    {
        Console.WriteLine($"Logger: Downloaded {e.Progress}%");
    }
}

public class Program
{
    public static void Main()
    {
        FileDownloader fileDownloader = new FileDownloader();
        DownloadMonitor downloadMonitor = new DownloadMonitor(fileDownloader);

        fileDownloader.DownloadFile("example_file.txt");
    }
}