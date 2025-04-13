using _5eTools.API.Models;
using _5eTools.Services;
using _5eTools.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace _5eTools.API.Controllers.V1;

[Route("api/v{version:apiversion}/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPut("register")]
    public IActionResult RegisterUser(LoginRequest user)
    {
        var errors = userService.ValidateNewUser(user);

        if (errors.Count > 0)
        {
            return BadRequest(new ResponseWrapper<bool>(errors));
        }

        var newUser = userService.RegisterUser(user);

        return Ok(new ResponseWrapper<UserDto>(newUser, "Account created", "Info"));
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest user)
    {
        var loginAttemptResult = userService.AttemptLogin(user);

        if (!string.IsNullOrEmpty(loginAttemptResult.Error))
        {
            return BadRequest(new ResponseWrapper<bool>(loginAttemptResult.Error));
        }

        return Ok(new ResponseWrapper<UserDto>(loginAttemptResult.User));
    }

    [HttpPost("deactivate/{id}")]
    public IActionResult DeactivateUser(int id)
    {
        if (userService.UserExists(id))
        {
            userService.SetUserActiveStatus(id, false);

            return Ok(new ResponseWrapper<bool>(true, "User Deactivated", "Info"));
        }

        return NotFound(new ResponseWrapper<object>($"No User with ID {id} found"));
    }

    [HttpPost("activate/{id}")]
    public IActionResult ActivateUser(int id)
    {
        if (userService.UserExists(id))
        {
            userService.SetUserActiveStatus(id, false);

            return Ok(new ResponseWrapper<bool>("User Activated", "Info"));
        }

        return NotFound(new ResponseWrapper<object>($"No User with ID {id} found"));
    }

    [HttpPost("{id}")]
    public IActionResult UpdateUserPermissions(int id, bool? isAdmin, bool? canHostCampaigns)
    {
        if (userService.UserExists(id))
        {
            userService.UpdateUserPermissions(id, isAdmin, canHostCampaigns);

            return Ok(new ResponseWrapper<bool>("Permissions updated", "Info"));
        }

        return NotFound(new ResponseWrapper<object>($"No User with ID {id} found"));
    }
}
