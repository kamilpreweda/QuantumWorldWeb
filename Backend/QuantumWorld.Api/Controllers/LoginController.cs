using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Api.Controllers
{
    public class LoginController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;
        public LoginController(IMediator mediator, IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
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