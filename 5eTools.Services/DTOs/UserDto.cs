namespace _5eTools.Services.DTOs;

public class UserDto
{
    public int UserId { get; set; }
    public required string Username { get; set; }
    public bool IsAdmin { get; set; }
}