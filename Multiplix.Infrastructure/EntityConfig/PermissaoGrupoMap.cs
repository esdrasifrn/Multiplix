using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multiplix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Infrastructure.EntityConfig
{
    public class PermissaoGrupoMap : IEntityTypeConfiguration<PermissaoGrupo>
    {
        public void Configure(EntityTypeBuilder<PermissaoGrupo> builder)
        {
            builder.HasKey(ug => new { ug.PermissaoId, ug.GrupoId });

            builder.HasOne(ug => ug.Permissao)
                .WithMany(u => u.PermissaoGrupos)
                .HasForeignKey(ug => ug.PermissaoId);

            builder.HasOne(ug => ug.Grupo)
                .WithMany(g => g.PermissaoGrupos)
                .HasForeignKey(ug => ug.GrupoId);
        }
    }
}
