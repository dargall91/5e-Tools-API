namespace _5eTools.Services.DTOs;

public class PlayerCharacterDto
{
    public required string Name { get; set; }

    public int BaseArmorClass { get; set; }

    public int ArmorClassBonus { get; set; }

    public int Damage { get; set; }

    public int HitPointMaximum { get; set; }

    public int TemporaryHitPoints { get; set; }

    public int MaxHitPointReduction { get; set; }

    public int DeathSaveFailures { get; set; }

    public int DeathSaveSuccesses { get; set; }

    public bool ToughFeat { get; set; }

    public int ExhaustionLevel { get; set; }

    public int SpellcasterLevel { get; set; }

    public int WarlockLevel { get; set; }

    public required StrengthDto Strength { get; set; }

    public required DexterityDto Dexterity { get; set; }

    public required ConstitutionDto Constitution { get; set; }

    public required IntelligenceDto Intelligence { get; set; }

    public required WisdomDto Wisdom { get; set; }

    public required CharismaDto Charisma { get; set; }

    public ResolveDto? Resolve { get; set; }

    public StressDto? Stress { get; set; }

    public UsedSpellSlotsDto? UsedSpellSlotsDto { get; set; }

    public required IEnumerable<CharacterClassDto> CharacterClasses { get; set; }
}
