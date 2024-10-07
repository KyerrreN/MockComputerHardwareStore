namespace Contracts
{
    public interface IRepositoryManager
    {
        IGraphicsCardRepository GraphicsCard { get; }
        IGraphicsCardBenchmarkRepository GraphicsCardBenchmark { get; }
        IBenchmarkRepository BenchmarkRepository { get; }
        void Save();
    }
}
