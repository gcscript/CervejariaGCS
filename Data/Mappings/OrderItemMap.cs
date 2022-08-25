using CervejariaGCS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CervejariaGCS.Data.Mappings
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity)
                .IsRequired()
                .HasColumnName("Quantity");

            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("SMALLMONEY");

            builder.Property(x => x.Cashback)
                .IsRequired()
                .HasColumnName("Cashback")
                .HasColumnType("SMALLMONEY");

            builder.HasIndex(x => new { x.OrderId, x.ProductId }, "IX_OrderItem_OrderId_ProductId").IsUnique();

            // RELACIONAMENTOS
            builder // Relacionamento 1 para N
                .HasOne(orderItem => orderItem.Order) // Uma [OrderItem] só pode (estar em)|(ter) um/uma [Order]
                .WithMany(order => order.OrderItem) // Uma [Order] pode (estar em)|(ter) vários/várias [OrderItem]
                .HasForeignKey(fk => fk.OrderId) // A chave estrangeira vai ser o [OrderId] na tabela [OrderItem]
                .HasConstraintName("FK_OrderItem_Order_OrderId")
                .OnDelete(DeleteBehavior.Cascade);

            builder // Relacionamento 1 para N
                .HasOne(orderItem => orderItem.Product) // Uma [OrderItem] só pode (estar em)|(ter) um/uma [Product]
                .WithMany(product => product.OrderItems) // Um [Product] pode (estar em)|(ter) vários/várias [OrderItem]
                .HasForeignKey(fk => fk.ProductId) // A chave estrangeira vai ser o [ProductId] na tabela [OrderItem]
                .HasConstraintName("FK_OrderItem_Product_ProductId");
        }
    }
}