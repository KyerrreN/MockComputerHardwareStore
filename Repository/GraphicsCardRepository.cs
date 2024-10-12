using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GraphicsCardRepository: RepositoryBase<GraphicsCard>, IGraphicsCardRepository
    {
        public GraphicsCardRepository(RepositoryContext context) : base(context)
        {
        }

        public IEnumerable<GraphicsCard> GetAllGraphicsCards(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(g => g.Manufacturer)
                .ToList();
        }

        public GraphicsCard GetGraphicsCard(Guid id, bool trackChanges)
        {
            return FindByCondition(g => g.Id.Equals(id), trackChanges)
                .SingleOrDefault();
        }

        public void CreateGraphicsCard(GraphicsCard graphicsCard)
        {
            Create(graphicsCard);
        }

        public IEnumerable<GraphicsCard> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            return FindByCondition(g => ids.Contains(g.Id), trackChanges)
                .ToList();
        }
    }
}
