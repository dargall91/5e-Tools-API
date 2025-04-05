using _5eTools.Data.Entities;

namespace _5eTools.Services.DTOs;

public class PrimalCompanionDto
{
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

    public required PrimalCompanionType PrimalCompanionType { get; set; }
}
