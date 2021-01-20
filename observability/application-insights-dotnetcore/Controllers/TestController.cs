using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using application_insight_dotnetcore.Models;
using ObservabilityPlatform;

namespace application_insight_dotnetcore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TestController> _logger;
        private readonly HttpClient _client = new HttpClient();
        private readonly static Random _random = new Random(1000);

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        // public TestController(ILoggerFactory loggerFactory)
        // {
        //     _logger = loggerFactory.CreateLogger("TestCustomCategory");
        // }

        [HttpGet("TestLog")]
        public async Task<ActionResult> TestLog()
        {
            _logger.LogDebug("Request debug");
            _logger.LogTrace("Request Trace");
            _logger.LogInformation("Request received");
            _logger.LogWarning("Request Warning");
            _logger.LogError("Request Errored");
            await _client.GetAsync("https://eos-ple.azurewebsites.net/health/ping");
            await DoWork();
            if (_random.Next(100) % 2 == 0)
            {
                return StatusCode(200, "TestLog");
            }
            else
            {
                return StatusCode(201, "TestLog");
            }
        }


        [HttpGet("TestLogEvent")]
        public async Task<ActionResult> TestLogEvent()
        {
            // _logger.LogInformation(4000, "GET TestLogEvent");
            _logger.LogInformation(LogEvents.TestLogEvent, "GET TestLogEvent");

            // Log message template -  https://messagetemplates.org/
            // https://github.com/NLog/NLog/wiki/How-to-use-structured-logging
            _logger.LogInformation("RequestReceived {Id} at {RequestTime} with statusCode {statusCode}", Guid.NewGuid(), DateTime.UtcNow, 200);
            await DoWork();
            return StatusCode(200, "TestLog");
        }

        [HttpGet("TestLogException")]
        public async Task<ActionResult> TestLogException(int id)
        {
            try
            {
                if (id == 0)
                {
                    await DoWork();
                    throw new Exception("Test exception");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, ex, "TestExp({Id})", id);
                return NotFound();
            }
            return StatusCode(200, "TestLog");
        }

        [HttpGet("TestLogScope")]
        public async Task<ActionResult> TestLogScope()
        {
            var transactionId = Guid.NewGuid();
            _logger.LogInformation("Request received");

            // A scope can group a set of logical operations. 
            using (_logger.BeginScope(transactionId))
            {
                _logger.LogInformation(LogEvents.GetItem, "Getting item");
                var result = await _client.GetAsync("http://www.bing.com");
                _logger.LogInformation(LogEvents.GetItem, "Result item {statusCode}", result.StatusCode);
            }

            return StatusCode(200, "TestLog");
        }

        [HttpGet("TestLogScope1")]
        public async Task<ActionResult> TestLogScope1()
        {
            var transactionId = Guid.NewGuid();
            _logger.LogInformation("Request received");

            // A scope can group a set of logical operations. 
            using (_logger.BeginScope(transactionId))
            {
                _logger.LogInformation(LogEvents.GetItem, "Getting item");
                var result = await _client.GetAsync("http://www.bing.com");
                _logger.LogInformation(LogEvents.GetItem, "Result item {statusCode}", result.StatusCode);
                await DoWorkWithLog();
            }
            OpReporterProvider.Reporter.RecordSosEvent("test", "hello help me !!");
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
