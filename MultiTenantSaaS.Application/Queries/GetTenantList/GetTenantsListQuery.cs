using MediatR;
using MultiTenantSaaS.Application.DTOs;

namespace MultiTenantSaaS.Application.Queries.GetTenantList
{
    public class GetTenantsListQuery : IRequest<PaginatedResponse<TenantDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public GetTenantsListQuery(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}