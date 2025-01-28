using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class GraphicsCardRepository: RepositoryBase<GraphicsCard>, IGraphicsCardRepository
    {
        public GraphicsCardRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<GraphicsCard>> GetAllGraphicsCardsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(g => g.Manufacturer)
                .ToListAsync();
        }

        public async Task<GraphicsCard> GetGraphicsCardAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(g => g.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
        }

        public void CreateGraphicsCard(GraphicsCard graphicsCard)
        {
            Create(graphicsCard);
        }

        public async Task<IEnumerable<GraphicsCard>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition(g => ids.Contains(g.Id), trackChanges)
                .ToListAsync();
        }

        public void DeleteGraphicsCard(GraphicsCard graphicsCard)
        {
            Delete(graphicsCard);
        }
    }
}
