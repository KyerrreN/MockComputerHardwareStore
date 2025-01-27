using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IValidator<GraphicsCardBenchmarkForCreationDto> _postValidator;
        public GraphicsCardBenchmarkController(IServiceManager service,
                                               IValidator<GraphicsCardBenchmarkForCreationDto> postValidator)
        {
            _service = service;
            _postValidator = postValidator;
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

            _postValidator.ValidateAndThrow(graphicsCardBenchmark);

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

        [HttpPut("{id:int}")]
        public IActionResult UpdateGraphicsCardBenchmark(Guid graphicsCardId, int id, [FromBody] GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmark)
        {
            if (graphicsCardBenchmark is null)
                return BadRequest("GraphicsCardBenchmarkForUpdateDto object is null");

            _service.GraphicsCardBenchmarkService.UpdateGraphicsCardBenchmark(graphicsCardId,
                                                                              id,
                                                                              graphicsCardBenchmark,
                                                                              gdTrackChanges: false,
                                                                              bnTrackChanges: true);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateGraphicsCardBenchmark(Guid graphicsCardId, int id, [FromBody] JsonPatchDocument<GraphicsCardBenchmarkForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("PatchDoc object sent from a client is null");

            var result = _service.GraphicsCardBenchmarkService.GetGraphicsCardBenchmarkForPatch(graphicsCardId, id, false, true);

            patchDoc.ApplyTo(result.graphicsCardBenchmarkToPatch);

            _service.GraphicsCardBenchmarkService.SaveChangesForPatch(result.graphicsCardBenchmarkToPatch, result.graphicsCardBenchmarkEntity);

            return NoContent();
        }
    }
}
