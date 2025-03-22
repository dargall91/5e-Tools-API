using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(LegendaryAction))]
public class LegendaryAction
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Cost { get; set; }

    [Required]
    [ForeignKey($"{nameof(Entities.Monster)}{nameof(Entities.Monster.Id)}")]
    public virtual Monster? Monster { get; set; }
}
