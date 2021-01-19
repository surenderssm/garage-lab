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
        private IOpReporter _opReporter;

        // next will point to the next TelemetryProcessor in the chain.
        public OpMetricTelemetryProcessor(ITelemetryProcessor next)
        {
            _next = next;
            _opReporter = OpReporterProvider.Reporter;
        }

        // https://github.com/microsoft/ApplicationInsights-dotnet/issues/1536
        // public OpMetricTelemetryProcessor(ITelemetryProcessor next, IOpReporter reporter)
        // {
        //     _next = next;
        //     _opReporter = reporter;
        // }

        public void Process(ITelemetry item)
        {
            // // TODO : 
            // // Depending on the DI provider one can get IOpReporter
            // // This is to keep simple for time being for the quick experiment
            // OpReporterProvider.Reporter.Process(item);

            if (_opReporter != null)
            {
                _opReporter.Process(item);
            }
            _next.Process(item);
        }
    }
}
