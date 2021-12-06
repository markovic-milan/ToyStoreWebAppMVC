namespace ToyStoreWebAppMVC.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Toy Toy { get; set; }
        public int OrderId { get; set; }
    }
}
