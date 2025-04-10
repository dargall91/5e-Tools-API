using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(PrimalCompanion))]
public class PrimalCompanion
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public int HitPointMaximum { get; set; }

    public int ArmorClassBonus { get; set; }

    public int Damage { get; set; }

    public int MaxHitPointReduction { get; set; }

    public int TemporaryHitPoints { get; set; }

    public int DeathSaveFailures { get; set; }

    public int DeathSaveSuccesses { get; set; }

    public int HitDiceUsed { get; set; }

    public int? StrengthOverride { get; set; }

    public int? DexterityOverride { get; set; }

    public int? ConstitutionOverride { get; set; }

    public int? IntelligenceOverride { get; set; }

    public int? WisdomOverride { get; set; }

    public int? CharismaOverride { get; set; }

    [ForeignKey($"{nameof(Entities.PrimalCompanionType)}{nameof(Entities.PrimalCompanionType.Id)}")]
    public virtual required PrimalCompanionType PrimalCompanionType { get; set; }
}
