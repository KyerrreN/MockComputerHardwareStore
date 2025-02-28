using AutoMapper;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        // Lazy fields
        private readonly Lazy<IGraphicsCardService> _graphicsCardService;
        private readonly Lazy<IGraphicsCardBenchmarkService> _graphicsCardBenchmarkService;
        private readonly Lazy<IBenchmarkService> _benchmarkService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        // ctor
        public ServiceManager(IRepositoryManager repositoryManager,
                              ILoggerManager loggerManager,
                              IMapper mapper,
                              IGraphicsCardBenchmarkLinks graphicsCardBenchmarkLinks,
                              UserManager<User> userManager,
                              IOptions<JwtConfiguration> configuration,
                              RoleManager<IdentityRole> roleManager)
        {
            _graphicsCardBenchmarkService = new Lazy<IGraphicsCardBenchmarkService>(() =>
                                                    new GraphicsCardBenchmarkService(repositoryManager, loggerManager, mapper, graphicsCardBenchmarkLinks));
            _graphicsCardService = new Lazy<IGraphicsCardService>(() => new GraphicsCardService(repositoryManager, loggerManager, mapper));
            _benchmarkService = new Lazy<IBenchmarkService>(() => new BenchmarkService(mapper, repositoryManager, loggerManager));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(loggerManager, userManager, configuration, mapper, roleManager));
        }

        // Interface Implementation to serve necessary services on demand
        public IGraphicsCardService GraphicsCardService => _graphicsCardService.Value;
        public IGraphicsCardBenchmarkService GraphicsCardBenchmarkService => _graphicsCardBenchmarkService.Value;
        public IBenchmarkService BenchmarkService => _benchmarkService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
