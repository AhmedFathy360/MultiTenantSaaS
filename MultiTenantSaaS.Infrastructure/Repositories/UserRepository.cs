using Microsoft.EntityFrameworkCore;
using MultiTenantSaaS.Domain.Entities;
using MultiTenantSaaS.Domain.Interfaces;
using MultiTenantSaaS.Infrastructure.Data;

namespace MultiTenantSaaS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MultiTenantDbContext _dbContext;

        public UserRepository(MultiTenantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _dbContext.Users
                .Include(u => u.Tenant)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users
                .Include(u => u.Tenant)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetByTenantAsync(Guid tenantId, int page = 1, int pageSize = 10)
        {
            return await _dbContext.Users
                .Where(u => u.TenantId == tenantId)
                .OrderBy(u => u.Email)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountByTenantAsync(Guid tenantId)
        {
            return await _dbContext.Users
                .Where(u => u.TenantId == tenantId)
                .CountAsync();
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid userId)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}