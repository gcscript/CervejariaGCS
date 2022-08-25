using CervejariaGCS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CervejariaGCS.Data.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Subtotal)
                .IsRequired()
                .HasColumnName("Subtotal")
                .HasColumnType("SMALLMONEY");

            builder.Property(x => x.Cashback)
                .IsRequired()
                .HasColumnName("Cashback")
                .HasColumnType("SMALLMONEY");

            builder.Property(x => x.Total)
                .IsRequired()
                .HasColumnName("Total")
                .HasColumnType("SMALLMONEY");
        }
    }
}