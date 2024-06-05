using TelemRec;

namespace Basekeeper.Diagnostics;

public class ConsoleLogger : Logger
{
    private readonly string scope;

    public static void Init()
    {
        LogFactory.Factory.Value = (scope) => new ConsoleLogger(scope);
    }

    public ConsoleLogger(string scope)
    {
        this.scope = scope;
    }

    public void Debug(string message)
    {
        WriteLog(message, "DEBUG");
    }

    public void Info(string message)
    {
        WriteLog(message, "INFO");
    }

    public void Warn(string message)
    {
        WriteLog(message, "WARN");
    }

    private void WriteLog(string message, string severity)
    {
        Console.WriteLine($"{severity} {scope} {message}");
    }
}