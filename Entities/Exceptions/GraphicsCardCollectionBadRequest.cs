﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class GraphicsCardCollectionBadRequest : BadRequestException
    {
        public GraphicsCardCollectionBadRequest()
            : base("Graphics card collection sent from a client is null")
        {            
        }
    }
}
