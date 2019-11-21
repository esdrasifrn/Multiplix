using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multiplix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Infrastructure.EntityConfig
{
    public class EstadoMap : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.HasKey(e => e.EstadoId);

            builder
               .Property(e => e.Sigla)
               .HasColumnType("varchar(2)");

            builder
              .Property(e => e.Nome)
              .HasColumnType("varchar(100)");
        }
    }    
}
