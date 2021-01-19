using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Metrics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ObservabilityPlatform
{
    /// <summary>
    /// OpReporter to report custom metrics in it's own instance
    /// </summary>
    public class OpReporter : IOpReporter
    {
        private OpReporterOptions _options;
        private ILogger<OpReporter> _logger;
        #region metrics
        private Metric _incomingRequestsDurationMetric = null;
        private Metric _outgoingRequestsDurationMetric = null;
        private Metric _errorsCountMetric = null;
        private Metric _jobDurationMetric = null;
        #endregion metrics
        // accessible only for read like unit test or other scenarios
        public TelemetryClient Client { get; private set; }
        public OpReporter(ILogger<OpReporter> logger, IConfiguration configuration)
        {
            _logger = logger;
            _options = new OpReporterOptions();
            configuration.Bind(Constants.ObservabilityPlatformConfigKey, _options);
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
                if (_options.IsEnabled == false || item == null)
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
                        RecordIncomingRequest(requestItem.Duration.Milliseconds, requestItem.ResponseCode);
                    }
                }
                else if (item is DependencyTelemetry)
                {
                    var dependecyItem = item as DependencyTelemetry;
                    if (IsValidOutgoingItem(dependecyItem))
                    {
                        RecordOutgoingRequest(dependecyItem.Duration.Milliseconds, dependecyItem.ResultCode);
                    }
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
                Constants.ErrorKey);
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
                Constants.CriticalKey);
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
                Constants.ExceptionKey);
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
                                {Constants.ServiceLineKey , _options.ServiceLine },
                                {Constants.ServiceNameKey , _options.ServiceName },
                                {Constants.OperationNameKey, operationName },
                                {Constants.MessageKey, message }
                            };
            Client.TrackEvent(Constants.SosEventName, dimensions);
        }

        private void Initialize()
        {
            if (_options.IsEnabled)
            {
                _options.ValidateAndPreProcess();
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

        /// <summary>
        /// Check if the incoming item is not part of FilterPaths
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool IsValidIncomingItem(RequestTelemetry item)
        {
            if (_options.IncomingFilterPaths?.Count > 0 && item?.Url?.AbsoluteUri != null)
            {
                foreach (var filterPath in _options.IncomingFilterPaths)
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
            if (_options.OutgoingFilterPaths?.Count > 0 && item != null)
            {
                foreach (var filterPath in _options.IncomingFilterPaths)
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