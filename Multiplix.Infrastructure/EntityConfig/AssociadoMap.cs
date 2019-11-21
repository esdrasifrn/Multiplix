using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multiplix.Domain.Entities;

namespace Multiplix.Infrastructure.EntityConfig
{
    public class AssociadoMap : IEntityTypeConfiguration<Associado>
    {
        public void Configure(EntityTypeBuilder<Associado> builder)
        {
            builder.HasKey(pa => pa.Id);
            builder
               .Property(pa => pa.Id)
               .UseSqlServerIdentityColumn();

            builder
               .HasOne(pa => pa.Usuario);

            builder
               .HasOne(pa => pa.Banco);

            builder
                .HasOne(planoAssinatura => planoAssinatura.PlanoAssinatura)
                .WithMany(a => a.Associados);

            builder.HasMany(pa => pa.Patrocinados)
                .WithOne()//opcional ou anulável
                .HasForeignKey(pa => pa.PatrocinadorId);                

            builder
                .Property(pa => pa.PatrocinadorId)
                .IsRequired(false);

            builder
                .Property(ba => ba.BancoId)
                .IsRequired(false);
            
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
               .Property(u => u.Sexo)
               .HasColumnType("varchar(1)");

            builder
              .Property(u => u.CPF)
              .HasColumnType("varchar(25)");
        }
           
    }
}
