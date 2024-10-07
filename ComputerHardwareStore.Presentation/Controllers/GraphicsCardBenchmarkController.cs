using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerHardwareStore.Presentation.Controllers
{
    [Route("api/graphicscards/{graphicsCardId}/benchmarks")]
    [ApiController]
    public class GraphicsCardBenchmarkController : ControllerBase
    {
        private readonly IServiceManager _service;
        public GraphicsCardBenchmarkController(IServiceManager service)
        {
            _service = service; 
        }

        [HttpGet]
        public IActionResult GetGraphicsCardBenchmarks(Guid graphicsCardId)
        {
            var benchmarks = _service.GraphicsCardBenchmarkService.GetBenchmarks(graphicsCardId, trackChanges: false);

            return Ok(benchmarks);
        }
    }
}
