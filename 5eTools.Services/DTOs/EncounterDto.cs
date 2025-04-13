namespace _5eTools.Services.DTOs;

public class EncounterDto
{
    public required int EncounterId { get; set; }

    public required string Name { get; set; }

    public bool HasLairAction { get; set; }

    public int MusicId { get; set; }

    public IEnumerable<EncounterMonsterDto> EncounterMonsterDtos { get; set; } = new List<EncounterMonsterDto>();
}
