using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
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

        [HttpGet("{id:int}", Name = "GetGraphicsCardBenchmark")]
        public IActionResult GetGraphicsCardBenchmark(Guid graphicsCardId, int id)
        {
            var benchmark = _service.GraphicsCardBenchmarkService.GetBenchmark(graphicsCardId, id, trackChanges: false);

            return Ok(benchmark);
        }

        [HttpPost("{id:int}")]
        public IActionResult CreateGraphicsCardBenchmark(Guid graphicsCardId, int id, [FromBody] GraphicsCardBenchmarkForCreationDto graphicsCardBenchmark)
        {
            if (graphicsCardBenchmark is null)
                return BadRequest("GraphicsCardBenchmarkForCreationDto object is null");

            var graphicsCardBenchmarkToReturn = _service.GraphicsCardBenchmarkService.CreateGraphicsCardBenchmark(graphicsCardId,
                                                                                                         id,
                                                                                                         graphicsCardBenchmark,
                                                                                                         trackChanges: false);

            return CreatedAtRoute("GetGraphicsCardBenchmark",
                new { graphicsCardId = graphicsCardId, id = graphicsCardBenchmarkToReturn.Id },
                graphicsCardBenchmarkToReturn);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteGraphicsCardBenchmark(Guid graphicsCardId, int id)
        {
            _service.GraphicsCardBenchmarkService.DeleteBenchmarkForGraphicsCard(graphicsCardId, id, false);

            return NoContent();
        }
    }
}
