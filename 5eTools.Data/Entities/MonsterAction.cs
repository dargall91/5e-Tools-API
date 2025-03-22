using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(MonsterAction))]
public class MonsterAction
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    [ForeignKey($"{nameof(Entities.Monster)}{nameof(Entities.Monster.Id)}")]
    public virtual Monster? Monster { get; set; }
}
