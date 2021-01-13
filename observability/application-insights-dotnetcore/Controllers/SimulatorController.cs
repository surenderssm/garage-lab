using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;

namespace application_insight_dotnetcore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimulatorController : ControllerBase
    {
        private TelemetryClient _client;
        private static Random Random = new Random();
        private static string[] ValidStatus = new string[] { "200", "201", "401", "500", "503", "404", "400", "429" };
        public SimulatorController(TelemetryClient client)
        {
            _client = client;
        }

        [HttpGet("Ping")]
        public ActionResult Ping()
        {
            return StatusCode(200, "Pong");
        }

        [HttpGet("Agreement")]
        public async Task<ActionResult> Agreement()
        {
            var t1 = Simulate("agreement", "agreement_fetch");
            var t2 = Simulate("agreement", "agreement_ui");
            var t3 = Simulate("agreement", "agreement_amend");
            await Task.WhenAll(t1, t2, t3);
            return StatusCode(200, "Pong");
        }


        [HttpGet("Subscription")]
        public async Task<ActionResult> Subscription()
        {
            var t1 = Simulate("subscription", "subscription_offer");
            var t2 = Simulate("subscription", "subscription_expiry");
            var t3 = Simulate("subscription", "subscription_get");
            await Task.WhenAll(t1, t2, t3);
            return StatusCode(200, "Pong");
        }

        [HttpGet("Payment")]
        public async Task<ActionResult> Payment()
        {
            var t1 = Simulate("payment", "payment_instrument");
            var t2 = Simulate("payment", "payment_recon");
            var t3 = Simulate("payment", "payment_get");
            await Task.WhenAll(t1, t2, t3);
            return StatusCode(200, "Pong");
        }

        private async Task Simulate(string serviceLine, string serviceName)
        {

            var currentTime = DateTime.UtcNow;

            // run for 5 minutes
            while (currentTime.AddMinutes(10) > DateTime.UtcNow)
            {
                var dimensions = new Dictionary<string, string>();
                dimensions["service_line"] = serviceLine;
                dimensions["service_name"] = serviceName;
                dimensions["cloude_role_name"] = "azurecontainer";

                // just for demo, ingestion
                // Track Error
                _client.TrackMetric("errors_count", Random.Next(300), dimensions);


                // requests
                var status = ValidStatus[Random.Next(100) % ValidStatus.Length];

                var reqDimensions = new Dictionary<string, string>();
                reqDimensions["service_line"] = serviceLine;
                reqDimensions["service_name"] = serviceName;
                reqDimensions["cloude_role_name"] = "azurewebsite";
                reqDimensions["result_code"] = status;

                _client.TrackMetric("incoming_requests_count", Random.Next(100), reqDimensions);
                _client.TrackMetric("incoming_requests_duration_ms", Random.Next(3000), reqDimensions);

                await Task.Delay(100);
            }

        }
    }
}
