using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class GraphicsCardBenchmark
    {
        // Columns
        public Guid GraphicsCardId { get; set; }
        public Guid BenchmarkId { get; set; }

        [Required(ErrorMessage = "FPS of the benchmark is required")]
        [Precision(4,1)]
        public decimal Fps { get; set; }

        public string TestingTool { get; set; } = null!;

        // Relationships
        public GraphicsCard GraphicsCard { get; set; } = null!;
        public Benchmark Benchmark { get; set; } = null!;
    }
}
