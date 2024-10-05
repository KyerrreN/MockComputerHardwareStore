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
    }
}
