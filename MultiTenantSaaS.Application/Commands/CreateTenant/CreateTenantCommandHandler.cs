using MediatR;
using MultiTenantSaaS.Domain.Entities;
using MultiTenantSaaS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSaaS.Application.Commands.CreateTenant
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Guid> // explain IRequestHandler? IRequestHandler is a generic interface that takes two parameters: the request type and the response type
    {
        // Handler that processes the CreateTenantCommand.
        private readonly ITenantRepository _tenantRepository; // explain ITenantRepository? ITenantRepository is an interface that defines the contract for the tenant repository.

        public CreateTenantCommandHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<Guid> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            // Create a new tenant entity
            var newTenant = new Tenant
            {
                TenantId = Guid.NewGuid(),
                Name = request.Name
            };

            // Add the tenant to the repository
            await _tenantRepository.AddAsync(newTenant);
            return newTenant.TenantId;
        }

    }
}