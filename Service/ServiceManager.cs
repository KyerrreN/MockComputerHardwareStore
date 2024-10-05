using AutoMapper;
using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        // Lazy fields
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<IGraphicsCardService> _graphicsCardService;

        // ctor
        public ServiceManager(IRepositoryManager repositoryManager,
                              ILoggerManager loggerManager,
                              IMapper mapper)
        {
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager, loggerManager, mapper));
            _graphicsCardService = new Lazy<IGraphicsCardService>(() => new GraphicsCardService(repositoryManager, loggerManager, mapper));
        }

        // Interface Implementation to serve necessary services on demand
        public ICategoryService CategoryService => _categoryService.Value;
        public IGraphicsCardService GraphicsCardService => _graphicsCardService.Value;
    }
}
