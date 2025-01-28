namespace Contracts
{
    public interface IRepositoryManager
    {
        IGraphicsCardRepository GraphicsCard { get; }
        IGraphicsCardBenchmarkRepository GraphicsCardBenchmark { get; }
        IBenchmarkRepository Benchmark { get; }
        Task SaveAsync();
    }
}
