using _5eTools.Data.Entities;

namespace _5eTools.Services.DTOs;

public class MonsterDto
{
    public required int MonsterId { get; set; }

    public required string Name { get; set; }

    public required string DisplayName { get; set; }

    public required string Size { get; set; }

    public required string Type { get; set; }

    public required string Alignment { get; set; }

    public required int ArmorClass { get; set; }

    public required int HitPoints { get; set; }

    public required string Speed { get; set; }

    public required string Senses { get; set; }

    public required string Languages { get; set; }

    public required int BonusInitiative { get; set; }

    public required int LegendaryActionCount { get; set; }

    public required StrengthDto Strength { get; set; }

    public required DexterityDto Dexterity { get; set; }

    public required ConstitutionDto Constitution { get; set; }

    public required IntelligenceDto Intelligence { get; set; }

    public required WisdomDto Wisdom { get; set; }

    public required CharismaDto Charisma { get; set; }

    public required ChallengeRating ChallengeRating { get; set; }

    public required IEnumerable<ActionAbilityDto> Abilities { get; set; }

    public required IEnumerable<ActionAbilityDto> Actions { get; set; }

    public required IEnumerable<LegendaryActionDto> LegendaryActions { get; set; }
}
