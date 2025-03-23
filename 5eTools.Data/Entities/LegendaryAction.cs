using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(LegendaryAction))]
public class LegendaryAction : BaseActionAbility
{
    public int Cost { get; set; }
}
