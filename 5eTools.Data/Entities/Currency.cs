using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Currency))]
public class Currency
{
    [Key]
    public int Id { get; set; }
    public int Copper { get; set; }
    public int Silver { get; set; }
    public int Electrum { get; set; }
    public int Gold { get; set; }
    public int Platinum { get; set; }
}
