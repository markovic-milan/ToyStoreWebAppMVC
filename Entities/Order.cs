using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyStoreWebAppMVC.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal Total { get; set; }
        [ForeignKey("OrderId")]
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
