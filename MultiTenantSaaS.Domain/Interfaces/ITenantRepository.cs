using MultiTenantSaaS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSaaS.Domain.Interfaces
{
    public interface ITenantRepository
    {
        Task AddAsync(Tenant tenant);
        Task<Tenant?> GetbyIdAync(Guid tenantId);
        Task<IEnumerable<Tenant>> GetAllAsync(int page = 1, int pageSize = 10);
        Task UpdateAsync(Tenant tenant);
        Task DeleteAsync(Guid tenantId);
        Task<int> GetTotalCountAsync();
    }
}
