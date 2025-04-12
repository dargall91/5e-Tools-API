using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(WarlockSpellSlots))]
public class WarlockSpellSlots
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public required int Level { get; set; }

    public required int Slots { get; set; }
}
