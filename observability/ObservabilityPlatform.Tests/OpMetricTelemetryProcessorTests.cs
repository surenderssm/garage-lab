using Xunit;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace ObservabilityPlatform.Tests
{
    public class OpMetricTelemetryProcessorTests
    {

        public OpMetricTelemetryProcessorTests()
        {
        }

        [Fact]
        public void CheckMetricProcessorWithNullOpReporter()
        {
            var dummyProcessor = new DummyProcessor();
            var processor = new OpMetricTelemetryProcessor(dummyProcessor);
            var item = new RequestTelemetry();
            processor.Process(item);
            Assert.True(dummyProcessor.Count == 1);
        }

        [Fact]
        public void CheckMetricProcessorWithValidOpReporter()
        {
            var dummyProcessor = new DummyProcessor();
            SetOpReporter();
            var reporter = (OpReporterProvider.Reporter as OpReporter);
            var metric = reporter.Client.GetMetric(Constants.IncomingRequestsDurationMetricName, Constants.ServiceLineKey,
                                                     Constants.ServiceNameKey, Constants.ResultCodeKey);

            var processor = new OpMetricTelemetryProcessor(dummyProcessor);
            var item = new RequestTelemetry();
            item.Duration = TimeSpan.FromMilliseconds(1);
            item.ResponseCode = "200";
            processor.Process(item);

            // By default series count is coming to be 1, so any processing changing the count
            Assert.True(metric.SeriesCount == 2);

            item = new RequestTelemetry();
            item.Duration = TimeSpan.FromMilliseconds(1);
            item.ResponseCode = "404";
            processor.Process(item);
            // change of response code should lead to change in series count
            Assert.True(metric.SeriesCount == 3);
        }

        private void SetOpReporter()
        {
            var options = new OpReporterOptions
            {
                IsEnabled = true,
                InstrumentationKey = "test",
                ServiceLine = "A",
                ServiceName = "1"
            };
            OpReporterProvider.Reporter = new OpReporter(Mock.Of<ILogger<OpReporter>>(), options);
        }
    }
}
