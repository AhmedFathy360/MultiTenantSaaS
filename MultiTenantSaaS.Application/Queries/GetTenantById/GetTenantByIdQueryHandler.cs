﻿using MediatR;
using MultiTenantSaaS.Application.DTOs;
using MultiTenantSaaS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSaaS.Application.Queries.GetTenantById
{
    public class GetTenantByIdQueryHandler : IRequestHandler<GetTenantByIdQuery, TenantDto?>
    {
        private readonly ITenantRepository _tenantRepository;
        public GetTenantByIdQueryHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public async Task<TenantDto?> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.GetbyIdAync(request.TenantId);

            if (tenant == null)
            {
                return null;
            }

            // Map domain entity to DTO
            var dto = new TenantDto
            {
                TenantId = tenant.TenantId,
                Name = tenant.Name
            };

            return dto;
        }
    }
}
