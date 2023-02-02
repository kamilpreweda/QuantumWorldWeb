using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuantumWorld.Infrastructure.Commands.Users;

namespace QuantumWorld.Api.Controllers
{
    public class LoginController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Login command)
        {
            await _mediator.Send(command);
            return Ok();
            // return JWT from cache
        }
    }
}