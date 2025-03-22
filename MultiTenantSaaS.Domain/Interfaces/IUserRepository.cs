using MultiTenantSaaS.Domain.Entities;

namespace MultiTenantSaaS.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid userId);
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetByTenantAsync(Guid tenantId, int page = 1, int pageSize = 10);
        Task<int> GetTotalCountByTenantAsync(Guid tenantId);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid userId);
    }
}