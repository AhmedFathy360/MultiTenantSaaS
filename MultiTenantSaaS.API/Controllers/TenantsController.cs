using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenantSaaS.Application.Commands.CreateTenant;
using MultiTenantSaaS.Application.Commands.DeleteTenant;
using MultiTenantSaaS.Application.Commands.UpdateTenant;
using MultiTenantSaaS.Application.DTOs;
using MultiTenantSaaS.Application.Queries.GetTenantById;
using MultiTenantSaaS.Application.Queries.GetTenantList;

namespace MultiTenantSaaS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TenantsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TenantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Guid>> CreateTenant([FromBody] CreateTenantCommand command)
        {
            var tenantId = await _mediator.Send(command);
            return Ok(tenantId);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetTenantById(Guid id)
        {
            // Ensure user belongs to the tenant they're trying to access
            var tenantClaim = User.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;
            if (tenantClaim != id.ToString())
                return Forbid();

            var result = await _mediator.Send(new GetTenantByIdQuery(id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PaginatedResponse<TenantDto>>> GetTenants([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetTenantsListQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateTenant(Guid id, [FromBody] UpdateTenantDto updateDto)
        {
            var command = new UpdateTenantCommand 
            { 
                TenantId = id,
                Name = updateDto.Name,
                IsActive = updateDto.IsActive
            };

            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteTenant(Guid id)
        {
            var result = await _mediator.Send(new DeleteTenantCommand(id));
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
