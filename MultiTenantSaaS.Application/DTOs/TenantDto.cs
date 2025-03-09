using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSaaS.Application.DTOs
{
    public class TenantDto
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; } = default!; // The human-readable name for the tenant.
    }
}
