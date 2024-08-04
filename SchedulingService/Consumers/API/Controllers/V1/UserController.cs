using Application.User.Ports;
using Application.User.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1;

[ApiController]
[Route("api/v1/users")]
public class UserController  : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfessionalAsync([FromBody] CreateUserRequest request)
    {
        request.SetRole(Domain.Enums.Roles.Professional);
        var created = await _userService.CreateUserAsync(request);
        if(created.Errors.Count() > 0)
            return BadRequest(created);

        var uri = "api/v1/users/" + created.User.Id;
        return Created(uri,created);
    }
}
