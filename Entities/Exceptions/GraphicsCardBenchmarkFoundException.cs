using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class GraphicsCardBenchmarkFoundException : BadRequestException
    {
        public GraphicsCardBenchmarkFoundException(Guid gcId, int benchmarkId)
            : base ($"Benchmark with id: {benchmarkId} for graphics card with id: {gcId} already exists")
        {
            
        }
    }
}
