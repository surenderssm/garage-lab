using System;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace application_insight_dotnetcore
{
    public class MetricProcessor : ITelemetryProcessor
    {
        private ITelemetryProcessor Next { get; set; }
        private TelemetryClient _client;
        // next will point to the next TelemetryProcessor in the chain.
        public MetricProcessor(ITelemetryProcessor next)
        {
            this.Next = next;
            //TODO : don't do this
            this._client = new TelemetryClient();
        }

        public void Process(ITelemetry item)
        {
            // TODO Move metric names to top
            // including the metric handle
            //Synthetic requests
            if (!string.IsNullOrEmpty(item.Context.Operation.SyntheticSource))
            { return; }

            // looking at every telemetry item going out of the app
            // if they are error or critical increase the counter
            if (item is TraceTelemetry || item is ExceptionTelemetry)
            {
                var errorMetric = _client.GetMetric("api_errors_count", "type");
                var errorItem = item as TraceTelemetry;

                if (errorItem != null && (errorItem.SeverityLevel == SeverityLevel.Critical
                || errorItem.SeverityLevel == SeverityLevel.Error))
                {

                    errorMetric.TrackValue(1, errorItem.SeverityLevel.ToString());
                }

                var exceptionItem = item as ExceptionTelemetry;
                if (exceptionItem != null)
                {
                    errorMetric.TrackValue(1, "Exception");
                }
            }

            var requestItem = item as RequestTelemetry;
            if (requestItem != null)
            {
                // filter the pings
                if (requestItem != null && requestItem.Url.AbsolutePath.Contains("/health/ping", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
                var requestCountMetric = _client.GetMetric("api_incoming_requests_count", "operation_name", "result_code");
                requestCountMetric.TrackValue(1, requestItem.Name, requestItem.ResponseCode);

                var requestDurationMetric = _client.GetMetric("api_incoming_requests_duration_ms", "operation_name", "result_code");
                requestDurationMetric.TrackValue(requestItem.Duration.Milliseconds,requestItem.Name, requestItem.ResponseCode);
            }

            var dependecyItem = item as DependencyTelemetry;
            if (dependecyItem != null)
            {
                var outgoingCountMetric = _client.GetMetric("api_outgoing_requests_count", "operation_name", "result_code");
                outgoingCountMetric.TrackValue(1, dependecyItem.Name, dependecyItem.ResultCode);

                var outgoingDurationMetric = _client.GetMetric("api_outgoing_requests_duration_ms", "operation_name", "result_code");
                outgoingDurationMetric.TrackValue(dependecyItem.Duration.Milliseconds, dependecyItem.Name, dependecyItem.ResultCode);
            }

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
