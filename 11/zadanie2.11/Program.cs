using System;

public interface ILogger
{
    string Log(string message);
}

public class BasicLogger : ILogger
{
    public string Log(string message)
    {
        return message;
    }
}

public abstract class LoggerDecorator : ILogger
{
    protected ILogger _logger;

    public LoggerDecorator(ILogger logger)
    {
        _logger = logger;
    }

    public virtual string Log(string message)
    {
        return _logger.Log(message); 
    }
}

public class TimestampDecorator : LoggerDecorator
{
    public TimestampDecorator(ILogger logger) : base(logger) { }

    public override string Log(string message)
    {
        return $"{DateTime.Now}: {_logger.Log(message)}";
    }
}

public class SeverityDecorator : LoggerDecorator
{
    private string _severity;

    public SeverityDecorator(ILogger logger, string severity) : base(logger)
    {
        _severity = severity;
    }

    public override string Log(string message)
    {
        return $"{_severity}: {_logger.Log(message)}";
    }
}

public class UserDecorator : LoggerDecorator
{
    private string _username;

    public UserDecorator(ILogger logger, string username) : base(logger)
    {
        _username = username;
    }

    public override string Log(string message)
    {
        return $"User: {_username}, {_logger.Log(message)}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        ILogger logger = new BasicLogger();

        logger = new TimestampDecorator(logger);  
        logger = new SeverityDecorator(logger, "INFO"); 
        logger = new UserDecorator(logger, "JohnDoe");  

        string logMessage = logger.Log("Это тестовое сообщение.");

        Console.WriteLine(logMessage);
    }
}