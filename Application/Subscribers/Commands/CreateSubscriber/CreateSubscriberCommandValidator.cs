using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Subscribers.Commands.CreateSubscriber
{
    public class CreateSubscriberCommandValidator: AbstractValidator<CreateSubscriberCommand>
    {
        private readonly IAppDbContext _context;

        public CreateSubscriberCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters.")
                .MustAsync(BeUniqueEmail).WithMessage("You are already subscribed to newsletter.")
                .EmailAddress();
        }

        public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Subscribers.AllAsync(e => e.Email != email, cancellationToken);
        }
    }
}
