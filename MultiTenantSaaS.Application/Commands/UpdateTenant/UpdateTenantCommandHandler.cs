using MediatR;
using MultiTenantSaaS.Domain.Interfaces;

namespace MultiTenantSaaS.Application.Commands.UpdateTenant
{
    public class UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand, bool>
    {
        private readonly ITenantRepository _tenantRepository;

        public UpdateTenantCommandHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<bool> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.GetbyIdAync(request.TenantId);
            if (tenant == null)
                return false;

            tenant.Name = request.Name;
            tenant.IsActive = request.IsActive;
            tenant.ModifiedAt = DateTime.UtcNow;

            await _tenantRepository.UpdateAsync(tenant);
            return true;
        }
    }
}