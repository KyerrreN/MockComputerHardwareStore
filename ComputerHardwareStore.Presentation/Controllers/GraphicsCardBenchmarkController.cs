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

        /// <summary>
        /// Returns a list of graphics card benchmarks for a graphics card with specified id
        /// </summary>
        /// <param name="graphicsCardId"></param>
        /// <param name="parameters"></param>
        /// <returns>A list of graphics card benchmarks</returns>
        /// <response code="200">Returns a list of graphics card benchmarks with HATEOAS</response>
        /// <response code="400">If one or more parameters is null</response>
        /// <response code="401">If no auth token provided</response>
        /// <response code="404">If graphics card with a specified id does not exist</response>
        [HttpGet]
        [HttpHead]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGraphicsCardBenchmarks(Guid graphicsCardId,
                                                                   [FromQuery] GraphicsCardBenchmarkParameters parameters)
        {
            var linkParams = new LinkParameters(parameters, HttpContext);

            var result = await _service.GraphicsCardBenchmarkService.GetBenchmarksAsync(graphicsCardId, linkParams, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.HasLinks ? Ok(result.linkResponse.LinkedEntities)
                                                : Ok(result.linkResponse.ShapedEntities);
        }

        /// <summary>
        /// Returns a graphics card benchmark item
        /// </summary>
        /// <param name="graphicsCardId">Id of a graphics card</param>
        /// <param name="id">Id of a benchmark</param>
        /// <returns>A graphics card benchmark item for a specified graphics card and benchmark</returns>
        /// <response code="200">Returns a graphics card benchmark</response>
        /// <response code="401">If no auth token is provided</response>
        /// <response code="404">If a graphics card or benchmark with specified id does not exist</response>
        [HttpGet("{id:guid}", Name = "GetGraphicsCardBenchmark")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGraphicsCardBenchmark(Guid graphicsCardId, Guid id)
        {
            var benchmark = await _service.GraphicsCardBenchmarkService.GetBenchmarkAsync(graphicsCardId, id, trackChanges: false);

            return Ok(benchmark);
        }

        /// <summary>
        /// Created a graphics card benchmark item
        /// </summary>
        /// <param name="graphicsCardId">Id for a graphics card to add benchmark results</param>
        /// <param name="id">Id of a benchmark</param>
        /// <param name="graphicsCardBenchmark">Benchmark results</param>
        /// <returns>A newly created graphics card benchmark for specified graphics card and bemchmark</returns>
        /// <response code="201">Returns a newly created graphics card benchmark item</response>
        /// <response code="400">If graphics card benchmark dto is null</response>
        /// <response code="401">If no auth token is provided, or user does not have sufficient rights</response>
        /// <response code="404">If a graphics card or benchmark with specified id does not exist</response>
        /// <response code="422">If graphics card benchmark data is invalid</response>
        [HttpPost("{id:guid}")]
        [ServiceFilter(typeof(BindingValidationFilterAttribute))]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
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

        /// <summary>
        /// Deletes a graphics card benchmark item
        /// </summary>
        /// <param name="graphicsCardId">Id of a graphics card</param>
        /// <param name="id">Id of a benchmark</param>
        /// <returns></returns>
        /// <response code="204">If graphics card benchmark item was deleted succesfully</response>
        /// <response code="401">If no auth token provided, or user does not have sufficient rights</response>
        /// <response code="404">If a graphics card or a benchmark with specified id does not exist</response>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteGraphicsCardBenchmark(Guid graphicsCardId, Guid id)
        {
            await _service.GraphicsCardBenchmarkService.DeleteBenchmarkForGraphicsCardAsync(graphicsCardId, id, false);

            return NoContent();
        }

        /// <summary>
        /// Updated a graphics card benchmark item
        /// </summary>
        /// <param name="graphicsCardId">Id of a graphics card</param>
        /// <param name="id">Id of a benchmark</param>
        /// <param name="graphicsCardBenchmark">Data to update a graphics card</param>
        /// <returns></returns>
        /// <response code="204">If a graphics card benchmark item is updated succefully</response>
        /// <response code="400">If graphics card benchmark data is null</response>
        /// <response code="401">If no auth token provided, or user does not have sufficient rights</response>
        /// <response code="404">If a graphics card of a benchmark with specified id does not exist</response>
        /// <response code="422">If graphics card benchmark data is invalid</response>
        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(BindingValidationFilterAttribute))]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
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
    }
}
