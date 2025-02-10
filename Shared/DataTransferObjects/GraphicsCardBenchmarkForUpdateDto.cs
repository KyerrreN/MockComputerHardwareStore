using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record GraphicsCardBenchmarkForUpdateDto
    {
        public decimal Fps { get; set; }
        public string TestingTool { get; set; }
    }
}
