using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace application_insight_dotnetcore
{
    public class HealthCheckFilter : ITelemetryProcessor
    {
        private ITelemetryProcessor Next { get; set; }

        // next will point to the next TelemetryProcessor in the chain.
        public HealthCheckFilter(ITelemetryProcessor next)
        {
            this.Next = next;
        }

        public void Process(ITelemetry item)
        {
            var request = item as RequestTelemetry;

            var item1 = item as TraceTelemetry;

            var request1 = item as DependencyTelemetry;

            // request1.Success
            // item1.SeverityLevel == SeverityLevel.Critical || Error
            // request.ResponseCode
            // request.Url.AbsolutePath


            //Synthetic requests
            if (!string.IsNullOrEmpty(item.Context.Operation.SyntheticSource))
            { return; }

            // filter the pings
            // if (request != null && request.Url.AbsolutePath.Contains("/health/ping", StringComparison.OrdinalIgnoreCase))
            // {
            //     return;
            // }
            // Send everything else
            this.Next.Process(item);
        }


        // public void ProcessUnAuth(ITelemetry item)
        // {
        //     var request = item as RequestTelemetry;

        //     if (request != null &&
        //     request.ResponseCode.Equals("401", StringComparison.OrdinalIgnoreCase))
        //     {
        //         // To filter out an item, return without calling the next processor.
        //         return;
        //     }

        //     // Send everything else
        //     this.Next.Process(item);
        // }
    }
}
