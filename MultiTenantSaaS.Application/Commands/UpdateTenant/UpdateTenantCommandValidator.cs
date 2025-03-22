using FluentValidation;

namespace MultiTenantSaaS.Application.Commands.UpdateTenant
{
    public class UpdateTenantCommandValidator : AbstractValidator<UpdateTenantCommand>
    {
        public UpdateTenantCommandValidator()
        {
            RuleFor(x => x.TenantId)
                .NotEmpty()
                .WithMessage("Tenant ID is required");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tenant name is required")
                .MaximumLength(200)
                .WithMessage("Tenant name cannot exceed 200 characters");
        }
    }
}