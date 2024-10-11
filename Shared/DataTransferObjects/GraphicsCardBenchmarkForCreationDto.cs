using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record GraphicsCardBenchmarkForCreationDto
    {
        public int BenchmarkId { get; set; }
        public decimal Fps { get; set; }
    }
}
