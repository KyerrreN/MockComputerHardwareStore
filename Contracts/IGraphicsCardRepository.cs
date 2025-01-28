using Entities.Models;

namespace Contracts
{
    public interface IGraphicsCardRepository
    {
        Task<IEnumerable<GraphicsCard>> GetAllGraphicsCardsAsync(bool trackChanges);
        Task<GraphicsCard> GetGraphicsCardAsync(Guid id, bool trackChanges);
        void CreateGraphicsCard(GraphicsCard graphicsCard);
        Task<IEnumerable<GraphicsCard>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteGraphicsCard(GraphicsCard graphicsCard);
    }
}
