using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    // Entity, that contains information about
    // graphics cards
    public class GraphicsCard
    {
        public Guid Id { get; set; } // PK

        [Required(ErrorMessage = "Distributor field is required")]
        [MaxLength(20)]
        public string Distributor { get; set; }

        [Required(ErrorMessage = "Manufacturer field is required")]
        [MaxLength(10)]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Model field is required")]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required(ErrorMessage = "Base clock speed is required")]
        [MaxLength(9)]
        [RegularExpression(@"^[1-9][0-9]{0,8}$",
                           ErrorMessage = "Only a digit starting with a 1-9 character allowed")]
        public string BaseClockSpeed { get; set; }

        [Required(ErrorMessage = "Max clock speed is required")]
        [MaxLength(9)]
        [RegularExpression(@"^[1-9][0-9]{0,8}$",
                           ErrorMessage = "Only a digit starting with a 1-9 character allowed")]
        public string MaxClockSpeed { get; set; }

        [Required(ErrorMessage = "Memory clock speed is required")]
        [MaxLength(9)]
        [RegularExpression(@"^[1-9][0-9]{0,8}$",
                           ErrorMessage = "Only a digit starting with a 1-9 character allowed")]
        public string MemoryClockSpeed { get; set; }

        [Required(ErrorMessage = "Connector pin field is required")]
        public byte ConnectorPins { get; set; }

        [Required(ErrorMessage = "RTX support field is required")]
        public bool IsSupportRtx { get; set; }

        [Required(ErrorMessage = "Price field is required")]
        [Precision(10, 2)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity field is required")]
        // TO-DO: deny negative values
        public int StockQuantity { get; set; }

        // Relationship
        public ICollection<GraphicsCardBenchmark> GraphicsCardBenchmarks { get; } = [];
    }
}
