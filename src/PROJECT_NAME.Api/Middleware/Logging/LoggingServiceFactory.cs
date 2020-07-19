using System;
using PROJECT_NAME.Infrastructure;
using CorrelationId.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace PROJECT_NAME.Api.Middleware.Logging
{
    public static class LoggingServiceFactory
    {
        /// <summary>
        /// Get a logging service that enriches with the correlation id
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static ILogger LoggingService(IServiceProvider provider)
        {

            var httpAccessor = provider.GetRequiredService<IHttpContextAccessor>();
            var correlationAccessor = provider.GetRequiredService<ICorrelationContextAccessor>();
            var configuration = provider.GetRequiredService<IOptions<EnvironmentConfiguration>>();
            var logLevelStr = configuration.Value.LOG_LEVEL;

            var logLevel = Enum.TryParse(logLevelStr, out LogEventLevel level) ? level : LogEventLevel.Information;

            var formatter = new JsonLogFormatter();

            var conf = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.With(new HttpEnricher(httpAccessor, correlationAccessor))
                .MinimumLevel.ControlledBy(new LoggingLevelSwitch(logLevel));

            conf.WriteTo.Console(formatter);

            return conf.CreateLogger();
        }
    }
}
