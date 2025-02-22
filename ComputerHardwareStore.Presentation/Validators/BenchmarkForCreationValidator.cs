using FluentValidation;
using Shared.DataTransferObjects;

namespace ComputerHardwareStore.Presentation.Validators
{
    public sealed class BenchmarkForCreationValidator : AbstractValidator<BenchmarkForCreationDto>
    {
        public BenchmarkForCreationValidator()
        {
            RuleFor(x => x.GameName)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty")
                .MaximumLength(64)
                .WithMessage("{PropertyName} must be {MaxLength} characters or fewer");

            RuleFor(x => x.Resolution)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null")
                .InclusiveBetween((byte)0, (byte)2)
                .WithMessage("{PropertyName} has to fall in range: [{From};{To}]");

            RuleFor(x => x.Settings)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null")
                .InclusiveBetween((byte)0, (byte)2)
                .WithMessage("{PropertyName} has to fall in range: [{From};{To}]");
        }
    }
}
