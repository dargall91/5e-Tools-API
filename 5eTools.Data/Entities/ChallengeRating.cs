using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _5eTools.Data.Entities;

[Table(nameof(ChallengeRating))]
public class ChallengeRating
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public required int Id { get; set; }

    [JsonPropertyName("cr")]
    public required string CR { get; set; }

    [JsonPropertyName("xp")]
    public required int XP { get; set; }

    public required int ProficiencyBonus { get; set; }
}
