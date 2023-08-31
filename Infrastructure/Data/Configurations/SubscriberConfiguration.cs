using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class SubscriberConfiguration : IEntityTypeConfiguration<Subscriber>
    {
        public void Configure(EntityTypeBuilder<Subscriber> builder)
        {
            //builder.HasKey(s => s.Id);

            // Email address validation and uniqueness
            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasAnnotation("RegularExpression", @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .HasAnnotation("MaxLength", 100);

            // Reason and other properties length
            builder.Property(s => s.Reason)
                .HasMaxLength(400);

            builder.Property(s => s.Other)
                .HasMaxLength(250);

            // Foreign key to ReferralSource
            builder.Property(s => s.ReferralSourceId)
                .IsRequired();

            // Relationship with ReferralSource
            builder.HasOne(s => s.ReferralSource)
                .WithMany()
                .HasForeignKey(s => s.ReferralSourceId);
        }
    }
}
