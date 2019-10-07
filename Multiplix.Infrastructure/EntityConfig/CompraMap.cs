using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multiplix.Domain.Entities;

namespace Multiplix.Infrastructure.EntityConfig
{
    public class CompraMap : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.HasKey(c => c.CompraId);
            builder
               .Property(c => c.CompraId)
               .UseSqlServerIdentityColumn();

            builder
               .HasOne(c => c.Associado)
               .WithMany(a => a.Compras);

            builder
               .HasOne(c => c.Parceiro)
               .WithMany(p => p.Compras);

            builder
                .HasMany(c => c.CompraItems)
                .WithOne(ci => ci.Compra);
        }
           
    }
}
