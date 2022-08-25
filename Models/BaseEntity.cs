namespace CervejariaGCS.Models
{
    public class BaseEntity
    {
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
