using Microsoft.EntityFrameworkCore;
using MultiTenantSaaS.Domain.Entities;
using MultiTenantSaaS.Domain.Interfaces;
using MultiTenantSaaS.Infrastructure.Data;

namespace MultiTenantSaaS.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly MultiTenantDbContext _dbContext;
        public TenantRepository(MultiTenantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Tenant tenant)
        {
            await _dbContext.Tenants.AddAsync(tenant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Tenant?> GetbyIdAync(Guid tenantId)
        {
            return await _dbContext.Tenants.FindAsync(tenantId);
        }

        public async Task<IEnumerable<Tenant>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            return await _dbContext.Tenants
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task UpdateAsync(Tenant tenant)
        {
            _dbContext.Tenants.Update(tenant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid tenantId)
        {
            var tenant = await GetbyIdAync(tenantId);
            if (tenant != null)
            {
                _dbContext.Tenants.Remove(tenant);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _dbContext.Tenants.CountAsync();
        }
    }
}
