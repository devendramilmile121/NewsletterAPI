using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Subscribers.Queries.GetSubscribers
{
    public class GetSubscribersQuery: IRequest<SubscribersVM>
    {

    }

    public class GetSubscribersQueryHandler : IRequestHandler<GetSubscribersQuery, SubscribersVM>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<GetSubscribersQueryHandler> _logger;

        public GetSubscribersQueryHandler(IAppDbContext context, ILogger<GetSubscribersQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<SubscribersVM> Handle(GetSubscribersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(string.Format("Getting subscriber list"));
            SubscribersVM subscribers = new SubscribersVM
            {
                Sources = await _context.ReferralSources.Select(s => new ReferralSourceDTO { Id = s.Id, Name = s.Name }).ToListAsync(cancellationToken),
                Subscribers = await _context.Subscribers.Include(s => s.ReferralSource).Select(s => new SubscriberDTO
                {
                    Id = s.Id,
                    Email = s.Email,
                    Other = s.Other,
                    Reason = s.Reason,
                    ReferralSource = new ReferralSourceDTO
                    {
                        Id = s.ReferralSource.Id,
                        Name = s.ReferralSource.Name,
                    },
                    ReferralSourceId = s.ReferralSourceId,
                }).OrderByDescending(a => a.Id).ToListAsync(cancellationToken)
            };
            _logger.LogInformation(string.Format("Found {0} subscribers", subscribers.Subscribers.Count()));
            return subscribers;
        }
    }
}
