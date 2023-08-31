using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class AppDbContext: DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Subscriber> Subscribers => Set<Subscriber>();
        public DbSet<ReferralSource> ReferralSources => Set<ReferralSource>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<Subscriber>().HasIndex(a => a.Email).IsUnique();

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "Admin";
                        entry.Entity.Created = DateTimeOffset.UtcNow;
                        entry.Entity.LastModifiedBy = "Admin";
                        entry.Entity.LastModified = DateTimeOffset.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "Admin";
                        entry.Entity.LastModified = DateTimeOffset.UtcNow;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
