using System;
using System.Text;
using System.Text.Json;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;

namespace LoggingService;

public class CustomConsoleTextFormatter : CustomTextFormat, ITextFormatter
{
    public void Format(LogEvent logEvent, TextWriter output)
    {
        FormatEvent(logEvent, output);
    }

}