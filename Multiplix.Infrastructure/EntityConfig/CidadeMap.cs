using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multiplix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Infrastructure.EntityConfig
{
    public class CidadeMap : IEntityTypeConfiguration<Cidade>
    {
        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            builder.HasKey(c => c.CidadeId);

            builder
                .HasOne(e => e.Estado)
                .WithMany(c => c.Cidades);           
        }
    }
}
