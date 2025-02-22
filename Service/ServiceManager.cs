using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;
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
        private readonly Lazy<IBenchmarkService> _benchmarkService;

        // ctor
        public ServiceManager(IRepositoryManager repositoryManager,
                              ILoggerManager loggerManager,
                              IMapper mapper,
                              IGraphicsCardBenchmarkLinks graphicsCardBenchmarkLinks)
        {
            _graphicsCardBenchmarkService = new Lazy<IGraphicsCardBenchmarkService>(() =>
                                                    new GraphicsCardBenchmarkService(repositoryManager, loggerManager, mapper, graphicsCardBenchmarkLinks));
            _graphicsCardService = new Lazy<IGraphicsCardService>(() => new GraphicsCardService(repositoryManager, loggerManager, mapper));
            _benchmarkService = new Lazy<IBenchmarkService>(() => new BenchmarkService(mapper, repositoryManager, loggerManager));
        }

        // Interface Implementation to serve necessary services on demand
        public IGraphicsCardService GraphicsCardService => _graphicsCardService.Value;
        public IGraphicsCardBenchmarkService GraphicsCardBenchmarkService => _graphicsCardBenchmarkService.Value;
        public IBenchmarkService BenchmarkService => _benchmarkService.Value;
    }
}
