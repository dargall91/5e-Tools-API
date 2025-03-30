namespace _5eTools.Services.DTOs;

public class EncounterMonsterDto
{
    public required int MonsterId { get; set; }

    public required string Name { get; set; }

    public required int Quantity { get; set; }

    public required int InitiativeRoll { get; set; }

    public required bool IsInvisible { get; set; }

    public required bool IsReinforcement { get; set; }

    public required bool IsMinion { get; set; }
}
