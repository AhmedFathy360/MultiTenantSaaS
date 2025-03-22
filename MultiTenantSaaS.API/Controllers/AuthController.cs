using Microsoft.AspNetCore.Mvc;
using MultiTenantSaaS.Application.Commands.Auth;
using MultiTenantSaaS.Application.DTOs;
using MediatR;

namespace MultiTenantSaaS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponse>> Register([FromBody] RegisterRequest request)
        {
            var command = new RegisterUserCommand
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                TenantId = request.TenantId
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] LoginRequest request)
        {
            var query = new LoginQuery
            {
                Email = request.Email,
                Password = request.Password
            };

            var result = await _mediator.Send(query);
            if (result == null)
                return Unauthorized();

            return Ok(result);
        }
    }
}