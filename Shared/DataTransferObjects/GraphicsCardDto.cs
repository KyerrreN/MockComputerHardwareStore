using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record GraphicsCardDto
    {
        public Guid Id { get; init; }
        public string FullName { get; init; }
        public int StockQuantity { get; init; }
        public bool IsSupportRtx { get; init; }
    };
}
