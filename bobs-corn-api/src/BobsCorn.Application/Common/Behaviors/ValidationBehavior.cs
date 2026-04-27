using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace BobsCorn.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationFailures = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var errors = validationFailures
                .Where(validationResult => !validationResult.IsValid)
                .SelectMany(result => result.Errors)
                .Select(failure => new ValidationFailure
                {
                    PropertyName = failure.PropertyName,
                    ErrorMessage = failure.ErrorMessage,
                    AttemptedValue = failure.AttemptedValue,
                    ErrorCode = failure.ErrorCode
                }).ToList();

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }

            return await next();
        }
    }
}
