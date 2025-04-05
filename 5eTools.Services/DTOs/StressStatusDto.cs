namespace _5eTools.Services.DTOs;

public class StressStatusDto
{
    public required int Id { get; set; }

    public required int MinimumRoll { get; set; }

    public required int MaximumRoll { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required string StressType { get; set; }
}
