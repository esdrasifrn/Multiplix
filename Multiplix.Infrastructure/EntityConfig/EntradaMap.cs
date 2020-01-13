using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multiplix.Domain.Entities;

namespace Multiplix.Infrastructure.EntityConfig
{
    public class EntradaMap : IEntityTypeConfiguration<Entrada>
    {
        public void Configure(EntityTypeBuilder<Entrada> builder)
        {
            builder.HasKey(c => c.EntradaId);
            builder
               .Property(c => c.EntradaId)
               .UseSqlServerIdentityColumn();

            builder
               .HasOne(c => c.Associado)
               .WithMany(a => a.Entradas);

            //builder
            //  .Property(e => e.Associado.Id)
            //  .IsRequired(false);
        }
           
    }
}
