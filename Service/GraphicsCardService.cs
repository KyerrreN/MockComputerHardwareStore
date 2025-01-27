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

        public IEnumerable<GraphicsCardDto> GetAllGraphicsCards(bool trackChanges)
        {
            var graphicsCards = _repository.GraphicsCard.GetAllGraphicsCards(trackChanges);

            var graphicsCardsDto = _mapper.Map<IEnumerable<GraphicsCardDto>>(graphicsCards);

            return graphicsCardsDto;
        }

        public GraphicsCardDto GetGraphicsCard(Guid id, bool trackChanges)
        {
            var graphicsCard = _repository.GraphicsCard.GetGraphicsCard(id, trackChanges);

            if (graphicsCard is null)
            {
                throw new GraphicsCardNotFoundException(id);
            }

            var graphicsCardDto = _mapper.Map<GraphicsCardDto>(graphicsCard);
            return graphicsCardDto;
        }

        public GraphicsCardDto CreateGraphicsCard(GraphicsCardForCreationDto graphicsCard)
        {
            var graphicsCardEntity = _mapper.Map<GraphicsCard>(graphicsCard);

            _repository.GraphicsCard.CreateGraphicsCard(graphicsCardEntity);
            _repository.Save();

            var graphicsCardToReturn = _mapper.Map<GraphicsCardDto>(graphicsCardEntity);

            return graphicsCardToReturn;
        }

        public IEnumerable<GraphicsCardDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var graphicsCards = _repository.GraphicsCard.GetByIds(ids, trackChanges);

            if (ids.Count() != graphicsCards.Count())
                throw new CollectionByIdsBadRequestException();

            var graphicsCardsDto = _mapper.Map<IEnumerable<GraphicsCardDto>>(graphicsCards);

            return graphicsCardsDto;
        }

        public (IEnumerable<GraphicsCardDto> graphicsCards, string ids) CreateGraphicsCardCollection(IEnumerable<GraphicsCardForCreationDto> graphicsCardCollection)
        {
            if (graphicsCardCollection is null)
                throw new GraphicsCardCollectionBadRequest();

            var graphicsCardEntities = _mapper.Map<IEnumerable<GraphicsCard>>(graphicsCardCollection);
            foreach (var graphicsCard in graphicsCardEntities)
            {
                _repository.GraphicsCard.CreateGraphicsCard(graphicsCard);
            }
            _repository.Save();

            var graphicsCardCollectionToReturn = _mapper.Map<IEnumerable<GraphicsCardDto>>(graphicsCardEntities);
            var ids = string.Join(',', graphicsCardCollectionToReturn.Select(g => g.Id));

            return (graphicsCards: graphicsCardCollectionToReturn, ids: ids);
        }

        public void DeleteGraphicsCard(Guid graphicsCardId, bool trackChanges)
        {
            var graphicsCardEntity = _repository.GraphicsCard.GetGraphicsCard(graphicsCardId, trackChanges);
            if (graphicsCardEntity is null)
                throw new GraphicsCardNotFoundException(graphicsCardId);

            _repository.GraphicsCard.DeleteGraphicsCard(graphicsCardEntity);
            _repository.Save();
        }

        public void UpdateGraphicsCard(Guid graphicsCardId, GraphicsCardForUpdateDto graphicsCardForUpdate, bool trackChanges)
        {
            var graphicsCardEntity = _repository.GraphicsCard.GetGraphicsCard(graphicsCardId, trackChanges);
            if (graphicsCardEntity is null)
                throw new GraphicsCardNotFoundException(graphicsCardId);

            foreach (var benchmark in graphicsCardForUpdate.GraphicsCardBenchmarks)
            {
                var found = _repository.GraphicsCardBenchmark.GetBenchmark(graphicsCardId, benchmark.BenchmarkId, false);

                if (found is not null)
                    throw new GraphicsCardBenchmarkFoundException(graphicsCardId, benchmark.BenchmarkId);
            }

            _mapper.Map(graphicsCardForUpdate, graphicsCardEntity);
            _repository.Save();
        }
    }
}
