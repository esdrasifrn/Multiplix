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
                .Property(a => a.Rua).HasColumnName("Rua")
                .HasColumnType("varchar(200)");

            builder               
                .Property(a => a.Cidade).HasColumnName("Cidade")
                .HasColumnType("varchar(200)");

            builder                
                .Property(a => a.Estado).HasColumnName("Estado")
                .HasColumnType("varchar(2)"); ;

            builder               
                .Property(a => a.CEP).HasColumnName("CEP")
                .HasColumnType("varchar(15)");

            builder                
                .Property(a => a.Numero).HasColumnName("Numero")
                .HasColumnType("varchar(10)");

            builder
                .Property(a => a.HorarioFuncionamento).HasColumnName("HorarioFuncionamento")
                .HasColumnType("varchar(75)");
        }
           
    }
}
