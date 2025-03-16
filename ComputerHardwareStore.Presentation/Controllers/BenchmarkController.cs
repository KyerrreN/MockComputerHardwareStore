using ComputerHardwareStore.Presentation.ActionFilters;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ComputerHardwareStore.Presentation.Controllers
{
    [ApiController]
    [Route("api/benchmarks")]
    [Authorize(Roles = "Admin")]
    public class BenchmarkController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IValidator<BenchmarkForCreationDto> _postValidator;

        public BenchmarkController(IServiceManager service,
                                   IValidator<BenchmarkForCreationDto> postValidator)
        {
            _service = service;
            _postValidator = postValidator;
        }

        /// <summary>
        /// Gets all benchmark items
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns a list of all available benchmarks</response>
        /// <response code="401">If auth fails, or no suffucient rights</response>
        [HttpGet(Name = "GetAllBenchmarks")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetAllBenchmarks()
        {
            var benchmarks = await _service.BenchmarkService.GetAllBenchmarksAsync(trackChanges: false);

            return Ok(benchmarks);
        }

        /// <summary>
        /// Gets one benchmark item
        /// </summary>
        /// <param name="id">Id of a benchmark</param>
        /// <returns>Benchmark item</returns>
        /// <response code="200">Returns a benchmark item</response>
        /// <response code="401">If auth fails, or no suffucient rights</response>
        /// <response code="404">If benchmark with specified id does not exist</response>
        [HttpGet("{id:guid}", Name = "GetBenchmarkById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBenchmarkById(Guid id)
        {
            var benchmark = await _service.BenchmarkService.GetBenchmarkAsync(id, trackChanges: false);

            return Ok(benchmark);
        }

        /// <summary>
        /// Creates a benchmark item
        /// </summary>
        /// <param name="bechmark"></param>
        /// <returns>A newly created benchmark item</returns>
        /// <response code="201">Returns a newly created benchmark item</response>
        /// <response code="400">If benchmark dto is null</response>
        /// <response code="401">If auth fails, or no suffucient rights</response>
        /// <response code="422">If benchmark dto is invalid</response>
        [HttpPost(Name = "CreateBenchmark")]
        [ServiceFilter(typeof(BindingValidationFilterAttribute))]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> CreateBenchmark([FromBody] BenchmarkForCreationDto bechmark)
        {
            _postValidator.ValidateAndThrow(bechmark);

            var createdBenchmark = await _service.BenchmarkService.CreateBenchmarkAsync(bechmark);

            return CreatedAtRoute("GetBenchmarkById", new { id = createdBenchmark.Id }, createdBenchmark);
        }
    }
}
