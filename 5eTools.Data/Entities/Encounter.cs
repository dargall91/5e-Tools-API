using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Encounter))]
public class Encounter
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public bool HasLairAction { get; set; }

    public bool IsArchived { get; set; }

    [ForeignKey($"{nameof(Entities.Music)}{nameof(Entities.Music.Id)}")]
    public virtual required Music Music { get; set; }

    [ForeignKey($"{nameof(Entities.Campaign)}{nameof(Entities.Campaign.Id)}")]
    public virtual required Campaign Campaign { get; set; }

    public virtual ICollection<EncounterMonster> EncounterMonsters { get; set; } = new List<EncounterMonster>();
}
