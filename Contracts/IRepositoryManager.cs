namespace Contracts
{
    public interface IRepositoryManager
    {
        IGraphicsCardRepository GraphicsCard { get; }
        void Save();
    }
}
