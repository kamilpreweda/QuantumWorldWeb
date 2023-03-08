using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QuantumWorld.Core.Domain;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Exceptions;
using QuantumWorld.Infrastructure.Extensions;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Api.Controllers
{
    public class LoginController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;

        public LoginController(IMediator mediator, IMemoryCache cache, IJwtService jwtService, IUserService userService)
        {
            _mediator = mediator;
            _cache = cache;
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Login request)
        {
            try
            {
                request.TokenId = Guid.NewGuid();
                await _mediator.Send(request);
                var jwt = _cache.GetJwt(request.TokenId);

                return Ok(jwt.Token);
            }
            catch (InvalidCredentialsException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}