using Xunit.Abstractions;

namespace TelemRec;

public class XunitLogger : Logger
{
    private readonly ITestOutputHelper output;
    private readonly string scope;

    public static void Init(ITestOutputHelper output)
    {
        LogFactory.Factory.Value = (scope) => new XunitLogger(output, scope);
    }

    public XunitLogger(ITestOutputHelper output, string scope)
    {
        this.output = output;
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
        output.WriteLine($"{severity} {scope} {message}");
    }
}