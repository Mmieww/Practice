using System;

public delegate void ProcessingCompletedHandler(string message);

public class DataProccesor
{
    public event ProcessingCompletedHandler ProcessingCompleted;

    public void ProcessData()
    {
        Console.WriteLine("Processing data...");

        System.Threading.Thread.Sleep(3000);

        OnProcessingCompleted("Data processing completed succesfully");
    }

    protected virtual void OnProcessingCompleted(string message)
    {
        ProcessingCompleted?.Invoke(message);
    }
}

public class ReportGenerator
{
    public void GeneratorReport(string message)
    {
        Console.WriteLine($"Report: {message}");
    }
}

public class Notifier
{
    public void NotifyUser(string message)
    {
        Console.WriteLine($"Notification: {message}");
    }
}

public class Program
{
    public static void Main()
    {
        DataProccesor dataProccesor = new DataProccesor();
        ReportGenerator reportGenerator = new ReportGenerator();
        Notifier notifier = new Notifier();

        dataProccesor.ProcessingCompleted += reportGenerator.GeneratorReport;
        dataProccesor.ProcessingCompleted += notifier.NotifyUser;

        dataProccesor.ProcessData();
    }
}
