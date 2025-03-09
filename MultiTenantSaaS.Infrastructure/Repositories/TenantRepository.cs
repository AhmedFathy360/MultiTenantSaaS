using MultiTenantSaaS.Domain.Entities;
using MultiTenantSaaS.Domain.Interfaces;
using MultiTenantSaaS.Infrastructure.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Tenant? GetbyIdAync(Guid tenantId)
        {
            return _dbContext.Tenants.Find(tenantId);
            
        }
    }
}
