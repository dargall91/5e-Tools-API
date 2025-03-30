using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(EncounterXpThreshold))]
public class EncounterXpThreshold
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public int Easy { get; set; }

    public int Medium { get; set; }

    public int Hard { get; set; }

    public int Deadly { get; set; }

    public int Budget { get; set; }
}
