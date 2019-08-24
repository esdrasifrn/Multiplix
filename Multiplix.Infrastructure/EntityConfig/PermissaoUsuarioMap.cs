using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multiplix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Infrastructure.EntityConfig
{
    public class PermissaoUsuarioMap : IEntityTypeConfiguration<PermissaoUsuario>
    {
        public void Configure(EntityTypeBuilder<PermissaoUsuario> builder)
        {
            builder
                .HasKey(ug => new { ug.PermissaoId, ug.UsuarioId });

            builder.HasOne(ug => ug.Permissao)
                .WithMany(u => u.PermissaoUsuarios)
                .HasForeignKey(ug => ug.PermissaoId);

            builder.HasOne(ug => ug.Usuario)
                .WithMany(g => g.PermissaoUsuarios)
                .HasForeignKey(ug => ug.UsuarioId);
        }
    }
}
