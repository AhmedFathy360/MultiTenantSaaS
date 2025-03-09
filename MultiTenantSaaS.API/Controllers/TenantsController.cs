using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultiTenantSaaS.Application.Commands.CreateTenant;
using MultiTenantSaaS.Application.Queries.GetTenantById;

namespace MultiTenantSaaS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TenantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateTenant([FromBody] CreateTenantCommand command)
        {
            var tenantId = await _mediator.Send(command);
            return Ok(tenantId);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetTenantById(Guid id)
        {
            var result = await _mediator.Send(new GetTenantByIdQuery(id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
