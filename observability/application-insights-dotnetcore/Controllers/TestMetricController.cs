using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using application_insight_dotnetcore.Models;

namespace application_insight_dotnetcore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestMetricController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly HttpClient _client = new HttpClient();
        private readonly TelemetryClient _telemetry;
        private readonly static Random _random = new Random(1000);
        public TestMetricController(ILogger<TestMetricController> logger, TelemetryClient telemetry)
        {
            _logger = logger;
            _telemetry = telemetry;
        }

        [HttpGet("TestEvent")]
        public async Task<ActionResult> TestEvent()
        {
            var dimensions = new Dictionary<string, string>();
            dimensions["orderId"] = "123";
            _telemetry.TrackEvent("order_submission_failed", dimensions);
            await DoWork();

            return StatusCode(200, "TestLog");
        }


        [HttpGet("TestMetric")]
        public async Task<ActionResult> TestMetric()
        {
            // _logger.LogInformation(4000, "GET TestLogEvent");
            // series , dimensions
            // metric is a group of series, for evey dimension combo there is a series
            // Geneva -  6 dimensions
            // Applicatio insights - 10 ( till 3 no special way)

            // 0 dimensional metric
            var requestMetric = _telemetry.GetMetric("agreement_count");
            requestMetric.TrackValue(1);

            // 1 dimensional metric
            var submitMetric = _telemetry.GetMetric("agreement_submit_count", "type");
            submitMetric.TrackValue(1, "New");

            // 3 dimensional metric
            var metric = _telemetry.GetMetric("order_submitted_count", "Type", "Region", "Status");
            await DoWork();
            metric.TrackValue(1, "Office", "IN", "new");
            metric.TrackValue(1, "Office", "IN", "submit");

            return StatusCode(200, "TestLog");
        }

        [HttpGet("LogMetric")]
        public async Task<ActionResult> LogMetric()
        {
            // _logger.LogInformation(4000, "GET TestLogEvent");
            // series , dimensions
            // metric is a group of series, for evey dimension combo there is a series
            // Geneva -  6 dimensions
            // Applicatio insights - 10 ( till 3 no special way)

            // 0 dimensional metric
            var requestMetric = _telemetry.GetMetric("request_count", "Type");
            requestMetric.TrackValue(1);

            // 1 dimensional metric
            var submitMetric = _telemetry.GetMetric("agreement_submit_count", "type");
            submitMetric.TrackValue(1, "New");

            // 3 dimensional metric
            var metric = _telemetry.GetMetric("order_submitted_count", "Type", "Status");
            await DoWork();
            var currentTime = DateTime.UtcNow;

            while (currentTime.AddSeconds(100) > DateTime.UtcNow)
            {
                bool status = false;
                status = metric.TrackValue(_random.Next(100), "Office", "new");
                if (status == false)
                {
                    _logger.LogError("exceeded");
                }
                status = metric.TrackValue(_random.Next(100), "Azure", "submit");

                if (status == false)
                {
                    _logger.LogError("exceeded");
                }
                await Task.Delay(1000);
            }
            return StatusCode(201, "LogMetric");
        }

        [HttpGet("TestRawMetric")]
        public async Task<ActionResult> TestRawMetric()
        {


            var dimensions = new Dictionary<string, string>();
            dimensions["OrderSubmitted"] = "Azure";
            dimensions["Region"] = "US";

            await DoWork();

            // anti pattern
            _telemetry.TrackMetric("order_submitted_count", 1, dimensions);
            return StatusCode(200, "TestLog");
        }

        private async Task DoWork()
        {
            await Task.Delay(100);
        }

        private async Task DoWorkWithLog()
        {
            _logger.LogInformation("Request received. DoWorkWithLog");

            await Task.Delay(100);
        }
    }
}
