using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(StressType))]
public class StressType
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public required string Name { get; set; }

    public required int MinimumRoll { get; set; }

    public required int MaximumRoll { get; set; }

    public virtual ICollection<StressStatus> StressStatuses { get; } = new List<StressStatus>();
}
