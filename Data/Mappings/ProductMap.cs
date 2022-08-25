using CervejariaGCS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CervejariaGCS.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("SMALLMONEY");

            builder.HasIndex(x => x.Slug, "IX_Product_Slug").IsUnique();

            builder.HasData(
                new Product { Id = Guid.Parse("42be5a6b-180a-49ca-be38-635bc84fcf0b"), Name = "Amstel", Slug = "amstel", Price = 4.09m, },
                new Product { Id = Guid.Parse("58ef01ce-1e7d-430b-af4f-b3540d64a5c9"), Name = "Antarctica", Slug = "antarctica", Price = 4.19m, },
                new Product { Id = Guid.Parse("46f350bc-e5d8-4edd-8599-5c589d000420"), Name = "Bohemia", Slug = "bohemia", Price = 4.29m, },
                new Product { Id = Guid.Parse("f2a0f81d-fdc4-4bec-83f4-a54e5b43e903"), Name = "Brahma", Slug = "brahma", Price = 4.39m, },
                new Product { Id = Guid.Parse("e0f61e13-1056-4f7c-90cf-d99fa3c648c9"), Name = "Budweiser", Slug = "budweiser", Price = 4.49m,},
                new Product { Id = Guid.Parse("ad745264-7179-4ded-aaba-586666f1d475"), Name = "Devassa", Slug = "devassa", Price = 4.59m, },
                new Product { Id = Guid.Parse("4f5549fc-7e29-41ea-9c72-cc8bd1b69872"), Name = "Eisenbahn", Slug = "eisenbahn", Price = 4.69m, },
                new Product { Id = Guid.Parse("f47a46a4-8447-4c8d-a61f-532f4f8d9407"), Name = "Heineken", Slug = "heineken", Price = 4.79m, },
                new Product { Id = Guid.Parse("f036f35b-bb93-4103-9d1a-e240efcce37b"), Name = "Itaipava", Slug = "itaipava", Price = 4.89m, },
                new Product { Id = Guid.Parse("46d59e70-4a10-4b7c-8362-394c623d6d26"), Name = "Original", Slug = "original", Price = 4.99m, },
                new Product { Id = Guid.Parse("c885a886-50a3-4a43-939e-a15743700801"), Name = "Schin", Slug = "schin", Price = 5.09m, },
                new Product { Id = Guid.Parse("949fa947-40b4-4df6-b4a0-bddf71d44c77"), Name = "Skol", Slug = "skol", Price = 5.19m, },
                new Product { Id = Guid.Parse("b2e83440-69c7-4168-adcd-226f100fb2c6"), Name = "Stella", Slug = "stella", Price = 5.29m, }
                );
        }
    }
}