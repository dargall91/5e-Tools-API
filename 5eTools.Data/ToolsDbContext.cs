using _5eTools.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace _5eTools.Data;

public class ToolsDbContext(DbContextOptions<ToolsDbContext> options) : DbContext(options)
{
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Monster> Monsters { get; set; }
    public DbSet<ChallengeRating> ChallengeRatings { get; set; }
    public DbSet<PlayerCharacter> PlayerCharacters { get; set; }
    public DbSet<Subclass> Subclasses { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<ExhaustionLevel> ExhaustionLevels { get; set; }
    public DbSet<SpellSlots> SpellSlots { get; set; }
    public DbSet<WarlockSpellSlots> WarlockSpellSlots { get; set; }
    public DbSet<ProficiencyBonus> ProficiencyBonuses { get; set; }
    public DbSet<StressStatus> StressStatuses { get; set; }
    public DbSet<PrimalCompanionType> PrimalCompanionTypes { get; set; }
    public DbSet<Encounter> Encounters { get; set; }
    public DbSet<EncounterXpThreshold> EncounterXpThresholds { get; set; }
    public DbSet<Music> Music { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.HasMany(x => x.Subclasses).WithMany(x => x.Campaigns).UsingEntity($"{nameof(Campaign)}{nameof(Subclass)}");
        });

        modelBuilder.Entity<Monster>(entity =>
        {
            entity.HasOne(x => x.Strength).WithOne();
            entity.HasOne(x => x.Dexterity).WithOne();
            entity.HasOne(x => x.Constitution).WithOne();
            entity.HasOne(x => x.Intelligence).WithOne();
            entity.HasOne(x => x.Wisdom).WithOne();
            entity.HasOne(x => x.Charisma).WithOne();

            entity.HasOne(x => x.ChallengeRating).WithMany();

            entity.HasMany(x => x.Abilities).WithOne();
            entity.HasMany(x => x.Actions).WithOne();
            entity.HasMany(x => x.LegendaryActions).WithOne();

            entity.HasOne(x => x.Campaign).WithMany();
        });

        modelBuilder.Entity<PlayerCharacter>(entity =>
        {
            entity.HasOne(x => x.Strength).WithOne();
            entity.HasOne(x => x.Dexterity).WithOne();
            entity.HasOne(x => x.Constitution).WithOne();
            entity.HasOne(x => x.Intelligence).WithOne();
            entity.HasOne(x => x.Wisdom).WithOne();
            entity.HasOne(x => x.Charisma).WithOne();

            entity.HasOne(x => x.Resolve).WithOne();
            entity.HasOne(x => x.Stress).WithOne();

            entity.HasOne(x => x.UsedSpellSlots).WithOne();

            entity.HasOne(x => x.Campaign).WithMany();
            entity.HasOne(x => x.User).WithMany();
        });

        modelBuilder.Entity<CharacterClass>(entity =>
        {
            entity.HasOne<PlayerCharacter>().WithMany(x => x.CharacterClasses);
            entity.HasOne(x => x.Subclass).WithMany();
            entity.HasOne(x => x.PrimalCompanion).WithOne();
        });

        modelBuilder.Entity<Subclass>(entity =>
        {
            entity.HasOne(x => x.Class).WithMany(x => x.Subclasses);
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasOne(x => x.CasterType).WithMany();
        });

        modelBuilder.Entity<PrimalCompanion>(entity =>
        {
            entity.HasOne(x => x.PrimalCompanionType).WithMany();
        });

        modelBuilder.Entity<StressStatus>(entity =>
        {
            entity.HasOne(x => x.StressType).WithMany();
        });

        modelBuilder.Entity<EncounterMonster>(entity =>
        {
            entity.HasOne(x => x.Encounter).WithMany(x => x.EncounterMonsters);
            entity.HasOne(x => x.Monster).WithMany();
        });
    }
}
