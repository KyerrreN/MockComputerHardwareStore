using AutoMapper;
using Contracts;
using Entities.Exceptions;
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
    }
}
