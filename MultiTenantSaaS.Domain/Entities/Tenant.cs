using MultiTenantSaaS.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSaaS.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        public Guid TenantId { get; set; } // Primary key or identifier for this tenant.
        public string Name { get; set; } = default!; // The human-readable name for the tenant.
        public bool IsActive { get; set; } = true;
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        
        public void Deactivate() => IsActive = false;
    }
}
