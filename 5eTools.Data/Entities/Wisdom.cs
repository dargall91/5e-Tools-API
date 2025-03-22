using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Wisdom))]
public class Wisdom
{
    [Key]
    public int Id { get; set; }

    public int Score { get; set; }

    public int AnimalHandling { get; set; }
    public int Insight { get; set; }
    public int Medicine { get; set; }
    public int Perception { get; set; }
    public int Survival { get; set; }
}
