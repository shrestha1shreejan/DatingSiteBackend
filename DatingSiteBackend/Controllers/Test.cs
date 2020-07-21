using FileLogger.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingSiteBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Test : ControllerBase
    {
        private ILoggerManager _logger;

        public Test(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInfo("Information");
            _logger.LogError("Error");
            _logger.LogDebug("Debug");
            _logger.LogWarn("Warning");
            return Ok("working");
        }
    }
}
