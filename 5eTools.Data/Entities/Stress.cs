using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Stress))]
public class Stress
{
    [Key]
    public int Id { get; set; }

    public int StressLevel { get; set; }

    public int StressThreshold { get; set; }

    public int StressMaximum { get; set; }

    public int MeditationDiceUsed { get; set; }

    [ForeignKey($"{nameof(StressStatus)}{nameof(Id)}")]
    public StressStatus? StressStatus { get; set; }
}
