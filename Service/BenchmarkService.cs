using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public class BenchmarkService : IBenchmarkService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public BenchmarkService(IMapper mapper,
                                IRepositoryManager repository,
                                ILoggerManager logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<BenchmarkDto> CreateBenchmarkAsync(BenchmarkForCreationDto benchmark)
        {
            var benchmarkEntity = _mapper.Map<Benchmark>(benchmark);

            _repository.Benchmark.CreateBenchmark(benchmarkEntity);
            await _repository.SaveAsync();

            var benchmarkToReturn = _mapper.Map<BenchmarkDto>(benchmarkEntity);

            return benchmarkToReturn;
        }

        public async Task<IEnumerable<BenchmarkDto>> GetAllBenchmarksAsync(bool trackChanges)
        {
            var benchmarks = await _repository.Benchmark.GetBenchmarksAsync(trackChanges);

            var benchmarksDto = _mapper.Map<IEnumerable<BenchmarkDto>>(benchmarks);

            return benchmarksDto;
        }

        public async Task<BenchmarkDto> GetBenchmarkAsync(int id, bool trackChanges)
        {
            var benchmark = await _repository.Benchmark.GetBenchmarkAsync(id, trackChanges);

            if (benchmark is null)
                throw new BenchmarkNotFoundException(id);

            var benchmarkDto = _mapper.Map<BenchmarkDto>(benchmark);

            return benchmarkDto;
        }
    }
}
