using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IBenchmarkService
    {
        Task<IEnumerable<BenchmarkDto>> GetAllBenchmarksAsync(bool trackChanges);
        Task<BenchmarkDto> GetBenchmarkAsync(int id, bool trackChanges);
        Task<BenchmarkDto> CreateBenchmarkAsync(BenchmarkForCreationDto benchmark);
    }
}
