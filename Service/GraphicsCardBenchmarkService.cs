using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Dynamic;

namespace Service
{
    public class GraphicsCardBenchmarkService : IGraphicsCardBenchmarkService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        //private readonly IDataShaper<GraphicsCardBenchmarkDto> _dataShaper;
        private readonly IGraphicsCardBenchmarkLinks _graphicsCardBenchmarkLinks;

        public GraphicsCardBenchmarkService(IRepositoryManager repository,
                                            ILoggerManager logger,
                                            IMapper mapper,
                                            IGraphicsCardBenchmarkLinks graphicsCardBenchmarkLinks)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            //_dataShaper = dataShaper;
            _graphicsCardBenchmarkLinks = graphicsCardBenchmarkLinks;
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetBenchmarksAsync(Guid grapicsCardId,
                                                                                    LinkParameters parameters,
                                                                                    bool trackChanges)
        {
            ValidateFilterParameters(parameters.GraphicsCardBenchmarkParameters);

            await CheckIfGraphicsCardExists(grapicsCardId, trackChanges);

            var benchmarksWithMetaData = await _repository.GraphicsCardBenchmark.GetBenchmarksAsync(grapicsCardId, parameters.GraphicsCardBenchmarkParameters, trackChanges);

            var benchmarksDto = _mapper.Map<IEnumerable<GraphicsCardBenchmarkDto>>(benchmarksWithMetaData);
            var links = _graphicsCardBenchmarkLinks.TryGenerateLinks(benchmarksDto, parameters.GraphicsCardBenchmarkParameters.Fields, grapicsCardId, parameters.Context);

            return (linkResponse: links, metaData: benchmarksWithMetaData.MetaData);
        }
        public async Task<GraphicsCardBenchmarkDto> GetBenchmarkAsync(Guid graphicsCardId, Guid benchmarkId, bool trackChanges)
        {
            await CheckIfGraphicsCardExists(graphicsCardId, trackChanges);

            var benchmark = await _repository.GraphicsCardBenchmark.GetBenchmarkAsync(graphicsCardId, benchmarkId, trackChanges);

            if (benchmark is null)
            {
                throw new BenchmarkNotFoundException(benchmarkId);
            }

            var benchmarkDto = _mapper.Map<GraphicsCardBenchmarkDto>(benchmark);

            return benchmarkDto;
        }

        public async Task<GraphicsCardBenchmarkDto> CreateGraphicsCardBenchmarkAsync(Guid graphicsCardId, Guid benchmarkId, GraphicsCardBenchmarkForCreationDto graphicsCardBenchmark, bool trackChanges)
        {
            await CheckIfGraphicsCardExists(graphicsCardId, trackChanges);

            await CheckIfBenchmarkExists(benchmarkId, trackChanges);

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

        public async Task DeleteBenchmarkForGraphicsCardAsync(Guid graphicsCardId, Guid benchmarkId, bool trackChanges)
        {
            await CheckIfGraphicsCardExists(graphicsCardId, trackChanges);

            await CheckIfBenchmarkExists(benchmarkId, trackChanges);

            var graphicsCardBenchmark = await GetGraphicsCardBenchmarkAndCheckIfExists(graphicsCardId, benchmarkId, trackChanges);

            _repository.GraphicsCardBenchmark.DeleteGraphicsCardBenchmark(graphicsCardBenchmark);
            await _repository.SaveAsync();
        }

        public async Task UpdateGraphicsCardBenchmarkAsync(Guid graphicsCardId, Guid benchmarkId, GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmark, bool gdTrackChanges, bool bnTrackChanges)
        {
            await CheckIfGraphicsCardExists(graphicsCardId, gdTrackChanges);

            await CheckIfBenchmarkExists(benchmarkId, gdTrackChanges);

            var graphicsCardBenchmarkEntity = await GetGraphicsCardBenchmarkAndCheckIfExists(graphicsCardId, benchmarkId, bnTrackChanges);

            _mapper.Map(graphicsCardBenchmark, graphicsCardBenchmarkEntity);
            await _repository.SaveAsync();
        }

        public async Task<(GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmarkToPatch, GraphicsCardBenchmark graphicsCardBenchmarkEntity)> GetGraphicsCardBenchmarkForPatchAsync(Guid graphicsCardId, Guid benchmarkId, bool gcTrackChanges, bool benchTrackChanges)
        {
            await CheckIfGraphicsCardExists(graphicsCardId, gcTrackChanges);

            await CheckIfBenchmarkExists(benchmarkId, benchTrackChanges);

            var graphicsCardBenchmarkEntity = await GetGraphicsCardBenchmarkAndCheckIfExists(graphicsCardId, benchmarkId, benchTrackChanges);

            var graphicsCardBenchmarkToPatch = _mapper.Map<GraphicsCardBenchmarkForUpdateDto>(graphicsCardBenchmarkEntity);

            return (graphicsCardBenchmarkToPatch, graphicsCardBenchmarkEntity);
        }

        public async Task SaveChangesForPatchAsync(GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmarkToPatch, GraphicsCardBenchmark graphicsCardBenchmarkEntity)
        {
            _mapper.Map(graphicsCardBenchmarkToPatch, graphicsCardBenchmarkEntity);
            await _repository.SaveAsync();
        }

        // PRIVATE METHODS
        private async Task CheckIfGraphicsCardExists(Guid graphicsCardId, bool trackChanges)
        {
            var graphicsCard = await _repository.GraphicsCard.GetGraphicsCardAsync(graphicsCardId, trackChanges);

            if (graphicsCard is null)
            {
                throw new GraphicsCardNotFoundException(graphicsCardId);
            }
        }
        private async Task CheckIfBenchmarkExists(Guid benchmarkId, bool trackChanges)
        {
            var benchmark = await _repository.Benchmark.GetBenchmarkAsync(benchmarkId, false);
            if (benchmark is null)
                throw new BenchmarkNotFoundException(benchmarkId);
        }
        private async Task<GraphicsCardBenchmark> GetGraphicsCardBenchmarkAndCheckIfExists(Guid graphicsCardId, Guid benchmarkId, bool trackChanges)
        {
            var graphicsCardBenchmark = await _repository.GraphicsCardBenchmark.GetBenchmarkAsync(graphicsCardId, benchmarkId, trackChanges);
            
            if (graphicsCardBenchmark is null)
                throw new GraphicsCardBenchmarkNotFoundException(graphicsCardId);

            return graphicsCardBenchmark;
        }
        private void ValidateFilterParameters(GraphicsCardBenchmarkParameters parameters)
        {
            if (parameters.MinFps < 0m)
                throw new MinFpsNegativeBadRequestException();

            if (parameters.MaxFps > 999.9m)
                throw new MaxFpsOverflowBadRequestException();

            if (!parameters.ValidFpsRange)
                throw new MaxFpsRangeBadRequestException();
        }
    }
}
