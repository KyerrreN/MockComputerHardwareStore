using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IGraphicsCardRepository
    {
        IEnumerable<GraphicsCard> GetAllGraphicsCards(bool trackChanges);
        GraphicsCard GetGraphicsCard(Guid id, bool trackChanges);
        void CreateGraphicsCard(GraphicsCard graphicsCard);
        IEnumerable<GraphicsCard> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteGraphicsCard(GraphicsCard graphicsCard);
    }
}
