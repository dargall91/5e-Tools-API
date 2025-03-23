using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Resolve))]
public class Resolve
{
    [Key]
    public int Id { get; set; }

    public int Score { get; set; }
}
