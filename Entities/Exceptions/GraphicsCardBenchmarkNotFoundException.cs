using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class GraphicsCardBenchmarkNotFoundException : NotFoundException
    {
        public GraphicsCardBenchmarkNotFoundException(Guid graphicsCardId)
            : base($"The benchmarks for graphics card with id: {graphicsCardId} don't exist in the database")
        {
            
        }
    }
}
