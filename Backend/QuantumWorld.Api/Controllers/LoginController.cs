using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Extensions;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Api.Controllers
{
    public class LoginController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;

        public LoginController(IMediator mediator, IMemoryCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Login command)
        {
            command.TokenId = Guid.NewGuid();
            await _mediator.Send(command);
            var jwt = _cache.GetJwt(command.TokenId);
            return Json(jwt);
        }
    }
}