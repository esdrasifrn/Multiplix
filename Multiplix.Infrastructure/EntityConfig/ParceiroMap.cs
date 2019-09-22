using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multiplix.Domain.Entities;

namespace Multiplix.Infrastructure.EntityConfig
{
    public class ParceiroMap : IEntityTypeConfiguration<Parceiro>
    {
        public void Configure(EntityTypeBuilder<Parceiro> builder)
        {
            builder.HasKey(a => a.ParceiroId);
            builder
               .Property(a => a.ParceiroId)
               .UseSqlServerIdentityColumn();

            builder
               .HasOne(pa => pa.Usuario);

            builder
                .OwnsOne(a => a.Endereco);

            builder
                .OwnsOne(a => a.Endereco)
                .Property(a => a.Rua).HasColumnName("Rua")
                .HasColumnType("varchar(200)");

            builder
                .OwnsOne(a => a.Endereco)
                .Property(a => a.Cidade).HasColumnName("Cidade")
                .HasColumnType("varchar(200)");

            builder
                .OwnsOne(a => a.Endereco)
                .Property(a => a.Estado).HasColumnName("Estado")
                .HasColumnType("varchar(2)"); ;

            builder
                .OwnsOne(a => a.Endereco)
                .Property(a => a.CEP).HasColumnName("CEP")
                .HasColumnType("varchar(15)");

            builder
                .OwnsOne(a => a.Endereco)
                .Property(a => a.Numero).HasColumnName("Numero")
                .HasColumnType("varchar(10)");
        }
           
    }
}
