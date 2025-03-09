using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSaaS.Application.Commands.CreateTenant
{
    public class CreateTenantCommand : IRequest<Guid> // explain IRequest? IRequest is a marker interface that tells MediatR that this is a request and it should return a response
    {
        // Command to create a new tenant
        public string Name { get; set; } = default!;
    }
}
