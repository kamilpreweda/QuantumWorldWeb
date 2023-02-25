using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("{username}")]
    public async Task<IActionResult> Get(string username)
    {
        var user = await _userService.GetAsync(username);
        if (user == null)
        {
            return NotFound();
        }

        return Json(user);
    }

    [HttpGet]
    [Route("myself")]
    // [HttpGet, Authorize(Roles = "Admin")]
    public ActionResult<string> GetMe()
    {
        var userId = _userService.GetMyId();
        return Ok(userId);
    }

    [HttpGet]
    public async Task<IActionResult> BrowseAsync()
    {
        var users = await _userService.BrowseAsync();
        return Json(users);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Post([FromBody] CreateUser request)
    {
        await _mediator.Send(request);
        var user = await _userService.GetAsync(request.Username);
        return Ok(user);
        // return Created($"users/{request.Username}", new object());
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteUser request)
    {
        await _mediator.Send(request);
        return NoContent();
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] GenerateRefreshToken request)
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var user = await _userService.GetAsync(request.Username);

        if (!refreshToken.Equals(user.RefreshToken))
        {
            return Unauthorized("Invalid Refresh Token.");
        }
        else if (user.TokenExpires < DateTime.Now)
        {
            return Unauthorized("Token expired");
        }
        await _mediator.Send(request);
        return Ok();
    }


}
