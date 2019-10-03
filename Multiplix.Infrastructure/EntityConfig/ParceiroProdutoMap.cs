using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multiplix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Infrastructure.EntityConfig
{
    public class ParceiroProdutoMap : IEntityTypeConfiguration<ParceiroProduto>
    {
        public void Configure(EntityTypeBuilder<ParceiroProduto> builder)
        {
            builder.HasKey(pp => new { pp.ParceiroId, pp.ProdutoId });

            builder.HasOne(ug => ug.Parceiro)
                .WithMany(u => u.ParceiroProdutos)
                .HasForeignKey(ug => ug.ParceiroId);

            builder.HasOne(ug => ug.Produto)
                .WithMany(g => g.ParceiroProdutos)
                .HasForeignKey(ug => ug.ProdutoId);
        }
    }
}
