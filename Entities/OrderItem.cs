namespace ToyStoreWebAppMVC.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string ToyName { get; set; }
        public string ToyManufacturer { get; set; }
        public decimal ToyCost { get; set; }
        public string ToyColor { get; set; }
        public int OrderId { get; set; }
    }
}
