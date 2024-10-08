﻿using Shared.DataTransferObjects;
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
    }
}
