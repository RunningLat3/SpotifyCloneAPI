using System;
using System.Text;
using System.Text.Json;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;

namespace LoggingService;

public class CustomFileTextFormatter : CustomTextFormat, ITextFormatter
{
    public void Format(LogEvent logEvent, TextWriter output)
    {
        if (logEvent.Level != LogEventLevel.Information)
        {
            FormatEvent(logEvent, output);
        }
    }
}