using System.ComponentModel.DataAnnotations;

namespace _5eTools.Data.Entities;

public class BaseActionAbility
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}
