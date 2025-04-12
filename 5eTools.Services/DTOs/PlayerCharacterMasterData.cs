using _5eTools.Data.Entities;

namespace _5eTools.Services.DTOs;

public class PlayerCharacterMasterData
{
    public required IEnumerable<StressTypeDto> StressTypes { get; set; }
    public required IEnumerable<ExhaustionLevel> ExhaustionLevels { get; set; }
    public required IEnumerable<PrimalCompanionType> PrimalCompanionTypes { get; set; }
}
