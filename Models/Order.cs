namespace CervejariaGCS.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Subtotal { get; set; }
        public decimal Cashback { get; set; }
        public decimal Total { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public ICollection<OrderItem> OrderItem { get; set; } // Pode (estar em)|(ter) vários/várias [OrderItem]
    }
}
