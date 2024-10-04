using Contracts;
using Service.Contracts;

namespace Service
{
    internal sealed class CategoryService: ICategoryService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public CategoryService(IRepositoryManager repository,
                               ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
    }
}
