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

        public async Task<IActionResult> Post([FromBody] Login command)
        {
            // await _mediator.Send(command);
            throw new NotImplementedException();
            // return JWT from cache

        }
    }
}