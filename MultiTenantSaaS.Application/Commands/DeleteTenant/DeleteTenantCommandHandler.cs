using MediatR;
using MultiTenantSaaS.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MultiTenantSaaS.Application.Commands.DeleteTenant
{
    public class DeleteTenantCommandHandler : IRequestHandler<DeleteTenantCommand, bool>
    {
        private readonly ITenantRepository _tenantRepository;

        public DeleteTenantCommandHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<bool> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.GetbyIdAync(request.TenantId);
            if (tenant == null)
                return false;

            await _tenantRepository.DeleteAsync(request.TenantId);
            return true;
        }
    }
}