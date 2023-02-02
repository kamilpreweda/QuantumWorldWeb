using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public AccountController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        [HttpPut]
        [Route("password")]
        public async Task<IActionResult> Put([FromBody] ChangeUserPassword request)
        {
            await Task.CompletedTask;
            return NoContent();
        }
    }
}