using _5eTools.Services.DTOs;

namespace _5eTools.Services.Models;

public class LoginAttemptResult
{
    public string? Error { get; set; }
    public UserDto? User { get; set; }
}
