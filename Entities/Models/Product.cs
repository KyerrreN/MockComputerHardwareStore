using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    // Entity that describes a product
    public class Product
    {
        public Guid Id { get; set; } // PK

        [Required(ErrorMessage = "Manufacturer field is required")]
        [MaxLength(10)]
        public string? Manufacturer { get; set; }

        [Required(ErrorMessage = "Model field is required")]
        [MaxLength(30)]
        public string? Model { get; set; }

        [Required(ErrorMessage = "Price field is required")]
        // TO-DO: ADD PRECISION
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity field is required")]
        // TO-DO: deny negative values
        public int StockQuantity { get; set; }

        // RELATIONSHIP
        public Guid CategoryId { get; set; } // FK
        public Category Category { get; set; } = null!;
    }
}
