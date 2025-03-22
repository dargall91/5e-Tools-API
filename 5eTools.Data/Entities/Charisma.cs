using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Charisma))]
public class Charisma
{
    [Key]
    public int Id { get; set; }

    public int Score { get; set; }
    public int Deception { get; set; }
    public int Intimidation { get; set; }
    public int Performance { get; set; }
    public int Persuasion { get; set; }
}
