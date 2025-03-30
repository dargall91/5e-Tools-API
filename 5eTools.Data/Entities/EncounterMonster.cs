using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(EncounterMonster))]
public class EncounterMonster
{
    [Key]
    public int Id { get; set; }

    public required int Quantity { get; set; }

    public required int InitiativeRoll { get; set; }

    public bool IsInvisible { get; set; }

    public bool IsReinforcement { get; set; }

    public bool IsMinion { get; set; }

    [ForeignKey($"{nameof(Entities.Encounter)}{nameof(Entities.Encounter.Id)}")]
    public virtual required Encounter Encounter { get; set; }

    [ForeignKey($"{nameof(Entities.Monster)}{nameof(Entities.Monster.Id)}")]
    public virtual required Monster Monster { get; set; }
}
