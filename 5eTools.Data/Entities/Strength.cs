using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Strength))]
public class Strength
{
    [Key]
    public int Id { get; set; }

    public int Score { get; set; }

    public int Athletics { get; set; }
}
