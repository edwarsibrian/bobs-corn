using FluentValidation;

namespace BobsCorn.Application.Corn.BuyCorn
{
    public sealed class BuyCornCommandValidator : AbstractValidator<BuyCornCommand>
    {
        public BuyCornCommandValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEmpty()
                .WithMessage("ClientId is required.")
                .MaximumLength(100)
                .WithMessage("ClientId cannot exceed 100 characters.");
        }
    }
}
