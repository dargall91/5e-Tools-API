using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(StressStatus))]
public class StressStatus
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public int MinimumRoll { get; set; }

    public int MaximumRoll { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    [ForeignKey($"{nameof(Entities.StressType)}{nameof(Entities.StressType.Id)}")]
    public required virtual StressType StressType { get; set; }
}
