using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record GraphicsCardForCreationDto(string Distributor, string Manufacturer, string Model, string BaseClockSpeed, string MaxClockSpeed,
                                             string MemoryClockSpeed, byte ConnectorPins, bool IsSupportRtx, decimal Price, int StockQuantity,
                                             IEnumerable<GraphicsCardBenchmarkForCreationDto> GraphicsCardBenchmarks);
}
