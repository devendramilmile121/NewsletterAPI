using FluentValidation;
using MediatR;
using ValidationException = Application.Common.Exceptions.ValidationException;

namespace Application.Common.Behaviour
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validatorResult = await Task.WhenAll(
                    _validators.Select(v =>
                        v.ValidateAsync(context, cancellationToken)));

                var failuers = validatorResult.Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors)
                    .ToList();

                if (failuers.Any())
                    throw new ValidationException(failuers);
            }

            return await next();
        }
    }
}
