using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(User))]
public class User
{
    [Key]
    public int Id { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }

    public bool IsAdmin { get; set; }

    public bool CanHostCampaigns { get; set; }

    public bool Deactivated { get; set; }
}
