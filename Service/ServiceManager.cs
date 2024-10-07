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
        private readonly Lazy<IGraphicsCardService> _graphicsCardService;
        private readonly Lazy<IGraphicsCardBenchmarkService> _graphicsCardBenchmarkService;

        // ctor
        public ServiceManager(IRepositoryManager repositoryManager,
                              ILoggerManager loggerManager,
                              IMapper mapper)
        {
            _graphicsCardBenchmarkService = new Lazy<IGraphicsCardBenchmarkService>(() =>
                                                    new GraphicsCardBenchmarkService(repositoryManager, loggerManager, mapper));
            _graphicsCardService = new Lazy<IGraphicsCardService>(() => new GraphicsCardService(repositoryManager, loggerManager, mapper));
        }

        // Interface Implementation to serve necessary services on demand
        public IGraphicsCardService GraphicsCardService => _graphicsCardService.Value;
        public IGraphicsCardBenchmarkService GraphicsCardBenchmarkService => _graphicsCardBenchmarkService.Value;
    }
}
