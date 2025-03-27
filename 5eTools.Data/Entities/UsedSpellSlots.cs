using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(UsedSpellSlots))]
public class UsedSpellSlots
{
    [Key]
    public int Id { get; set; }

    public int FirstLevel { get; set; }

    public int SecondLevel { get; set; }

    public int ThirdLevel { get; set; }

    public int FourthLevel { get; set; }

    public int FifthLevel { get; set; }

    public int SixthLevel { get; set; }

    public int SeventhLevel { get; set; }

    public int EigthLevel { get; set; }

    public int NinthLevel { get; set; }

    public int Warlock { get; set; }
}
