using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuantumWorld.Infrastructure.Commands.Users;
using QuantumWorld.Infrastructure.DTO;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ApiControllerBase
{
    private readonly IUserService _userService;
    private readonly IMediator _mediator;

    public UsersController(IUserService userService, IMediator mediator)
    {
        _userService = userService;
        _mediator = mediator;
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> Get(string email)
    {
        var user = await _userService.GetAsync(email);
        if (user == null)
        {
            return NotFound();
        }

        return Json(user);
    }

    [HttpGet]
    public async Task<IActionResult> BrowseAsync()
    {
        var users = await _userService.BrowseAsync();
        return Json(users);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUser request)
    {
        await _mediator.Send(request);
        return Created($"users/{request.Email}", new object());
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteUser request)
    {
        await _mediator.Send(request);
        return NoContent();
    }


}
