using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multiplix.Domain.Entities;

namespace Multiplix.Infrastructure.EntityConfig
{
    public class RamoAtividadeMap : IEntityTypeConfiguration<RamoAtividade>
    {
        public void Configure(EntityTypeBuilder<RamoAtividade> builder)
        {
            builder.HasKey(r => r.RamoAtividadeId);
            builder
               .Property(r => r.RamoAtividadeId)
               .UseSqlServerIdentityColumn();
           
            builder               
                .Property(r => r.Nome).HasColumnName("Nome")
                .HasColumnType("varchar(150)");            
        }
           
    }
}
