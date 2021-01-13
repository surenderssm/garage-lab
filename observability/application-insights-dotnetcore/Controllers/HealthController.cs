using Microsoft.AspNetCore.Mvc;

namespace application_insight_dotnetcore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        public HealthController()
        {
        }

        [HttpGet("Ping")]
        public ActionResult Ping()
        {
            return StatusCode(200, "Pong");
        }
    }
}
