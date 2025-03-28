using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(ExhaustionLevel))]
public class ExhaustionLevel
{
    [Key]
    public int Id { get; set; }

    public required string Description { get; set; }
}
