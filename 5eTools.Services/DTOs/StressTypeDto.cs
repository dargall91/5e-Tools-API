namespace _5eTools.Services.DTOs;

public class StressTypeDto
{
    public required string Name { get; set; }

    public required int MinimumRoll { get; set; }

    public required int MaximumRoll { get; set; }

    public required IEnumerable<StressStatusDto> StressStatuses { get; set; }
}
