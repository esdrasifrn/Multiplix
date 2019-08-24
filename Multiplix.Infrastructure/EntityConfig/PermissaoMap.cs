using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multiplix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Infrastructure.EntityConfig
{
    public class PermissaoMap : IEntityTypeConfiguration<Permissao>
    {
        public void Configure(EntityTypeBuilder<Permissao> builder)
        {
            builder.HasKey(u => u.PermisaoId);
            builder
              .Property(u => u.PermisaoId)
              .UseSqlServerIdentityColumn();

            builder
                .Property(u => u.NomeId)
                .HasColumnType("varchar(250)");

            builder
                .Property(u => u.Descricao)
                .HasColumnType("varchar(250)");
        }
    }
}
