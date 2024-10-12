using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IGraphicsCardService
    {
        IEnumerable<GraphicsCardDto> GetAllGraphicsCards(bool trackChanges);
        GraphicsCardDto GetGraphicsCard(Guid id, bool trackChanges);
        GraphicsCardDto CreateGraphicsCard(GraphicsCardForCreationDto graphicsCard);
        IEnumerable<GraphicsCardDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        (IEnumerable<GraphicsCardDto> graphicsCards, string ids) CreateGraphicsCardCollection(IEnumerable<GraphicsCardForCreationDto> graphicsCardCollection);
    }
}
