using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class GraphicsCardService : IGraphicsCardService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public GraphicsCardService(IRepositoryManager repository,
                                   ILoggerManager logger,
                                   IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GraphicsCardDto>> GetAllGraphicsCardsAsync(bool trackChanges)
        {
            var graphicsCards = await _repository.GraphicsCard.GetAllGraphicsCardsAsync(trackChanges);

            var graphicsCardsDto = _mapper.Map<IEnumerable<GraphicsCardDto>>(graphicsCards);

            return graphicsCardsDto;
        }

        public async Task<GraphicsCardDto> GetGraphicsCardAsync(Guid id, bool trackChanges)
        {
            var graphicsCard = await _repository.GraphicsCard.GetGraphicsCardAsync(id, trackChanges);

            if (graphicsCard is null)
            {
                throw new GraphicsCardNotFoundException(id);
            }

            var graphicsCardDto = _mapper.Map<GraphicsCardDto>(graphicsCard);
            return graphicsCardDto;
        }

        public async Task<GraphicsCardDto> CreateGraphicsCardAsync(GraphicsCardForCreationDto graphicsCard)
        {
            var graphicsCardEntity = _mapper.Map<GraphicsCard>(graphicsCard);

            _repository.GraphicsCard.CreateGraphicsCard(graphicsCardEntity);
            await _repository.SaveAsync();

            var graphicsCardToReturn = _mapper.Map<GraphicsCardDto>(graphicsCardEntity);

            return graphicsCardToReturn;
        }

        public async Task<IEnumerable<GraphicsCardDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var graphicsCards = await _repository.GraphicsCard.GetByIdsAsync(ids, trackChanges);

            if (ids.Count() != graphicsCards.Count())
                throw new CollectionByIdsBadRequestException();

            var graphicsCardsDto = _mapper.Map<IEnumerable<GraphicsCardDto>>(graphicsCards);

            return graphicsCardsDto;
        }

        public async Task<(IEnumerable<GraphicsCardDto> graphicsCards, string ids)> CreateGraphicsCardCollectionAsync(IEnumerable<GraphicsCardForCreationDto> graphicsCardCollection)
        {
            if (graphicsCardCollection is null)
                throw new GraphicsCardCollectionBadRequest();

            var graphicsCardEntities = _mapper.Map<IEnumerable<GraphicsCard>>(graphicsCardCollection);
            foreach (var graphicsCard in graphicsCardEntities)
            {
                _repository.GraphicsCard.CreateGraphicsCard(graphicsCard);
            }
            await _repository.SaveAsync();

            var graphicsCardCollectionToReturn = _mapper.Map<IEnumerable<GraphicsCardDto>>(graphicsCardEntities);
            var ids = string.Join(',', graphicsCardCollectionToReturn.Select(g => g.Id));

            return (graphicsCards: graphicsCardCollectionToReturn, ids: ids);
        }

        public async Task DeleteGraphicsCardAsync(Guid graphicsCardId, bool trackChanges)
        {
            var graphicsCardEntity = await _repository.GraphicsCard.GetGraphicsCardAsync(graphicsCardId, trackChanges);
            if (graphicsCardEntity is null)
                throw new GraphicsCardNotFoundException(graphicsCardId);

            _repository.GraphicsCard.DeleteGraphicsCard(graphicsCardEntity);
            await _repository.SaveAsync();
        }

        public async Task UpdateGraphicsCardAsync(Guid graphicsCardId, GraphicsCardForUpdateDto graphicsCardForUpdate, bool trackChanges)
        {
            var graphicsCardEntity = await _repository.GraphicsCard.GetGraphicsCardAsync(graphicsCardId, trackChanges);
            if (graphicsCardEntity is null)
                throw new GraphicsCardNotFoundException(graphicsCardId);

            foreach (var benchmark in graphicsCardForUpdate.GraphicsCardBenchmarks)
            {
                var found = await _repository.GraphicsCardBenchmark.GetBenchmarkAsync(graphicsCardId, benchmark.BenchmarkId, false);

                if (found is not null)
                    throw new GraphicsCardBenchmarkFoundException(graphicsCardId, benchmark.BenchmarkId);
            }

            _mapper.Map(graphicsCardForUpdate, graphicsCardEntity);
            await _repository.SaveAsync();
        }
    }
}
