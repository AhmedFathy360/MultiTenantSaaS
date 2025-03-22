using MediatR;
using MultiTenantSaaS.Application.DTOs;
using MultiTenantSaaS.Domain.Interfaces;

namespace MultiTenantSaaS.Application.Queries.GetTenantList
{
    public class GetTenantsListQueryHandler : IRequestHandler<GetTenantsListQuery, PaginatedResponse<TenantDto>>
    {
        private readonly ITenantRepository _tenantRepository;

        public GetTenantsListQueryHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<PaginatedResponse<TenantDto>> Handle(GetTenantsListQuery request, CancellationToken cancellationToken)
        {
            var tenants = await _tenantRepository.GetAllAsync(request.PageNumber, request.PageSize);
            var totalCount = await _tenantRepository.GetTotalCountAsync();

            var tenantDtos = tenants.Select(t => new TenantDto
            {
                TenantId = t.TenantId,
                Name = t.Name
            });

            return new PaginatedResponse<TenantDto>
            {
                Items = tenantDtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}