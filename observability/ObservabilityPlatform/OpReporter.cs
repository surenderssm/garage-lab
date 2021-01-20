using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Metrics;
using Microsoft.Extensions.Configuration;

namespace ObservabilityPlatform
{
    /// <summary>
    /// OpReporter to report custom metrics in it's own instance
    /// </summary>
    public class OpReporter : IOpReporter
    {
        private OpReporterOptions _options;
        private Metric _incomingRequestsDurationMetric = null;
        private Metric _outgoingRequestsDurationMetric = null;
        private Metric _errorsCountMetric = null;
        private Metric _jobDurationMetric = null;
        // accessible only for read like unit test or other scenarios
        public TelemetryClient Client { get; private set; }

        public bool IsEnabled { get { return _options.IsEnabled; } }

        public IList<string> IncomingFilterPaths { get { return _options.IncomingFilterPaths; } }

        public IList<string> OutgoingFilterPaths { get { return _options.OutgoingFilterPaths; } }

        public OpReporter(IConfiguration configuration)
        {
            _options = new OpReporterOptions();
            configuration.Bind(Constants.ObservabilityPlatformConfigKey, _options);
            Initialize();
        }

        public OpReporter(OpReporterOptions opReporterOptions)
        {
            _options = opReporterOptions;
            Initialize();
        }

        public bool RecordIncomingRequest(int durationInMs, string resultCode)
        {
            if (_options.IsEnabled == false || durationInMs < 1 || string.IsNullOrWhiteSpace(resultCode))
            {
                return false;
            }

            return _incomingRequestsDurationMetric.TrackValue(durationInMs, _options.ServiceLine,
                        _options.ServiceName, resultCode);
        }

        public bool RecordOutgoingRequest(int durationInMs, string resultCode)
        {
            if (_options.IsEnabled == false || durationInMs < 1 || string.IsNullOrWhiteSpace(resultCode))
            {
                return false;
            }
            return _outgoingRequestsDurationMetric.TrackValue(durationInMs, _options.ServiceLine,
                        _options.ServiceName, resultCode);
        }

        public void RecordError()
        {
            if (_options.IsEnabled == false)
            {
                return;
            }
            _errorsCountMetric.TrackValue(1, _options.ServiceLine, _options.ServiceName,
                Constants.ErrorKey);
        }

        public void RecordCriticalError()
        {
            if (_options.IsEnabled == false)
            {
                return;
            }
            _errorsCountMetric.TrackValue(1, _options.ServiceLine, _options.ServiceName,
                Constants.CriticalKey);
        }

        public void RecordException()
        {
            if (_options.IsEnabled == false)
            {
                return;
            }
            _errorsCountMetric.TrackValue(1, _options.ServiceLine, _options.ServiceName,
                Constants.ExceptionKey);
        }

        public bool RecordJob(int durationInMs, string resultCode, string operationName)
        {
            if (_options.IsEnabled == false
                    || durationInMs < 1
                    || string.IsNullOrWhiteSpace(resultCode)
                    || string.IsNullOrWhiteSpace(operationName)
                )
            {
                return false;
            }

            return _jobDurationMetric.TrackValue(durationInMs, _options.ServiceLine, _options.ServiceName,
                resultCode, operationName);
        }

        /// <summary>
        /// RecordSosEvent app can use to raise an SOS to seek immediate attention. Like queue full, cert expired, invalid keys,app not able to startup
        /// </summary>
        /// <param name="operationName"></param>
        /// <param name="message"></param>
        public bool RecordSosEvent(string operationName, string message)
        {
            if (_options.IsEnabled == false
                || string.IsNullOrWhiteSpace(operationName)
                || string.IsNullOrWhiteSpace(message)
            )
            {
                return false;
            }

            var dimensions = new Dictionary<string, string>
                            {
                                {Constants.ServiceLineKey , _options.ServiceLine },
                                {Constants.ServiceNameKey , _options.ServiceName },
                                {Constants.OperationNameKey, operationName },
                                {Constants.MessageKey, message }
                            };
            Client.TrackEvent(Constants.SosEventName, dimensions);
            // this is to seek immediate attention, hence the flush
            Client.Flush();
            return true;
        }

        private void Initialize()
        {
            if (_options.IsEnabled)
            {
                _options.ValidateAndPreProcess();
                InitializeClient();
                InitializeMetrics();
                Trace.TraceInformation("Reporter is Enabled and initialized");
            }
            else
            {
                Trace.TraceWarning("Reporter is disbaled");
            }
        }

        private void InitializeClient()
        {
            var config = new TelemetryConfiguration(_options.InstrumentationKey);
            Client = new TelemetryClient(config);
        }

        private void InitializeMetrics()
        {
            _incomingRequestsDurationMetric = Client.GetMetric(Constants.IncomingRequestsDurationMetricName, Constants.ServiceLineKey,
                                                        Constants.ServiceNameKey, Constants.ResultCodeKey);

            _outgoingRequestsDurationMetric = Client.GetMetric(Constants.OutgoingRequestsDurationMetricName, Constants.ServiceLineKey,
                                                        Constants.ServiceNameKey, Constants.ResultCodeKey);

            _errorsCountMetric = Client.GetMetric(Constants.ErrorsCountMetricName, Constants.ServiceLineKey,
                                                        Constants.ServiceNameKey, Constants.ErrorTypeKey);

            var jobDurationMetricId = new MetricIdentifier(Constants.JobDurationMetricName, Constants.ServiceLineKey,
                                                        Constants.ServiceNameKey, Constants.ResultCodeKey, Constants.OperationNameKey);

            _jobDurationMetric = Client.GetMetric(jobDurationMetricId);
        }
    }
}