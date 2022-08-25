using CervejariaGCS.Data.Mappings;
using CervejariaGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace CervejariaGCS.Data
{
    public class GCSDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cashback> Cashbacks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost,1433;Database=CervejariaGCS;User ID=sa;Password=k@FGu@r5");

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseInMemoryDatabase("CervejariaGCS");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new OrderItemMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new CashbackMap());
        }
    }
}