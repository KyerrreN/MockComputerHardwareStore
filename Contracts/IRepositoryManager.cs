namespace Contracts
{
    public interface IRepositoryManager
    {
        ICategoryRepository Category { get; }
        IGraphicsCardRepository GraphicsCard { get; }
        void Save();
    }
}
