using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multiplix.Domain.Entities;

namespace Multiplix.Infrastructure.EntityConfig
{
    public class BonusMap : IEntityTypeConfiguration<Bonus>
    {
        public void Configure(EntityTypeBuilder<Bonus> builder)
        {
            builder.HasKey(c => c.BonusId);
            builder
               .Property(c => c.BonusId)
               .UseSqlServerIdentityColumn();

            builder
               .HasOne(c => c.AssociadoDono)
               .WithMany(a => a.Bonus);

           
        }
           
    }
}
