using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multiplix.Domain.Entities;

namespace Multiplix.Infrastructure.EntityConfig
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.UsuarioId);
            builder
               .Property(u => u.UsuarioId)
               .UseSqlServerIdentityColumn();

            builder
                .Property(u => u.Login)
                .HasColumnType("varchar(55)");

            builder
                .Property(u => u.Senha)
                .HasColumnType("varchar(50)");

            builder
                .Property(u => u.Nome)
                .HasColumnType("varchar(100)");

            builder
                .Property(u => u.Celular)
                .HasColumnType("varchar(25)");

            builder
                .Property(u => u.Email)
                .HasColumnType("varchar(100)");
        }
    }
}
