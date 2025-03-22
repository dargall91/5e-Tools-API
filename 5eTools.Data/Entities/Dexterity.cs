using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Dexterity))]
public class Dexterity
{
    [Key]
    public int Id { get; set; }

    public int Score { get; set; }

    public int Acrobatics { get; set; }
    public int SleightOfHand { get; set; }
    public int Stealth { get; set; }
}
