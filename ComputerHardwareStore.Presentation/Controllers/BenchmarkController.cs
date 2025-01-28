﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ComputerHardwareStore.Presentation.Controllers
{
    [ApiController]
    [Route("api/benchmarks")]
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

        [HttpGet]
        public async Task<IActionResult> GetAllBenchmarks()
        {
            var benchmarks = await _service.BenchmarkService.GetAllBenchmarksAsync(trackChanges: false);

            return Ok(benchmarks);
        }

        [HttpGet("{id:int}", Name = "GetBenchmarkById")]
        public async Task<IActionResult> GetBenchmarkById(int id)
        {
            var benchmark = await _service.BenchmarkService.GetBenchmarkAsync(id, trackChanges: false);

            return Ok(benchmark);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBenchmark([FromBody] BenchmarkForCreationDto bechmark)
        {
            if (bechmark is null)
                return BadRequest("BenchmarkDto object is null");

            _postValidator.ValidateAndThrow(bechmark);

            var createdBenchmark = await _service.BenchmarkService.CreateBenchmarkAsync(bechmark);

            return CreatedAtRoute("GetBenchmarkById", new { id = createdBenchmark.Id }, createdBenchmark);
        }
    }
}
