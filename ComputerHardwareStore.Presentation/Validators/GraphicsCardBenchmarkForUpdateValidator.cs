using FluentValidation;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerHardwareStore.Presentation.Validators
{
    public sealed class GraphicsCardBenchmarkForUpdateValidator : AbstractValidator<GraphicsCardBenchmarkForUpdateDto>
    {
        public GraphicsCardBenchmarkForUpdateValidator()
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
        }
    }
}
