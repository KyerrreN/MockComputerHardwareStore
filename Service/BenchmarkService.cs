﻿using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BenchmarkService : IBenchmarkService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public BenchmarkService(IMapper mapper,
                                IRepositoryManager repository,
                                ILoggerManager logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public BenchmarkDto CreateBenchmark(BenchmarkForCreationDto benchmark)
        {
            var benchmarkEntity = _mapper.Map<Benchmark>(benchmark);

            _repository.Benchmark.CreateBenchmark(benchmarkEntity);
            _repository.Save();

            var benchmarkToReturn = _mapper.Map<BenchmarkDto>(benchmarkEntity);

            return benchmarkToReturn;
        }

        public IEnumerable<BenchmarkDto> GetAllBenchmarks(bool trackChanges)
        {
            var benchmarks = _repository.Benchmark.GetBenchmarks(trackChanges);

            var benchmarksDto = _mapper.Map<IEnumerable<BenchmarkDto>>(benchmarks);

            return benchmarksDto;
        }

        public BenchmarkDto GetBenchmark(int id, bool trackChanges)
        {
            var benchmark = _repository.Benchmark.GetBenchmark(id, trackChanges);

            var benchmarkDto = _mapper.Map<BenchmarkDto>(benchmark);

            return benchmarkDto;
        }
    }
}