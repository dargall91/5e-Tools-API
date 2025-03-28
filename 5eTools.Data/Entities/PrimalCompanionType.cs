using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(PrimalCompanionType))]
public class PrimalCompanionType
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public required int HitPointMultiplier { get; set; }

    public required int HitDieSize { get; set; }

    public required string Speed { get; set; }

    public required string Size { get; set; }

    public required int Strength { get; set; }

    public required int Dexterity { get; set; }

    public required int Constitution { get; set; }

    public required int Intelligence { get; set; }

    public required int Wisdom { get; set; }

    public required int Charisma { get; set; }

    public required string AbilityName { get; set; }

    public required string AbilityDescription { get; set; }

    public required string ActionName { get; set; }

    public required string ActionDescription { get; set; }
}
