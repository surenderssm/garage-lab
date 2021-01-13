using System;
using System.Linq;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace application_insight_dotnetcore
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationInsightsLoggerProvider(this IServiceCollection services, IConfiguration configuration)
        {
            //disable perf counter only for demo (https://github.com/microsoft/ApplicationInsights-ServiceFabric/issues/61)
            var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ImplementationType == typeof(PerformanceCollectorModule));
            services.Remove(serviceDescriptor);

            // string instrumentationkey = configuration.GetValue<string>("Telemetry:InstrumentationKey");
            // https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core
            var aiOptions = new ApplicationInsightsServiceOptions
            {
                EnableAdaptiveSampling = false,
                EnableAppServicesHeartbeatTelemetryModule = true,
                EnablePerformanceCounterCollectionModule = true,
                EnableRequestTrackingTelemetryModule = true,
                EnableDependencyTrackingTelemetryModule = true,
            };
            // The following line enables Application Insights telemetry collection.
            services.AddApplicationInsightsTelemetry(aiOptions);

            //telemetry processor
            services.AddApplicationInsightsTelemetryProcessor<HealthCheckFilter>();

            services.AddSingleton<ITelemetryInitializer>(new ResourceApiInitializer(configuration));
            return services;
        }

        public static ILogger GetLogger(this IServiceCollection services)
        {
            var intermediateServiceProvider = services.BuildServiceProvider();
            var logger = intermediateServiceProvider.GetService<ILogger>();
            return logger;
        }

        public static ILogger GetLogger(this HttpContext context)
        {
            var logger = (ILogger)context.RequestServices.GetService(typeof(ILogger));
            return logger;
        }
    }
}
