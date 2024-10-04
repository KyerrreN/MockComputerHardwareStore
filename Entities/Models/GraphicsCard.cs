using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    // Entity, that contains information about
    // graphics cards
    public class GraphicsCard : Product
    {
        public Guid Id { get; set; } // FK

        [Required(ErrorMessage = "Distributor field is required")]
        [MaxLength(20)]
        public string? Distributor { get; set; }

        [Required(ErrorMessage = "Base clock speed is required")]
        [MaxLength(9)]
        [RegularExpression(@"^[1-9][0-9]{0,8}$",
                           ErrorMessage = "Only a digit starting with a 1-9 character allowed")]
        public string? BaseClockSpeed { get; set; }

        [Required(ErrorMessage = "Max clock speed is required")]
        [MaxLength(9)]
        [RegularExpression(@"^[1-9][0-9]{0,8}$",
                           ErrorMessage = "Only a digit starting with a 1-9 character allowed")]
        public string? MaxClockSpeed { get; set; }

        [Required(ErrorMessage = "Memory clock speed is required")]
        [MaxLength(9)]
        [RegularExpression(@"^[1-9][0-9]{0,8}$",
                           ErrorMessage = "Only a digit starting with a 1-9 character allowed")]
        public string? MemoryClockSpeed { get; set; }

        [Required(ErrorMessage = "Connector pin field is required")]
        public byte ConnectorPins { get; set; }

        [Required(ErrorMessage = "RTX support field is required")]
        public bool IsSupportRtx { get; set; }

    }
}
