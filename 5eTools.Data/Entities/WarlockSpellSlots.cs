using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(WarlockSpellSlots))]
public class WarlockSpellSlots
{
    [Key]
    public int Id { get; set; }

    public required string Level { get; set; }

    public int Slots { get; set; }
}
