using System.ComponentModel.DataAnnotations;

namespace _5eTools.Data.Entities;

public class BaseAbilityScore
{
    [Key]
    public int Id { get; set; }

    public int Score { get; set; }
}
