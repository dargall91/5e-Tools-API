using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Dexterity))]
public class Dexterity : BaseAbilityScore
{
    public int Acrobatics { get; set; }
    public int SleightOfHand { get; set; }
    public int Stealth { get; set; }
}
