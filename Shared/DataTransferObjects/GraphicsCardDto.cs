using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record GraphicsCardDto(Guid Id, string FullName, int StockQuantity, bool IsSupportRtx);
}
