using MediatR;
using System;

namespace MultiTenantSaaS.Application.Commands.DeleteTenant
{
    public class DeleteTenantCommand : IRequest<bool>
    {
        public Guid TenantId { get; set; }

        public DeleteTenantCommand(Guid tenantId)
        {
            TenantId = tenantId;
        }
    }
}