namespace MultiTenantSaaS.Application.DTOs
{
    public class AuthenticationResponse
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;
        public Guid TenantId { get; set; }
        public string Role { get; set; } = default!;
    }
}