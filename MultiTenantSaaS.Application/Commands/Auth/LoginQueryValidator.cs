using FluentValidation;

namespace MultiTenantSaaS.Application.Commands.Auth
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email address is required")
                .MaximumLength(256).WithMessage("Email cannot exceed 256 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}