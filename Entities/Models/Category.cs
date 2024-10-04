using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    // Entity, contains a category of hardware
    public class Category
    {
        public Guid Id { get; set; } // PK

        [Required(ErrorMessage = "Category name is a required field")]
        [MaxLength(20)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Category description is a required field")]
        [MaxLength(255)]
        public string? Description { get; set; }

        // Collection Navigations
        public ICollection<Product> Products { get; } = []; // Collection navigation to Products
    }
}
