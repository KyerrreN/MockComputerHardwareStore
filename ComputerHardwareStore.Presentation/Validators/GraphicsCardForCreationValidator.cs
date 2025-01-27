using FluentValidation;
using Shared.DataTransferObjects;

namespace ComputerHardwareStore.Presentation.Validators
{
    public sealed class GraphicsCardForCreationValidator : AbstractValidator<GraphicsCardForCreationDto>
    {
        public GraphicsCardForCreationValidator()
        {
            RuleFor(x => x.Distributor)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty")
                .MaximumLength(20)
                .WithMessage("{PropertyName} cannot be longer than 20 characters");

            RuleFor(x => x.Manufacturer)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty")
                .MaximumLength(10)
                .WithMessage("{PropertyName} cannot be longer than 20 characters");

            RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty")
                .MaximumLength(30)
                .WithMessage("{PropertyName} cannot be longer than 20 characters");

            RuleFor(x => x.BaseClockSpeed)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty")
                .MaximumLength(20)
                .WithMessage("{PropertyName} cannot be longer than 20 characters")
                .Matches(@"^[1-9][0-9]{0,8}$")
                .WithMessage("Only a digit starting with a 1-9 character allowed");

            RuleFor(x => x.MaxClockSpeed)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty")
                .MaximumLength(20)
                .WithMessage("{PropertyName} cannot be longer than 20 characters")
                .Matches(@"^[1-9][0-9]{0,8}$")
                .WithMessage("Only a digit starting with a 1-9 character allowed");

            RuleFor(x => x.MemoryClockSpeed)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty")
                .MaximumLength(20)
                .WithMessage("{PropertyName} cannot be longer than 20 characters")
                .Matches(@"^[1-9][0-9]{0,8}$")
                .WithMessage("Only a digit starting with a 1-9 character allowed");

            RuleFor(x => x.ConnectorPins)
                .NotNull()
                .WithMessage("{PropertyName} is required");

            RuleFor(x => x.IsSupportRtx)
                .NotNull()
                .WithMessage("{PropertyName} is required");

            RuleFor(x => x.Price)
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .PrecisionScale(10, 2, false)
                .WithMessage("{PropertyName} cannot have more than 2 digits after a decimal point")
                .InclusiveBetween(0m, 1_000_000m)
                .WithMessage("{PropertyName} cannot be less than {From} and greater than {To}");

            RuleFor(x => x.StockQuantity)
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} cannot be less than {ComparisonValue}");

            RuleForEach(x => x.GraphicsCardBenchmarks)
                .SetValidator(new GraphicsCardBenchmarkForCreationValidator());
        }
    }
}
