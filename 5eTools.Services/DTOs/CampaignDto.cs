namespace _5eTools.Services.DTOs;

public class CampaignDto
{
    public int CampaignId { get; set; }

    public required string Name { get; set; }

    public required bool UsesInflatedHitPoints { get; set; }

    public required bool UsesStress { get; set; }

    public bool AllowsMulticlassing { get; set; }

    public required IEnumerable<ClassDto> Classes { get; set; }
}
