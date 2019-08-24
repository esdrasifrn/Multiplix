using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multiplix.Domain.Entities;

namespace Multiplix.Infrastructure.EntityConfig
{
    public class PatrocinadorMap : IEntityTypeConfiguration<Patrocinador>
    {
        public void Configure(EntityTypeBuilder<Patrocinador> builder)
        {
            builder.HasKey(pa => pa.Id);
            builder
               .Property(pa => pa.Id)
               .UseSqlServerIdentityColumn();

            builder
               .HasOne(pa => pa.Usuario);

            builder.HasMany(pa => pa.Patrocinados)
                .WithOne()//opcional ou anulável
                .HasForeignKey(pa => pa.PatrocinadorId);                

            builder
                .Property(pa => pa.PatrocinadorId)
                .IsRequired(false);           
        }
           
    }
}
