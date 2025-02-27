using ComputerHardwareStore.Presentation.ActionFilters;
using Entities.LinkModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Text.Json;

namespace ComputerHardwareStore.Presentation.Controllers
{
    [Route("api/graphicscards/{graphicsCardId}/benchmarks")]
    [ApiController]
    [Authorize]
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
        [HttpHead]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetGraphicsCardBenchmarks(Guid graphicsCardId,
                                                                   [FromQuery] GraphicsCardBenchmarkParameters parameters)
        {
            var linkParams = new LinkParameters(parameters, HttpContext);

            var result = await _service.GraphicsCardBenchmarkService.GetBenchmarksAsync(graphicsCardId, linkParams, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.HasLinks ? Ok(result.linkResponse.LinkedEntities)
                                                : Ok(result.linkResponse.ShapedEntities);
        }

        [HttpGet("{id:guid}", Name = "GetGraphicsCardBenchmark")]
        public async Task<IActionResult> GetGraphicsCardBenchmark(Guid graphicsCardId, Guid id)
        {
            var benchmark = await _service.GraphicsCardBenchmarkService.GetBenchmarkAsync(graphicsCardId, id, trackChanges: false);

            return Ok(benchmark);
        }

        [HttpPost("{id:guid}")]
        [ServiceFilter(typeof(BindingValidationFilterAttribute))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateGraphicsCardBenchmark(Guid graphicsCardId, Guid id, [FromBody] GraphicsCardBenchmarkForCreationDto graphicsCardBenchmark)
        {
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

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGraphicsCardBenchmark(Guid graphicsCardId, Guid id)
        {
            await _service.GraphicsCardBenchmarkService.DeleteBenchmarkForGraphicsCardAsync(graphicsCardId, id, false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(BindingValidationFilterAttribute))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGraphicsCardBenchmark(Guid graphicsCardId, Guid id, [FromBody] GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmark)
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

        [HttpPatch("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PartiallyUpdateGraphicsCardBenchmark(Guid graphicsCardId, Guid id, [FromBody] JsonPatchDocument<GraphicsCardBenchmarkForUpdateDto> patchDoc)
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
