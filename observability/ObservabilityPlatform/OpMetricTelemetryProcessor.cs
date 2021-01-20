using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace ObservabilityPlatform
{
    /// <summary>
    /// OpMetricTelemetryProcessor processor to be injected in the pipeline of the source app (appinsight)
    /// </summary>
    public class OpMetricTelemetryProcessor : ITelemetryProcessor
    {
        private ITelemetryProcessor _next { get; set; }
        public IOpReporter Reporter;

        // next will point to the next TelemetryProcessor in the chain.
        public OpMetricTelemetryProcessor(ITelemetryProcessor next)
        {
            _next = next;
            // TODO : Depending on the DI provider one can get IOpReporter,This is to keep simple for time being for the quick experiment
            Reporter = OpReporterProvider.Reporter;
        }

        // https://github.com/microsoft/ApplicationInsights-dotnet/issues/1536
        // public OpMetricTelemetryProcessor(ITelemetryProcessor next, IOpReporter reporter)
        // {
        //     _next = next;
        //     _opReporter = reporter;
        // }

        public void Process(ITelemetry item)
        {
            ProcessViaReporter(item);
            _next.Process(item);
        }

        private void ProcessViaReporter(ITelemetry item)
        {
            try
            {
                if (Reporter == null || Reporter.IsEnabled == false || item == null)
                {
                    return;
                }

                //Synthetic requests
                if (!string.IsNullOrEmpty(item.Context.Operation.SyntheticSource))
                {
                    return;
                }

                // looking at every telemetry item going out and record appropriate item
                if (item is RequestTelemetry)
                {
                    var requestItem = item as RequestTelemetry;
                    if (IsValidIncomingItem(requestItem))
                    {
                        Reporter.RecordIncomingRequest(requestItem.Duration.Milliseconds, requestItem.ResponseCode);
                    }
                }
                else if (item is DependencyTelemetry)
                {
                    var dependecyItem = item as DependencyTelemetry;
                    if (IsValidOutgoingItem(dependecyItem))
                    {
                        Reporter.RecordOutgoingRequest(dependecyItem.Duration.Milliseconds, dependecyItem.ResultCode);
                    }
                }
                else if (item is TraceTelemetry)
                {
                    var errorItem = item as TraceTelemetry;
                    if (errorItem.SeverityLevel == SeverityLevel.Error)
                    {
                        Reporter.RecordError();
                    }
                    else if (errorItem.SeverityLevel == SeverityLevel.Critical)
                    {
                        Reporter.RecordCriticalError();
                    }
                }
                else if (item is ExceptionTelemetry)
                {
                    Reporter.RecordException();
                }
            }
            catch (Exception ex)
            {
                // intentionally infor and eating up exception as this should not impact the other processors in the chain
                System.Diagnostics.Trace.TraceWarning("ProcessViaReporter failed", ex);
            }
        }

        /// <summary>
        /// Check if the incoming item is not part of FilterPaths
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool IsValidIncomingItem(RequestTelemetry item)
        {
            if (item != null & Reporter.IncomingFilterPaths?.Count > 0 && item?.Url?.AbsoluteUri != null)
            {
                foreach (var filterPath in Reporter.IncomingFilterPaths)
                {
                    // Contains(Char, StringComparison) is only availble in .Net 5.0 and .Net Standard 2.1
                    if (item.Url.AbsoluteUri.IndexOf(filterPath, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Check if the outgoing item is not part of FilterPaths
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool IsValidOutgoingItem(DependencyTelemetry item)
        {
            if (item != null && Reporter.OutgoingFilterPaths?.Count > 0)
            {
                foreach (var filterPath in Reporter.OutgoingFilterPaths)
                {
                    if (item.Data != null && item.Data.IndexOf(filterPath, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return false;
                    }
                    // sql server anme and other details will be available in Name
                    if (item.Name != null && item.Name.IndexOf(filterPath, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
