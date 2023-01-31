using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Api.Controllers;

public class ResearchController : ApiControllerBase
{
    private readonly IUserService _userService;
    private readonly IMediator _mediator;

    public ResearchController(IUserService userService, IMediator mediator)
    {
        _userService = userService;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UpgradeResearch request)
    {
        await _mediator.Send(request);
        return Ok();
    }

}