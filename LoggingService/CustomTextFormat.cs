using Serilog.Events;
using Serilog.Formatting.Json;

namespace LoggingService;

public class CustomTextFormat
{
    protected internal void FormatEvent(LogEvent logEvent, TextWriter output)
    {
        string logLevel = FormatLogLevel(logEvent.Level);
        var sourceContext = logEvent.Properties.GetValueOrDefault("SourceContext")?.ToString().Replace("\"", String.Empty) ?? "<none>";
        string message = logEvent.MessageTemplate.Render(logEvent.Properties);
        output.WriteLine($"[{logEvent.Timestamp:yyyy-MM-dd'T'HH:mm:ss.fff}] [{logLevel}] [{sourceContext}] {message}");

        output.Write("{");
        foreach (var item in logEvent.Properties.Select((Entry, Index) => new { Entry, Index }))
        {
            if (item.Index != 0) output.Write(',');
            output.Write(item.Entry.Key);
            output.Write(':');
            output.Write(item.Entry.Value);
        }
        output.Write("}");

        if (logEvent.Exception is not null)
        {
            output.Write("\nException - {0} ", logEvent.Exception);
            output.Write("StackTrace - {0} ", logEvent.Exception.StackTrace);
            output.Write("Message - {0} ", logEvent.Exception.Message);
            output.Write("Source - {0} ", logEvent.Exception.Source);
            output.Write("InnerException - {0}", logEvent.Exception.InnerException);
        }

        output.WriteLine();
    }

    private string FormatLogLevel(LogEventLevel logLevel)
    {
        switch (logLevel)
        {
            case LogEventLevel.Debug:
                return "DEBUG";
            case LogEventLevel.Error:
                return "ERROR";
            case LogEventLevel.Warning:
                return "WARN";
            case LogEventLevel.Fatal:
                return "CRITICAL";
            case LogEventLevel.Verbose:
                return "VERBOSE";
            default:
                return "INFO";
        }
    }
}
