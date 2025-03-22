using MediatR;
using MultiTenantSaaS.Application.DTOs;

namespace MultiTenantSaaS.Application.Commands.Auth
{
    public class RegisterUserCommand : IRequest<AuthenticationResponse>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public Guid TenantId { get; set; }
    }
}