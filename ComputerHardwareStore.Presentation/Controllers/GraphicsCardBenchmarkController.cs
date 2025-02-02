using ComputerHardwareStore.Presentation.ActionFilters;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ComputerHardwareStore.Presentation.Controllers
{
    [Route("api/graphicscards/{graphicsCardId}/benchmarks")]
    [ApiController]
    public class GraphicsCardBenchmarkController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IValidator<GraphicsCardBenchmarkForCreationDto> _postValidator;
        private readonly IValidator<GraphicsCardBenchmarkForUpdateDto> _putValidator;
        public GraphicsCardBenchmarkController(IServiceManager service,
                                               IValidator<GraphicsCardBenchmarkForCreationDto> postValidator,
                                               IValidator<GraphicsCardBenchmarkForUpdateDto> putValidator)
        {
            _service = service;
            _postValidator = postValidator;
            _putValidator = putValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetGraphicsCardBenchmarks(Guid graphicsCardId)
        {
            var benchmarks = await _service.GraphicsCardBenchmarkService.GetBenchmarksAsync(graphicsCardId, trackChanges: false);

            return Ok(benchmarks);
        }

        [HttpGet("{id:int}", Name = "GetGraphicsCardBenchmark")]
        public async Task<IActionResult> GetGraphicsCardBenchmark(Guid graphicsCardId, int id)
        {
            var benchmark = await _service.GraphicsCardBenchmarkService.GetBenchmarkAsync(graphicsCardId, id, trackChanges: false);

            return Ok(benchmark);
        }

        [HttpPost("{id:int}")]
        [ServiceFilter(typeof(BindingValidationFilterAttribute))]
        public async Task<IActionResult> CreateGraphicsCardBenchmark(Guid graphicsCardId, int id, [FromBody] GraphicsCardBenchmarkForCreationDto graphicsCardBenchmark)
        {
            //if (graphicsCardBenchmark is null)
            //    return BadRequest("GraphicsCardBenchmarkForCreationDto object is null");

            _postValidator.ValidateAndThrow(graphicsCardBenchmark);

            var graphicsCardBenchmarkToReturn = await _service.GraphicsCardBenchmarkService.CreateGraphicsCardBenchmarkAsync
                                                                                                        (graphicsCardId,
                                                                                                         id,
                                                                                                         graphicsCardBenchmark,
                                                                                                         trackChanges: false);

            return CreatedAtRoute("GetGraphicsCardBenchmark",
                new { graphicsCardId = graphicsCardId, id = graphicsCardBenchmarkToReturn.Id },
                graphicsCardBenchmarkToReturn);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGraphicsCardBenchmark(Guid graphicsCardId, int id)
        {
            await _service.GraphicsCardBenchmarkService.DeleteBenchmarkForGraphicsCardAsync(graphicsCardId, id, false);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(BindingValidationFilterAttribute))]
        public async Task<IActionResult> UpdateGraphicsCardBenchmark(Guid graphicsCardId, int id, [FromBody] GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmark)
        {
            //if (graphicsCardBenchmark is null)
            //    return BadRequest("GraphicsCardBenchmarkForUpdateDto object is null");

            _putValidator.ValidateAndThrow(graphicsCardBenchmark);

            await _service.GraphicsCardBenchmarkService.UpdateGraphicsCardBenchmarkAsync(graphicsCardId,
                                                                                         id,
                                                                                         graphicsCardBenchmark,
                                                                                         gdTrackChanges: false,
                                                                                         bnTrackChanges: true);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateGraphicsCardBenchmark(Guid graphicsCardId, int id, [FromBody] JsonPatchDocument<GraphicsCardBenchmarkForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("PatchDoc object sent from a client is null");

            var result = await _service.GraphicsCardBenchmarkService.GetGraphicsCardBenchmarkForPatchAsync(graphicsCardId, id, false, true);

            patchDoc.ApplyTo(result.graphicsCardBenchmarkToPatch);

            await _service.GraphicsCardBenchmarkService.SaveChangesForPatchAsync(result.graphicsCardBenchmarkToPatch, result.graphicsCardBenchmarkEntity);

            return NoContent();
        }
    }
}
