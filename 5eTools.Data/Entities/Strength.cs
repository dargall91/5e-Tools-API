using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Strength))]
public class Strength : BaseAbilityScore
{
    public int Athletics { get; set; }
}
