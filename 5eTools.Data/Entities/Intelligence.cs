using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Intelligence))]
public class Intelligence
{
    [Key]
    public int Id { get; set; }

    public int Score { get; set; }

    public int Arcana { get; set; }
    public int History { get; set; }
    public int Investigation { get; set; }
    public int Nature { get; set; }
    public int Religion { get; set; }
}
