using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

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

        public async Task<IEnumerable<GraphicsCardBenchmarkDto>> GetBenchmarksAsync(Guid grapicsCardId, bool trackChanges)
        {
            var graphicsCard = await _repository.GraphicsCard.GetGraphicsCardAsync(grapicsCardId, trackChanges);

            if (graphicsCard is null)
            {
                throw new GraphicsCardNotFoundException(grapicsCardId);
            }

            var benchmarks = await _repository.GraphicsCardBenchmark.GetBenchmarksAsync(grapicsCardId, trackChanges);

            if (benchmarks is null || !benchmarks.Any())
            {
                throw new GraphicsCardBenchmarkNotFoundException(grapicsCardId);
            }

            var benchmarksDto = _mapper.Map<IEnumerable<GraphicsCardBenchmarkDto>>(benchmarks);

            return benchmarksDto;
        }
        public async Task<GraphicsCardBenchmarkDto> GetBenchmarkAsync(Guid graphicsCardId, int benchmarkId, bool trackChanges)
        {
            var graphicsCard = await _repository.GraphicsCard.GetGraphicsCardAsync(graphicsCardId, trackChanges);

            if (graphicsCard is null)
            {
                throw new GraphicsCardNotFoundException(graphicsCardId);
            }

            var benchmark = await _repository.GraphicsCardBenchmark.GetBenchmarkAsync(graphicsCardId, benchmarkId, trackChanges);

            if (benchmark is null)
            {
                throw new BenchmarkNotFoundException(benchmarkId);
            }

            var benchmarkDto = _mapper.Map<GraphicsCardBenchmarkDto>(benchmark);

            return benchmarkDto;
        }

        public async Task<GraphicsCardBenchmarkDto> CreateGraphicsCardBenchmarkAsync(Guid graphicsCardId, int benchmarkId, GraphicsCardBenchmarkForCreationDto graphicsCardBenchmark, bool trackChanges)
        {
            var graphicsCard = await _repository.GraphicsCard.GetGraphicsCardAsync(graphicsCardId, false);
            if (graphicsCard is null)
                throw new GraphicsCardNotFoundException(graphicsCardId);

            var benchmark = await _repository.Benchmark.GetBenchmarkAsync(benchmarkId, false);
            if (benchmark is null)
                throw new BenchmarkNotFoundException(benchmarkId);

            var graphicsCardBenchmarkInDB = await _repository.GraphicsCardBenchmark.GetBenchmarkAsync(graphicsCardId,
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
            await _repository.SaveAsync();

            // I AM NOT SURE IF ITS GOOD
            var graphicsCardBenchmarkCreated = await _repository.GraphicsCardBenchmark.GetBenchmarkAsync(graphicsCardId,
                                                                                                         benchmarkId,
                                                                                                         false);

            var graphicsCardBenchmarkToReturn = _mapper.Map<GraphicsCardBenchmarkDto>(graphicsCardBenchmarkCreated);

            return graphicsCardBenchmarkToReturn;
        }

        public async Task DeleteBenchmarkForGraphicsCardAsync(Guid graphicsCardId, int benchmarkId, bool trackChanges)
        {
            var graphicsCard = await _repository.GraphicsCard.GetGraphicsCardAsync(graphicsCardId, trackChanges);
            if (graphicsCard is null)
                throw new GraphicsCardNotFoundException(graphicsCardId);

            var benchmark = await _repository.Benchmark.GetBenchmarkAsync(benchmarkId, trackChanges);
            if (benchmark is null)
                throw new BenchmarkNotFoundException(benchmarkId);

            var graphicsCardBenchmark = await _repository.GraphicsCardBenchmark.GetBenchmarkAsync(graphicsCardId, benchmarkId, trackChanges);
            if (graphicsCardBenchmark is null)
                throw new GraphicsCardBenchmarkNotFoundException(graphicsCardId);

            _repository.GraphicsCardBenchmark.DeleteGraphicsCardBenchmark(graphicsCardBenchmark);
            await _repository.SaveAsync();
        }

        public async Task UpdateGraphicsCardBenchmarkAsync(Guid graphicsCardId, int benchmarkId, GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmark, bool gdTrackChanges, bool bnTrackChanges)
        {
            var graphicsCard = await _repository.GraphicsCard.GetGraphicsCardAsync(graphicsCardId, gdTrackChanges);
            if (graphicsCard is null)
                throw new GraphicsCardBenchmarkNotFoundException(graphicsCardId);

            var benchmark = await _repository.Benchmark.GetBenchmarkAsync(benchmarkId, false);
            if (benchmark is null)
                throw new BenchmarkNotFoundException(benchmarkId);

            var graphicsCardBenchmarkEntity = await _repository.GraphicsCardBenchmark.GetBenchmarkAsync(graphicsCardId, benchmarkId, bnTrackChanges);
            if (graphicsCardBenchmarkEntity is null)
                throw new GraphicsCardBenchmarkNotFoundException(graphicsCardId);

            _mapper.Map(graphicsCardBenchmark, graphicsCardBenchmarkEntity);
            await _repository.SaveAsync();
        }

        public async Task<(GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmarkToPatch, GraphicsCardBenchmark graphicsCardBenchmarkEntity)> GetGraphicsCardBenchmarkForPatchAsync(Guid graphicsCardId, int benchmarkId, bool gcTrackChanges, bool benchTrackChanges)
        {
            var graphicsCard = await _repository.GraphicsCard.GetGraphicsCardAsync(graphicsCardId, gcTrackChanges);
            if (graphicsCard is null)
                throw new GraphicsCardNotFoundException(graphicsCardId);

            var benchmark = await _repository.Benchmark.GetBenchmarkAsync(benchmarkId, benchTrackChanges);
            if (benchmark is null)
                throw new BenchmarkNotFoundException(benchmarkId);

            var graphicsCardBenchmarkEntity = await _repository.GraphicsCardBenchmark.GetBenchmarkAsync(graphicsCardId, benchmarkId, benchTrackChanges);
            if (graphicsCardBenchmarkEntity is null)
                throw new GraphicsCardBenchmarkNotFoundException(graphicsCardId);

            var graphicsCardBenchmarkToPatch = _mapper.Map<GraphicsCardBenchmarkForUpdateDto>(graphicsCardBenchmarkEntity);

            return (graphicsCardBenchmarkToPatch, graphicsCardBenchmarkEntity);
        }

        public async Task SaveChangesForPatchAsync(GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmarkToPatch, GraphicsCardBenchmark graphicsCardBenchmarkEntity)
        {
            _mapper.Map(graphicsCardBenchmarkToPatch, graphicsCardBenchmarkEntity);
            await _repository.SaveAsync();
        }
    }
}
