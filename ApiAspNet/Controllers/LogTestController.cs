using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogTestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            Log.Information("🔥 Ceci est un test manuel de log pour ELK !");
            return Ok("Log envoyé !");
        }
    }
}
