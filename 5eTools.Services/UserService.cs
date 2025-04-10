using _5eTools.Data;
using _5eTools.Data.Entities;
using _5eTools.Services.DTOs;
using _5eTools.Services.Models;
namespace _5eTools.Services;

public interface IUserService
{
    bool UserExists(int id);

    User FindById(int id);

    /// <summary>
    /// Validates a login attempt.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>
    /// <list type="bullet">
    /// <item>A <c>null</c> if the login attempt was successful</item>
    /// <item>An error describing the failure if the login attempt was unsuccessful<item>
    /// </list>
    /// </returns>
    LoginAttemptResult AttemptLogin(LoginRequest user);

    /// <summary>
    /// Validates that a new user's username is unique and their password meets the
    /// minimum requirements
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    List<string> ValidateNewUser(LoginRequest user);

    /// <summary>
    /// Adds a new <see cref="User"/>. The new User's Username and Password
    /// are assumed to be valid at this point. They should be validated via
    /// <see cref="ValidateNewUser"/> prior to calling this method.
    /// </summary>
    /// <param name="user">The credentials for the new user</param>
    /// <returns>The ID of the newly created <see cref="User"/></returns>
    UserDto RegisterUser(LoginRequest user);

    void UpdateUserPermissions(int id, bool? isAdmin, bool? canHostCampaigns);

    void SetUserActiveStatus(int id, bool isActive);
}

public class UserService(ICryptographyService cryptographyService, ToolsDbContext dbContext) : IUserService
{
    public bool UserExists(int id) => dbContext.Users.Find(id) != default;

    public User FindById(int id) => dbContext.Users.Find(id)!;

    public LoginAttemptResult AttemptLogin(LoginRequest loginAttempt)
    {
        var result = new LoginAttemptResult();

        var user = dbContext.Users.SingleOrDefault(x => x.Username == loginAttempt.Username);

        if (user == default)
        {
            result.Error = "Username or Password is incorrect";
        }
        else
        {
            var password = cryptographyService.Decrypt(loginAttempt.Password, loginAttempt.Username);

            if (user.Password != password)
            {
                result.Error = "Username or Password is incorrect";
            }
            else
            {
                result.User = FindUserDto(user.Id);
            }
        }

        return result;
    }

    public UserDto RegisterUser(LoginRequest user)
    {
        var newUser = new User
        {
            Username = user.Username,
            Password = cryptographyService.Decrypt(user.Password, user.Username),
        };

        //first user to register is defacto admin
        if (!dbContext.Users.Any())
        {
            newUser.IsAdmin = true;
        }

        dbContext.Add(newUser);
        dbContext.SaveChanges();

        return FindUserDto(newUser.Id);
    }

    public List<string> ValidateNewUser(LoginRequest user)
    {
        var errors = new List<string>();

        if (dbContext.Users.Any(x => x.Username == user.Username))
        {
            errors.Add("A user with this username already exists");
        }


        errors.AddRange(ValidatePassword(cryptographyService.Decrypt(user.Password, user.Username)));

        return errors;
    }

    public void UpdateUserPermissions(int id, bool? isAdmin, bool? canHostCampaigns)
    {
        var user = dbContext.Users.Find(id)!;
        user.IsAdmin = isAdmin ?? user.IsAdmin;
        user.CanHostCampaigns = canHostCampaigns ?? user.CanHostCampaigns;

        dbContext.SaveChanges();
    }

    public void SetUserActiveStatus(int id, bool isActive)
    {
        var user = dbContext.Users.Find(id)!;
        user.Deactivated = !isActive;

        dbContext.SaveChanges();
    }

    private static List<string> ValidatePassword(string encryptedPassword)
    {
        var errors = new List<string>();

        //TODO: define password rules

        return errors;
    }

    private UserDto FindUserDto(int id)
    {
        return dbContext.Users.Select(x => new UserDto
        {
            UserId = x.Id,
            Username = x.Username,
            IsAdmin = x.IsAdmin
        })
        .Single(x => x.UserId == id);
    }
}
