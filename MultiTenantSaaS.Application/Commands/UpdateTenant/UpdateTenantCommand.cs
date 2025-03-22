using MediatR;
using System;

namespace MultiTenantSaaS.Application.Commands.UpdateTenant
{
    public class UpdateTenantCommand : IRequest<bool>
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}