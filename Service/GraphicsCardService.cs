using AutoMapper;
using Contracts;
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

            //Check for null here

            var graphicsCardDto = _mapper.Map<GraphicsCardDto>(graphicsCard);
            return graphicsCardDto;
        }
    }
}
