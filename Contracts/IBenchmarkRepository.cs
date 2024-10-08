﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBenchmarkRepository
    {
        IEnumerable<Benchmark> GetBenchmarks(bool trackChanges);
        Benchmark GetBenchmark(int benchmarkId, bool trackChanges);
        void CreateBenchmark(Benchmark benchmark);
    }
}
