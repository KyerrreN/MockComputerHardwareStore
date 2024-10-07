using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record BenchmarkDto(int Id, string GraphicsCardName, string GameName, string Resolution, string Settings, decimal Fps);
}
