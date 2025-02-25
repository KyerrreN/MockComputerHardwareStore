using FluentValidation;
using Shared.DataTransferObjects;

namespace ComputerHardwareStore.Presentation.Validators
{
    public class UserForRegistrationValidator : AbstractValidator<UserForRegistrationDto>
    {
        public UserForRegistrationValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be null or empty");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be null or empty");
        }
    }
}
