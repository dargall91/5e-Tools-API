using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(ChallengeRating))]
public class ChallengeRating
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public required int Id { get; set; }
    public required string CR { get; set; }
    public required int XP { get; set; }
    public required int ProficiencyBonus { get; set; }
}
