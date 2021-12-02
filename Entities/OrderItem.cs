using System.ComponentModel.DataAnnotations.Schema;

namespace ToyStoreWebAppMVC.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Count { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal ItemCost { get; set; }
        public int OrderId { get; set; }
        [NotMapped]
        public int ToyId { get; set; }
    }
}
