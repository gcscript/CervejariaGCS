namespace CervejariaGCS.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public short Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Cashback { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public Order Order { get; set; } // Pode (estar em)|(ter) um/uma [Order]
        public Product Product { get; set; } // Pode (estar em)|(ter) um/uma [Product]
    }
}
