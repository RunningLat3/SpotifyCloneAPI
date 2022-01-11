using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace LoggingService;

public static class LoggingServiceExtensions
{
    public static IServiceCollection AddLoggingService(this IServiceCollection services, IHostBuilder builder)
    {
        builder.UseSerilog((ctx, provider, lc) =>
            lc.ReadFrom.Configuration(ctx.Configuration)
        );

        return services;
    }
    public static IApplicationBuilder UseLoggingService(this IApplicationBuilder builder)
    {
        builder.UseSerilogRequestLogging(options =>
        {
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                diagnosticContext.Set("RequestMethod", httpContext.Request.Method);
                diagnosticContext.Set("RequestProtocol", httpContext.Request.Protocol);
                diagnosticContext.Set("RequestPath", httpContext.Request.Path);
                diagnosticContext.Set("RequestRemoteAddress", httpContext.Connection.RemoteIpAddress);
            };
        });

        return builder;
    }
}
