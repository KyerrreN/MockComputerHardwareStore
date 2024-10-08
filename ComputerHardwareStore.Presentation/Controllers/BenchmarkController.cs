﻿using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerHardwareStore.Presentation.Controllers
{
    [ApiController]
    [Route("api/benchmarks")]
    public class BenchmarkController : ControllerBase
    {
        private readonly IServiceManager _service;

        public BenchmarkController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllBenchmarks()
        {
            var benchmarks = _service.BenchmarkService.GetAllBenchmarks(trackChanges: false);

            return Ok(benchmarks);
        }

        [HttpGet("{id:int}", Name = "GetBenchmarkById")]
        public IActionResult GetBenchmarkById(int id)
        {
            var benchmark = _service.BenchmarkService.GetBenchmark(id, trackChanges: false);

            return Ok(benchmark);
        }

        [HttpPost]
        public IActionResult CreateBenchmark([FromBody] BenchmarkForCreationDto bechmark)
        {
            if (bechmark is null)
                return BadRequest("BenchmarkDto object is null");

            var createdBenchmark = _service.BenchmarkService.CreateBenchmark(bechmark);

            return CreatedAtRoute("GetBenchmarkById", new { id = createdBenchmark.Id }, createdBenchmark);
        }
    }
}
