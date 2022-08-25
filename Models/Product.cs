namespace CervejariaGCS.Models;

public class Product : BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Slug { get; set; }
    public decimal Price { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } // Pode (estar em)|(ter) vários/várias [OrderItem]
    public ICollection<Cashback> Cashbacks { get; set; } // Pode (estar em)|(ter) vários/várias [Cashback]
}
