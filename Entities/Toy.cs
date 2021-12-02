using System.ComponentModel.DataAnnotations;

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
        public OrderItem OrderItem { get; set; }
    }
}
