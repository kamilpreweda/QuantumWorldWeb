using Microsoft.AspNetCore.Mvc;
using QuantumWorld.Infrastructure.DTO;
using QuantumWorld.Infrastructure.Services;

namespace QuantumWorld.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{email}")]
    public UserDto Get(string email)
    {
        return _userService.GetUser(email);
    }
}
