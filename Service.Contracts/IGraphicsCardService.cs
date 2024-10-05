﻿using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IGraphicsCardService
    {
        IEnumerable<GraphicsCardDto> GetAllGraphicsCards(bool trackChanges);
        GraphicsCardDto GetGraphicsCard(Guid id, bool trackChanges);
    }
}
