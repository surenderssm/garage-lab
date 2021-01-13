using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Metrics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace application_insight_dotnetcore
{
    /// <summary>
    /// OpReporter to report custom metrics in it's own instance
    /// </summary>
    public class OpReporter : IOpReporter
    {
        private const string SosEventName = "op_sos";
        private const string IncomingRequestsDurationMetricName = "op_incoming_requests_duration_ms";
        private const string OutgoingRequestsDurationMetricName = "op_outgoing_requests_duration_ms";
        private const string ErrorsCountMetricName = "op_errors_count";
        private const string JobDurationMetricName = "op_job_duration_ms";
        private const string ServiceLineKey = "service_line";
        private const string ServiceNameKey = "service_name";
        private const string ResultCodeKey = "result_code";
        private const string ErrorTypeKey = "error_type";
        private const string OperationNameKey = "operation_name";
        private const string MessageKey = "message";
        private const string ErrorKey = "error";
        private const string CriticalKey = "critical";
        private const string ExceptionKey = "exception";
        private const string ObservabilityPlatformConfigKey = "ObservabilityPlatform";
        private OpReporterOptions _options;
        private TelemetryClient _client;
        private ILogger<OpReporter> _logger;
        #region metrics
        private Metric _incomingRequestsDurationMetric = null;
        private Metric _outgoingRequestsDurationMetric = null;
        private Metric _errorsCountMetric = null;
        private Metric _jobDurationMetric = null;
        #endregion metrics
        public OpReporter(ILogger<OpReporter> logger, IConfiguration configuration)
        {
            _logger = logger;
            _options = new OpReporterOptions();
            configuration.Bind(ObservabilityPlatformConfigKey, _options);
            Initialize();
        }

        public OpReporter(ILogger<OpReporter> logger, OpReporterOptions opReporterOptions)
        {
            _logger = logger;
            _options = opReporterOptions;
            Initialize();
        }

        public void Process(ITelemetry item)
        {
            try
            {
                if (_options.IsEnabled == false)
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
                    // TODO : make use of _options.IncomingFilterPath
                    // filter the pings or any health check
                    // or the paths given in the config
                    if (requestItem != null && requestItem.Url.AbsolutePath.Contains("/health/ping", StringComparison.OrdinalIgnoreCase))
                    {
                        return;
                    }
                    RecordIncomingRequest(requestItem.Duration.Milliseconds, requestItem.ResponseCode);
                }
                else if (item is DependencyTelemetry)
                {
                    var dependecyItem = item as DependencyTelemetry;
                    RecordOutgoingRequest(dependecyItem.Duration.Milliseconds, dependecyItem.ResultCode);
                }
                else if (item is TraceTelemetry)
                {
                    var errorItem = item as TraceTelemetry;
                    if (errorItem.SeverityLevel == SeverityLevel.Error)
                    {
                        RecordError();
                    }
                    else if (errorItem.SeverityLevel == SeverityLevel.Critical)
                    {
                        RecordCriticalError();
                    }
                }
                else if (item is ExceptionTelemetry)
                {
                    RecordException();
                }
            }
            catch (Exception ex)
            {
                // intentionally infor and eating up exception as this should not impact the other processors in the chain
                _logger.LogWarning("Process failed", ex);
            }
        }

        public void RecordIncomingRequest(int durationInMs, string resultCode)
        {
            // This can be removed if we make this private
            // till the time it is publically accessible this check should be there
            if (_options.IsEnabled == false)
            {
                return;
            }
            _incomingRequestsDurationMetric.TrackValue(durationInMs, _options.ServiceLine, _options.ServiceName,
                resultCode);
        }

        public void RecordOutgoingRequest(int durationInMs, string resultCode)
        {
            // This can be removed if we make this private
            // till the time it is publically accessible this check should be there
            if (_options.IsEnabled == false)
            {
                return;
            }
            _outgoingRequestsDurationMetric.TrackValue(durationInMs, _options.ServiceLine, _options.ServiceName,
                resultCode);
        }

        public void RecordError()
        {
            // This can be removed if we make this private
            // till the time it is publically accessible this check should be there
            if (_options.IsEnabled == false)
            {
                return;
            }
            _errorsCountMetric.TrackValue(1, _options.ServiceLine, _options.ServiceName,
                ErrorKey);
        }

        public void RecordCriticalError()
        {
            // This can be removed if we make this private
            // till the time it is publically accessible this check should be there
            if (_options.IsEnabled == false)
            {
                return;
            }
            _errorsCountMetric.TrackValue(1, _options.ServiceLine, _options.ServiceName,
                ErrorKey);
        }


        public void RecordException()
        {
            // This can be removed if we make this private
            // till the time it is publically accessible this check should be there
            if (_options.IsEnabled == false)
            {
                return;
            }
            _errorsCountMetric.TrackValue(1, _options.ServiceLine, _options.ServiceName,
                ExceptionKey);
        }

        public void RecordJob(int durationInMs, string resultCode, string operationName)
        {
            // This can be removed if we make this private
            // till the time it is publically accessible this check should be there
            if (_options.IsEnabled == false)
            {
                return;
            }
            _jobDurationMetric.TrackValue(durationInMs, _options.ServiceLine, _options.ServiceName,
                resultCode, operationName);
        }

        /// <summary>
        /// RecordSosEvent app can use to raise an SOS to seek immediate attention. Like queue full, cert expired, invalid keys,app not able to startup
        /// </summary>
        /// <param name="operationName"></param>
        /// <param name="message"></param>
        public void RecordSosEvent(string operationName, string message)
        {
            if (_options.IsEnabled == false)
            {
                return;
            }

            var dimensions = new Dictionary<string, string>
                            {
                                {ServiceLineKey , _options.ServiceLine },
                                {ServiceNameKey , _options.ServiceName },
                                {OperationNameKey, operationName },
                                {MessageKey, message }
                            };
            _client.TrackEvent(SosEventName, dimensions);
        }

        private void Initialize()
        {
            if (_options.IsEnabled)
            {
                InitializeClient();
                InitializeMetrics();
                _logger.LogInformation("Reporter is Enabled and initialized");
            }
            else
            {
                _logger.LogWarning("OpReporter is Disabled");
            }
        }

        private void InitializeClient()
        {
            var config = new TelemetryConfiguration(_options.InstrumentationKey);
            _client = new TelemetryClient(config);
        }

        private void InitializeMetrics()
        {
            _incomingRequestsDurationMetric = _client.GetMetric(IncomingRequestsDurationMetricName, ServiceLineKey,
                                                        ServiceNameKey, ResultCodeKey);

            _outgoingRequestsDurationMetric = _client.GetMetric(OutgoingRequestsDurationMetricName, ServiceLineKey,
                                                        ServiceNameKey, ResultCodeKey);

            _errorsCountMetric = _client.GetMetric(ErrorsCountMetricName, ServiceLineKey,
                                                        ServiceNameKey, ErrorTypeKey);

            var jobDurationMetricId = new MetricIdentifier(JobDurationMetricName, ServiceLineKey,
                                                        ServiceNameKey, ResultCodeKey, OperationNameKey);

            _jobDurationMetric = _client.GetMetric(jobDurationMetricId);
        }
    }
}