using CervejariaGCS.Data.Mappings;
using CervejariaGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace CervejariaGCS.Data
{
    public class GCSDataContext : DbContext
    {

        /// <summary>
        /// Construtor para utilização de DbContextOptions em Program.cs e Testes.
        /// </summary>
        /// <param name="options"></param>
        public GCSDataContext(DbContextOptions<GCSDataContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cashback> Cashbacks { get; set; }

        // MOVIDO PARA PROGRAM.CS. MOTIVO: PERMITIR OPERAR COM DADOS EM MEMÓRIA NOS TESTES
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlServer("Server=localhost,1433;Database=CervejariaGCS;User ID=sa;Password=k@FGu@r5");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new OrderItemMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new CashbackMap());
        }
    }
}