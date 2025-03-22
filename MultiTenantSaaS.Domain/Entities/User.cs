using MultiTenantSaaS.Domain.Base;

namespace MultiTenantSaaS.Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; } = default!;
        public string Role { get; set; } = "User"; // Default role
        public bool IsActive { get; set; } = true;

        public string FullName => $"{FirstName} {LastName}";
    }
}