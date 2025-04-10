namespace _5eTools.Services.DTOs;

public class ClassDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required int HitDieSize { get; set; }

    public required string ClassAbilityScore { get; set; }

    public required IEnumerable<SubclassDto> Subclasses { get; set; }
}
