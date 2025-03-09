using MediatR;
using MultiTenantSaaS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSaaS.Application.Queries.GetTenantById
{
    public class GetTenantByIdQuery : IRequest<TenantDto?>
    {
        // Query to retrieve a Tenant by its ID.
        public Guid TenantId { get; set; }
        public GetTenantByIdQuery(Guid tenantId)
        {
            TenantId = tenantId;
        }
    }
}
