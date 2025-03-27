using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(WarlockSpellSlots))]
public class WarlockSpellSlots
{
    [Key]
    public int Id { get; set; }

    public int FirstLevel { get; set; }

    public int SecondLevel { get; set; }

    public int ThirdLevel { get; set; }

    public int FourthLevel { get; set; }

    public int FifthLevel { get; set; }
}
