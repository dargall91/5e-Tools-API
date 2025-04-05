namespace _5eTools.Services.DTOs;

public class SubclassDto
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public required string ClassName { get; set; }

    public required bool PrimalCompanion { get; set; }

    public required bool JackOfAllTrades { get; set; }

    public required int ClassHitDieSize { get; set; }
}
