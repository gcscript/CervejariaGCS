namespace CervejariaGCS.Models
{
    public class Cashback : BaseEntity
    {
        public int Id { get; set; }

        public Guid ProductId { get; set; }
        public short DayOfWeek { get; set; }
        public short Percent { get; set; }

        public Product Product { get; set; }
    }
}
