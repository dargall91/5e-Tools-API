using _5eTools.API.Models;
using _5eTools.Services;
using _5eTools.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace _5eTools.API.Controllers.V1;

public class UserController(IUserService userService) : ControllerBase
{
    [HttpPut]
    public IActionResult RegisterUser(UserDto user)
    {
        var errors = userService.ValidateNewUser(user);

        if (errors.Count > 0)
        {
            return BadRequest(new ResponseWrapper<int>(errors));
        }

        var id = userService.RegisterUser(user);

        return Ok(new ResponseWrapper<int>(id, "Account created", "Info"));
    }

    [HttpPost("login")]
    public IActionResult Login(UserDto user)
    {
        var loginAttemptResult = userService.AttemptLogin(user);

        if (!string.IsNullOrEmpty(loginAttemptResult.Error))
        {
            return BadRequest(new ResponseWrapper<int>(loginAttemptResult.Error));
        }

        return Ok(new ResponseWrapper<int>(loginAttemptResult.UserId));
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
