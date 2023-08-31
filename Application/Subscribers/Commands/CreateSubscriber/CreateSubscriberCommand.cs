using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Subscribers.Commands.CreateSubscriber
{
    public class CreateSubscriberCommand: IRequest<int>
    {
        public string Email { get; set; }
        public string? Reason { get; set; }
        public string? Other { get; set; }
        public int ReferralSourceId { get; set; }
    }

    public class CreateSubscriberCommandHandler : IRequestHandler<CreateSubscriberCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateSubscriberCommandHandler> _logger;
        public CreateSubscriberCommandHandler(IAppDbContext context, ILogger<CreateSubscriberCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> Handle(CreateSubscriberCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation(string.Format("Adding {0} to subscriber list", command.Email));
            var entity = new Subscriber();
            entity.Email = command.Email;
            entity.Reason = command.Reason;
            entity.Other = command.Other;
            entity.ReferralSourceId = command.ReferralSourceId;
            
            _context.Subscribers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(string.Format("Added {0} to subscriber list with Id:{1}", command.Email, entity.Id));
            return entity.Id;
        }
    }
}
