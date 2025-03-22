using MediatR;
using MultiTenantSaaS.Application.DTOs;
using MultiTenantSaaS.Application.Services;
using MultiTenantSaaS.Domain.Entities;
using MultiTenantSaaS.Domain.Interfaces;

namespace MultiTenantSaaS.Application.Commands.Auth
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthenticationResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IJwtService _jwtService;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            ITenantRepository tenantRepository,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _tenantRepository = tenantRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthenticationResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // Verify tenant exists
            var tenant = await _tenantRepository.GetbyIdAync(request.TenantId);
            if (tenant == null)
                throw new KeyNotFoundException("Tenant not found");

            // Check if user already exists
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                throw new InvalidOperationException("User with this email already exists");

            // Create new user with hashed password
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                TenantId = request.TenantId,
                Role = "User" // Default role
            };

            await _userRepository.AddAsync(user);

            // Generate JWT token
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