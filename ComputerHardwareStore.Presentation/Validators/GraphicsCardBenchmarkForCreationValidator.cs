using FluentValidation;
using Shared.DataTransferObjects;

namespace ComputerHardwareStore.Presentation.Validators
{
    public sealed class GraphicsCardBenchmarkForCreationValidator : AbstractValidator<GraphicsCardBenchmarkForCreationDto>
    {
        public GraphicsCardBenchmarkForCreationValidator()
        {
            RuleFor(x => x.Fps)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null")
                .GreaterThanOrEqualTo(0m)
                .WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}")
                .LessThanOrEqualTo(1000m)
                .WithMessage("{PropertyName} must be less than or equal to {ComparisonValue}")
                .PrecisionScale(6, 2, false)
                .WithMessage("{PropertyName} cannot have more than 2 digits after a decimal point, and cannot exceed 6 digits");

            RuleFor(x => x.TestingTool)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be null or empty")
                .MaximumLength(100)
                .WithMessage("{PropertyName}'s name cannot be longer than 100 characters");
        }
    }
}
