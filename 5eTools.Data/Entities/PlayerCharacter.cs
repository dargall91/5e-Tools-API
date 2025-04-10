using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(PlayerCharacter))]
public class PlayerCharacter
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public int BaseArmorClass { get; set; }

    public int ArmorClassBonus { get; set; }

    public int Damage { get; set; }

    public int HitPointMaximum { get; set; }

    public int TemporaryHitPoints { get; set; }

    public int MaxHitPointReduction { get; set; }

    public int DeathSaveFailures { get; set; }

    public int DeathSaveSuccesses { get; set; }

    public bool ToughFeat { get; set; }

    public bool IsDead { get; set; }

    public bool IsCombatant { get; set; }

    public bool DwarvenToughness { get; set; }

    public int InitiativeRoll { get; set; }

    public int InitiativeBonus { get; set; }

    [ForeignKey($"{nameof(Entities.SpellSlots)}{nameof(Entities.SpellSlots.Id)}")]
    public virtual SpellSlots? SpellSlots { get; set; }

    [ForeignKey($"{nameof(Entities.WarlockSpellSlots)}{nameof(Entities.WarlockSpellSlots.Id)}")]
    public virtual WarlockSpellSlots? WarlockSpellSlots { get; set; }

    [ForeignKey($"{nameof(Entities.ProficiencyBonus)}{nameof(Entities.ProficiencyBonus.Id)}")]
    public virtual required ProficiencyBonus ProficiencyBonus { get; set; }

    [ForeignKey($"{nameof(Entities.ExhaustionLevel)}{nameof(Entities.ExhaustionLevel.Id)}")]
    public virtual ExhaustionLevel? ExhaustionLevel { get; set; }

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

    [ForeignKey($"{nameof(Entities.Resolve)}{nameof(Entities.Resolve.Id)}")]
    public virtual Resolve? Resolve { get; set; }

    [ForeignKey($"{nameof(Entities.Stress)}{nameof(Entities.Stress.Id)}")]
    public Stress? Stress { get; set; }

    [ForeignKey($"{nameof(Entities.UsedSpellSlots)}{nameof(Entities.UsedSpellSlots.Id)}")]
    public UsedSpellSlots? UsedSpellSlots { get; set; }

    [ForeignKey($"{nameof(PlayerCharacter)}{nameof(Id)}")]
    public virtual ICollection<CharacterClass> CharacterClasses { get; set; } = new List<CharacterClass>();

    [ForeignKey($"{nameof(Entities.Campaign)}{nameof(Entities.Campaign.Id)}")]
    public required virtual Campaign Campaign { get; set; }

    [ForeignKey($"{nameof(Entities.User)}{nameof(Entities.User.Id)}")]
    public required virtual User User { get; set; }
}
