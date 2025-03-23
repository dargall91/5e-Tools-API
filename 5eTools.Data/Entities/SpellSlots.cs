using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(SpellSlots))]
public class SpellSlots
{
    [Key]
    public int Id { get; set; }

    public int FirstLevelSlotsUsed { get; set; }

    public int SecondLevelSlotsUsed { get; set; }

    public int ThirdLevelSlotsUsed { get; set; }
    public int FourthLevelSlotsUsed { get; set; }
    public int FifthLevelSlotsUsed { get; set; }
    public int SixthLevelSlotsUsed { get; set; }
    public int SeventhLevelSlotsUsed { get; set; }
    public int EigthLevelSlotsUsed { get; set; }
    public int NinthLevelSlotsUsed { get; set; }
    public int WarlockSlotsUsed { get; set; }
}
