namespace _5eTools.Services.DTOs;

public class PlayerCharacterCombatantDto
{
    public required int PlayerCharacterId { get; set; }

    public required string PlayerCharacterName { get; set; }

    public required int TotalArmorClass { get; set; }

    public required bool IsCombatant { get; set; }

    public required int InitiativeRoll { get; set; }

    public required int InitiativeBonus { get; set; }
}
