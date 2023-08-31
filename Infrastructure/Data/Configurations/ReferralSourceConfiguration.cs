using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ReferralSourceConfiguration : IEntityTypeConfiguration<ReferralSource>
    {
        public void Configure(EntityTypeBuilder<ReferralSource> builder)
        {
            // Name property length
            builder.Property(s => s.Name)
                .HasMaxLength(100);
        }
    }
}
