using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Subscriber> Subscribers { get; }
    DbSet<ReferralSource> ReferralSources { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
