using System.ComponentModel.DataAnnotations;

namespace _5eTools.Services.DTOs;

public class AddEditCampaign
{
    [MaxLength(100)]
    public required string Name { get; set; }

    public required bool UsesInflatedHitPoints { get; set; }

    public required bool UsesStress { get; set; }

    public bool IsFinished { get; set; }
}
