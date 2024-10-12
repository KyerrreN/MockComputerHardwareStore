using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class GraphicsCardBenchmarkService : IGraphicsCardBenchmarkService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public GraphicsCardBenchmarkService(IRepositoryManager repository,
                                            ILoggerManager logger,
                                            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<GraphicsCardBenchmarkDto> GetBenchmarks(Guid grapicsCardId, bool trackChanges)
        {
            var graphicsCard = _repository.GraphicsCard.GetGraphicsCard(grapicsCardId, trackChanges);

            if (graphicsCard is null)
            {
                throw new GraphicsCardNotFoundException(grapicsCardId);
            }

            var benchmarks = _repository.GraphicsCardBenchmark.GetBenchmarks(grapicsCardId, trackChanges);

            if (benchmarks is null || !benchmarks.Any())
            {
                throw new GraphicsCardBenchmarkNotFoundException(grapicsCardId);
            }

            var benchmarksDto = _mapper.Map<IEnumerable<GraphicsCardBenchmarkDto>>(benchmarks);

            return benchmarksDto;
        }
        public GraphicsCardBenchmarkDto GetBenchmark(Guid graphicsCardId, int benchmarkId, bool trackChanges)
        {
            var graphicsCard = _repository.GraphicsCard.GetGraphicsCard(graphicsCardId, trackChanges);

            if (graphicsCard is null)
            {
                throw new GraphicsCardNotFoundException(graphicsCardId);
            }

            var benchmark = _repository.GraphicsCardBenchmark.GetBenchmark(graphicsCardId, benchmarkId, trackChanges);

            if (benchmark is null)
            {
                throw new BenchmarkNotFoundException(benchmarkId);
            }

            var benchmarkDto = _mapper.Map<GraphicsCardBenchmarkDto>(benchmark);

            return benchmarkDto;
        }

        public GraphicsCardBenchmarkDto CreateGraphicsCardBenchmark(Guid graphicsCardId, int benchmarkId, GraphicsCardBenchmarkForCreationDto graphicsCardBenchmark, bool trackChanges)
        {
            var graphicsCard = _repository.GraphicsCard.GetGraphicsCard(graphicsCardId, false);
            if (graphicsCard is null)
                throw new GraphicsCardNotFoundException(graphicsCardId);

            var benchmark = _repository.Benchmark.GetBenchmark(benchmarkId, false);
            if (benchmark is null)
                throw new BenchmarkNotFoundException(benchmarkId);

            var graphicsCardBenchmarkInDB = _repository.GraphicsCardBenchmark.GetBenchmark(graphicsCardId,
                                                                                           benchmarkId,
                                                                                           false);
            if (graphicsCardBenchmarkInDB is not null)
                throw new GraphicsCardBenchmarkNotFoundException(graphicsCardId);

            // Map from DTO to entity
            var graphicsCardBenchmarkEntity = _mapper.Map<GraphicsCardBenchmark>(graphicsCardBenchmark);
            _repository.GraphicsCardBenchmark.CreateGraphicsCardBenchmark(
                                                                    graphicsCardId,
                                                                    benchmarkId,
                                                                    graphicsCardBenchmarkEntity);
            _repository.Save();

            // I AM NOT SURE IF ITS GOOD
            var graphicsCardBenchmarkCreated = _repository.GraphicsCardBenchmark.GetBenchmark(graphicsCardId,
                                                                                              benchmarkId,
                                                                                              false);

            var graphicsCardBenchmarkToReturn = _mapper.Map<GraphicsCardBenchmarkDto>(graphicsCardBenchmarkCreated);

            return graphicsCardBenchmarkToReturn;
        }

        public void DeleteBenchmarkForGraphicsCard(Guid graphicsCardId, int benchmarkId, bool trackChanges)
        {
            var graphicsCard = _repository.GraphicsCard.GetGraphicsCard(graphicsCardId, trackChanges);
            if (graphicsCard is null)
                throw new GraphicsCardNotFoundException(graphicsCardId);

            var benchmark = _repository.Benchmark.GetBenchmark(benchmarkId, trackChanges);
            if (benchmark is null)
                throw new BenchmarkNotFoundException(benchmarkId);

            var graphicsCardBenchmark = _repository.GraphicsCardBenchmark.GetBenchmark(graphicsCardId, benchmarkId, trackChanges);
            if (graphicsCardBenchmark is null)
                throw new GraphicsCardBenchmarkNotFoundException(graphicsCardId);

            _repository.GraphicsCardBenchmark.DeleteGraphicsCardBenchmark(graphicsCardBenchmark);
            _repository.Save();
        }

        public void UpdateGraphicsCardBenchmark(Guid graphicsCardId, int benchmarkId, GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmark, bool gdTrackChanges, bool bnTrackChanges)
        {
            var graphicsCard = _repository.GraphicsCard.GetGraphicsCard(graphicsCardId, gdTrackChanges);
            if (graphicsCard is null)
                throw new GraphicsCardBenchmarkNotFoundException(graphicsCardId);

            var benchmark = _repository.Benchmark.GetBenchmark(benchmarkId, false);
            if (benchmark is null)
                throw new BenchmarkNotFoundException(benchmarkId);

            var graphicsCardBenchmarkEntity = _repository.GraphicsCardBenchmark.GetBenchmark(graphicsCardId, benchmarkId, bnTrackChanges);
            if (graphicsCardBenchmarkEntity is null)
                throw new GraphicsCardBenchmarkNotFoundException(graphicsCardId);

            _mapper.Map(graphicsCardBenchmark, graphicsCardBenchmarkEntity);
            _repository.Save();
        }
    }
}
