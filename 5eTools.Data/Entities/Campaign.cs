using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Campaign))]
public class Campaign
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public required string Name { get; set; }

    public required bool IsActive { get; set; }

    public required bool UsesInflatedHitPoints { get; set; }

    public required bool UsesStress { get; set; }

    public required bool IsDeleted { get; set; }
}
