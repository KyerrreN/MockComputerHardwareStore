using FluentValidation;
using Shared.DataTransferObjects;

namespace ComputerHardwareStore.Presentation.Validators
{
    public class UserForAuthenticationValidator : AbstractValidator<UserForAuthenticationDto>
    {
        public UserForAuthenticationValidator()
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
