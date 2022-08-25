using CervejariaGCS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CervejariaGCS.Data.Mappings
{
    public class CashbackMap : IEntityTypeConfiguration<Cashback>
    {
        public void Configure(EntityTypeBuilder<Cashback> builder)
        {
            builder.ToTable("Cashback");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.DayOfWeek)
                .IsRequired()
                .HasColumnName("DayOfWeek");

            builder.Property(x => x.Percent)
                .IsRequired()
                .HasColumnName("PercentCashback");

            builder.HasIndex(x => new { x.ProductId, x.DayOfWeek }, "IX_Cashback_ProductId_DayOfWeek").IsUnique();

            // RELACIONAMENTOS
            builder // Relacionamento 1 para N
                .HasOne(cashback => cashback.Product) // Um [Cashback] só pode (estar em)|(ter) um/uma [Product]
                .WithMany(product => product.Cashbacks) // Um [Product] pode (estar em)|(ter) vários/várias [Cashback]
                .HasForeignKey(fk => fk.ProductId) // A chave estrangeira vai ser o [ProductId] na tabela [Cashback]
                .HasConstraintName("FK_Cashback_Product_ProductId");

            builder.HasData(
                new Cashback { Id = 1, ProductId = Guid.Parse("46F350BC-E5D8-4EDD-8599-5C589D000420"), DayOfWeek = 0, Percent = 40, }, // BOHEMIA
                new Cashback { Id = 2, ProductId = Guid.Parse("46F350BC-E5D8-4EDD-8599-5C589D000420"), DayOfWeek = 1, Percent = 10, },
                new Cashback { Id = 3, ProductId = Guid.Parse("46F350BC-E5D8-4EDD-8599-5C589D000420"), DayOfWeek = 2, Percent = 15, },
                new Cashback { Id = 4, ProductId = Guid.Parse("46F350BC-E5D8-4EDD-8599-5C589D000420"), DayOfWeek = 3, Percent = 15, },
                new Cashback { Id = 5, ProductId = Guid.Parse("46F350BC-E5D8-4EDD-8599-5C589D000420"), DayOfWeek = 4, Percent = 15, },
                new Cashback { Id = 6, ProductId = Guid.Parse("46F350BC-E5D8-4EDD-8599-5C589D000420"), DayOfWeek = 5, Percent = 20, },
                new Cashback { Id = 7, ProductId = Guid.Parse("46F350BC-E5D8-4EDD-8599-5C589D000420"), DayOfWeek = 6, Percent = 40, },

                new Cashback { Id = 8, ProductId = Guid.Parse("F2A0F81D-FDC4-4BEC-83F4-A54E5B43E903"), DayOfWeek = 0, Percent = 30, }, // BRAHMA
                new Cashback { Id = 9, ProductId = Guid.Parse("F2A0F81D-FDC4-4BEC-83F4-A54E5B43E903"), DayOfWeek = 1, Percent = 5, },
                new Cashback { Id = 10, ProductId = Guid.Parse("F2A0F81D-FDC4-4BEC-83F4-A54E5B43E903"), DayOfWeek = 2, Percent = 10, },
                new Cashback { Id = 11, ProductId = Guid.Parse("F2A0F81D-FDC4-4BEC-83F4-A54E5B43E903"), DayOfWeek = 3, Percent = 15, },
                new Cashback { Id = 12, ProductId = Guid.Parse("F2A0F81D-FDC4-4BEC-83F4-A54E5B43E903"), DayOfWeek = 4, Percent = 20, },
                new Cashback { Id = 13, ProductId = Guid.Parse("F2A0F81D-FDC4-4BEC-83F4-A54E5B43E903"), DayOfWeek = 5, Percent = 25, },
                new Cashback { Id = 14, ProductId = Guid.Parse("F2A0F81D-FDC4-4BEC-83F4-A54E5B43E903"), DayOfWeek = 6, Percent = 30, },

                new Cashback { Id = 15, ProductId = Guid.Parse("949FA947-40B4-4DF6-B4A0-BDDF71D44C77"), DayOfWeek = 0, Percent = 25, }, // SKOL
                new Cashback { Id = 16, ProductId = Guid.Parse("949FA947-40B4-4DF6-B4A0-BDDF71D44C77"), DayOfWeek = 1, Percent = 7, },
                new Cashback { Id = 17, ProductId = Guid.Parse("949FA947-40B4-4DF6-B4A0-BDDF71D44C77"), DayOfWeek = 2, Percent = 6, },
                new Cashback { Id = 18, ProductId = Guid.Parse("949FA947-40B4-4DF6-B4A0-BDDF71D44C77"), DayOfWeek = 3, Percent = 2, },
                new Cashback { Id = 19, ProductId = Guid.Parse("949FA947-40B4-4DF6-B4A0-BDDF71D44C77"), DayOfWeek = 4, Percent = 10, },
                new Cashback { Id = 20, ProductId = Guid.Parse("949FA947-40B4-4DF6-B4A0-BDDF71D44C77"), DayOfWeek = 5, Percent = 15, },
                new Cashback { Id = 21, ProductId = Guid.Parse("949FA947-40B4-4DF6-B4A0-BDDF71D44C77"), DayOfWeek = 6, Percent = 20, },

                new Cashback { Id = 22, ProductId = Guid.Parse("B2E83440-69C7-4168-ADCD-226F100FB2C6"), DayOfWeek = 0, Percent = 35, }, // STELLA
                new Cashback { Id = 23, ProductId = Guid.Parse("B2E83440-69C7-4168-ADCD-226F100FB2C6"), DayOfWeek = 1, Percent = 3, },
                new Cashback { Id = 24, ProductId = Guid.Parse("B2E83440-69C7-4168-ADCD-226F100FB2C6"), DayOfWeek = 2, Percent = 5, },
                new Cashback { Id = 25, ProductId = Guid.Parse("B2E83440-69C7-4168-ADCD-226F100FB2C6"), DayOfWeek = 3, Percent = 8, },
                new Cashback { Id = 26, ProductId = Guid.Parse("B2E83440-69C7-4168-ADCD-226F100FB2C6"), DayOfWeek = 4, Percent = 13, },
                new Cashback { Id = 27, ProductId = Guid.Parse("B2E83440-69C7-4168-ADCD-226F100FB2C6"), DayOfWeek = 5, Percent = 18, },
                new Cashback { Id = 28, ProductId = Guid.Parse("B2E83440-69C7-4168-ADCD-226F100FB2C6"), DayOfWeek = 6, Percent = 25, }
                );
        }
    }
}