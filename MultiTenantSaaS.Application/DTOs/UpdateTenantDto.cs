using System;

namespace MultiTenantSaaS.Application.DTOs
{
    public class UpdateTenantDto
    {
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}