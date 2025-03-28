using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(CharacterClass))]
public class CharacterClass
{
    [Key]
    public int Id { get; set; }

    public int Level { get; set; }

    public int HitDiceUsed { get; set; }

    public bool BaseClass { get; set; }

    public int BaseClassSaveDc { get; set; }

    [ForeignKey($"{nameof(Entities.Subclass)}{nameof(Entities.Subclass.Id)}")]
    public virtual required Subclass Subclass { get; set; }

    [ForeignKey($"{nameof(Entities.PrimalCompanion)}{nameof(Entities.PrimalCompanion.Id)}")]
    public virtual PrimalCompanion? PrimalCompanion { get; set; }
}
