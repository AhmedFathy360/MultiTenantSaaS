using MultiTenantSaaS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSaaS.Domain.Events
{
    public class TenantCreatedEvent
    {
        public Tenant Tenant { get; set; }
        public TenantCreatedEvent(Tenant tenant)
        {
            Tenant = tenant;
        }
    }
}
