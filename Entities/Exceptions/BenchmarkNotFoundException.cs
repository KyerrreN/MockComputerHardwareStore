using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class BenchmarkNotFoundException : NotFoundException
    {
        public BenchmarkNotFoundException(int benchmarkId)
            : base ($"Benchmark with id : {benchmarkId} doesn't exist in the database")
        {
            
        }
    }
}
