using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ComputerHardwareStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILoggerManager logger;

        public WeatherForecastController(ILoggerManager logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            logger.LogInfo("Here is info message from our weather controller");
            logger.LogDebug("Here is debug message from our weather controller");
            logger.LogWarn("Here is warning message from our weather controller");
            logger.LogError("Here is an error message from our weather controller");

            return ["some value", "another value"];
        }
    }
}
