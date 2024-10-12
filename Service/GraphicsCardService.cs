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
    }
}
