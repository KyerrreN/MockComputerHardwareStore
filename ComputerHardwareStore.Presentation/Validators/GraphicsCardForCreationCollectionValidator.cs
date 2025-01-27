using FluentValidation;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerHardwareStore.Presentation.Validators
{
    public sealed class GraphicsCardForCreationCollectionValidator : AbstractValidator<IEnumerable<GraphicsCardForCreationDto>>
    {
        public GraphicsCardForCreationCollectionValidator()
        {
            RuleFor(gc => gc)
                .NotEmpty()
                .WithMessage("Collection cannot be empty");

            RuleForEach(gc => gc)
                .SetValidator(new GraphicsCardForCreationValidator());
        }
    }
}
