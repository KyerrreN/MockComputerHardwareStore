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
        }
    }
}
