using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;

namespace application_insight_dotnetcore
{


    public class ResourceApiInitializer : ITelemetryInitializer
    {
        private static string BuildVersion;
        private static string ServiceName = "DemoApi";

        public ResourceApiInitializer(IConfiguration configuration)
        {
            BuildVersion = configuration["BuildVersion"];
        }
        // Telemetry initializers always run before telemetry processors.
        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Component.Version = BuildVersion;

            // only in request module
            var requestTelemetry = telemetry as RequestTelemetry;
            if (requestTelemetry == null) return;

            requestTelemetry.Properties.Add("ServiceName", ServiceName);
        }
    }
}
