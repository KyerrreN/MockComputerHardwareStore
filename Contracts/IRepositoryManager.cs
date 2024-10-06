namespace Contracts
{
    public interface IRepositoryManager
    {
        ICategoryRepository Category { get; }
        IGraphicsCardRepository GraphicsCard { get; }
        IBenchmarkRepository Benchmark { get; }
        void Save();
    }
}
