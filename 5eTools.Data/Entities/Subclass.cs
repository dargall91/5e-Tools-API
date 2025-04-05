using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Subclass))]
public class Subclass
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public bool HasPrimalCompanion { get; set; }

    public bool ThirdCaster { get; set; }

    [ForeignKey($"{nameof(Entities.Class)}{nameof(Entities.Class.Id)}")]
    public virtual required Class Class { get; set; }

    [ForeignKey($"{nameof(Subclass)}{nameof(Id)}")]
    public required virtual ICollection<Campaign> Campaigns { get; set; }
}
