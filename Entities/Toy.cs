using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyStoreWebAppMVC.Entities
{
    public class Toy
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Color { get; set; }
        [Required]
        [StringLength(50)]
        public string Category { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        [StringLength(100)]
        public string ThumbnailImagePath { get; set; }
        [Required]
        [StringLength(100)]
        public string Manufacturer { get; set; }
        [Required]
        [Column(TypeName = "decimal(6,2)")]
        [Range(0D, 9999D)]
        public decimal Cost { get; set; }
        [Required]
        [Range(0, 10000)]
        public int Quantity { get; set; }
    }
}
