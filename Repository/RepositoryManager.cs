using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        // fields to work with
        private readonly RepositoryContext _context;
        private readonly Lazy<IGraphicsCardRepository> _graphicsCardRepository;
        private readonly Lazy<IGraphicsCardBenchmarkRepository> _graphicsCardBenchmarkRepository;
        private readonly Lazy<IBenchmarkRepository> _benchmarkRepository;

        // DI
        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _graphicsCardRepository = new Lazy<IGraphicsCardRepository>(() => new GraphicsCardRepository(context));
            _graphicsCardBenchmarkRepository = new Lazy<IGraphicsCardBenchmarkRepository>(() => new GraphicsCardBenchmarkRepository(context));
            _benchmarkRepository = new Lazy<IBenchmarkRepository>(() => new BenchmarkRepository(context));
        }

        // Properties to serve needed repositories
        public IGraphicsCardRepository GraphicsCard => _graphicsCardRepository.Value;
        public IGraphicsCardBenchmarkRepository GraphicsCardBenchmark => _graphicsCardBenchmarkRepository.Value;
        public IBenchmarkRepository Benchmark => _benchmarkRepository.Value;

        // Method to save changes
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
