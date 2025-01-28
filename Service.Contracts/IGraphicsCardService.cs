using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IGraphicsCardService
    {
        Task<IEnumerable<GraphicsCardDto>> GetAllGraphicsCardsAsync (bool trackChanges);
        Task<GraphicsCardDto> GetGraphicsCardAsync(Guid id, bool trackChanges);
        Task<GraphicsCardDto> CreateGraphicsCardAsync(GraphicsCardForCreationDto graphicsCard);
        Task<IEnumerable<GraphicsCardDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        Task<(IEnumerable<GraphicsCardDto> graphicsCards, string ids)> CreateGraphicsCardCollectionAsync(IEnumerable<GraphicsCardForCreationDto> graphicsCardCollection);
        Task DeleteGraphicsCardAsync(Guid graphicsCardId, bool trackChanges);
        Task UpdateGraphicsCardAsync(Guid graphicsCardId, GraphicsCardForUpdateDto graphicsCardForUpdate, bool trackChanges);
    }
}
