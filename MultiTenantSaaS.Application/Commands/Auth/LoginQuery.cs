using MediatR;
using MultiTenantSaaS.Application.DTOs;

namespace MultiTenantSaaS.Application.Commands.Auth
{
    public class LoginQuery : IRequest<AuthenticationResponse?>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}