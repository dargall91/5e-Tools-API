using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Monster))]
public class Monster
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public string DisplayName { get; set; } = string.Empty;

    public string Size { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public string Alignment { get; set; } = string.Empty;

    public int ArmorClass { get; set; }
    public int HitPoints { get; set; }

    public string Speed { get; set; } = string.Empty;

    public string Senses { get; set; } = string.Empty;
    public string Languages { get; set; } = string.Empty;

    public int BonusInitiative { get; set; }

    public int LegendaryActionCount { get; set; }

    public bool IsArchived { get; set; }

    [ForeignKey($"{nameof(Entities.Strength)}{nameof(Entities.Strength.Id)}")]
    public virtual Strength Strength { get; set; } = new();

    [ForeignKey($"{nameof(Entities.Dexterity)}{nameof(Entities.Dexterity.Id)}")]
    public virtual Dexterity Dexterity { get; set; } = new();

    [ForeignKey($"{nameof(Entities.Constitution)}{nameof(Entities.Constitution.Id)}")]
    public virtual Constitution Constitution { get; set; } = new();

    [ForeignKey($"{nameof(Entities.Intelligence)}{nameof(Entities.Intelligence.Id)}")]
    public virtual Intelligence Intelligence { get; set; } = new();

    [ForeignKey($"{nameof(Entities.Wisdom)}{nameof(Entities.Wisdom.Id)}")]
    public virtual Wisdom Wisdom { get; set; } = new();

    [ForeignKey($"{nameof(Entities.Charisma)}{nameof(Entities.Charisma.Id)}")]
    public virtual Charisma Charisma { get; set; } = new();

    [ForeignKey($"{nameof(Entities.ChallengeRating)}{nameof(Entities.ChallengeRating.Id)}")]
    public required virtual ChallengeRating ChallengeRating { get; set; }

    [ForeignKey($"{nameof(Monster)}{nameof(Id)}")]
    public virtual ICollection<MonsterAbility> Abilities { get; set; } = new List<MonsterAbility>();

    [ForeignKey($"{nameof(Monster)}{nameof(Id)}")]
    public virtual ICollection<MonsterAction> Actions { get; set; } = new List<MonsterAction>();

    [ForeignKey($"{nameof(Monster)}{nameof(Id)}")]
    public virtual ICollection<LegendaryAction> LegendaryActions { get; set; } = new List<LegendaryAction>();

    [ForeignKey($"{nameof(Entities.Campaign)}{nameof(Entities.Campaign.Id)}")]
    public required virtual Campaign Campaign { get; set; }

}
