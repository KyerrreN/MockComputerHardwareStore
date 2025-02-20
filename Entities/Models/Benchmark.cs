using Entities.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    // Entity, that represents predetermined benchmark
    // results of a hardware component
    // I created it because I need to work with child entities
    // In my next chapter of the book
    public class Benchmark
    {
        [Key]
        public Guid Id { get; set; } // PK

        [Required(ErrorMessage = "The name of the game is required")]
        [MaxLength(64)]
        public string GameName { get; set; }
        
        [Required(ErrorMessage = "Resolution is required")]
        public BenchmarkResolution Resolution { get; set; }

        [Required(ErrorMessage = "Settings are required")]
        public BenchmarkSettings Settings { get; set; }

        // Relationships
        public ICollection<GraphicsCardBenchmark> GraphicsCardBenchmarks { get; } = [];
    }
}
