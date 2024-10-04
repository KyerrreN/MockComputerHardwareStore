using Contracts;
using Service.Contracts;

namespace Service
{
    internal sealed class GraphicsCardService: IGraphicsCardService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public GraphicsCardService(IRepositoryManager repository,
                                   ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
    }
}
