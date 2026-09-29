using Microsoft.AspNetCore.Mvc;

namespace ServiceDesk.Api.Modules.Users;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserRequest request)
    {
        var user = await _userService.CreateAsync(request);

        return Ok(user);
    }
}