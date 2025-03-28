using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Class))]
public class Class
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public required int HitDieSize { get; set; }

    public required int AverageHitDieRoll { get; set; }

    public string ClassAbilityScore { get; set; } = string.Empty;

    [ForeignKey($"{nameof(Entities.CasterType)}{nameof(Entities.CasterType.Id)}")]
    public virtual required CasterType CasterType { get; set; }

    public virtual ICollection<Subclass> Subclasses { get; } = new List<Subclass>();
}
