using MediatR;
using MultiTenantSaaS.Application.DTOs;
using MultiTenantSaaS.Application.Services;
using MultiTenantSaaS.Domain.Interfaces;

namespace MultiTenantSaaS.Application.Commands.Auth
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResponse?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public LoginQueryHandler(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthenticationResponse?> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            var token = _jwtService.GenerateToken(user.UserId, user.Email, user.TenantId, user.Role);

            return new AuthenticationResponse
            {
                UserId = user.UserId,
                Email = user.Email,
                Token = token,
                TenantId = user.TenantId,
                Role = user.Role
            };
        }
    }
}