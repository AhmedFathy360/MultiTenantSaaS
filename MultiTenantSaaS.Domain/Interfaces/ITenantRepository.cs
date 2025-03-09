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
        Tenant? GetbyIdAync(Guid tenantId);
    }
}
