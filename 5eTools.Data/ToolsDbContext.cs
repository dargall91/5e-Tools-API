using _5eTools.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace _5eTools.Data;

public class ToolsDbContext(DbContextOptions<ToolsDbContext> options) : DbContext(options)
{
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Monster> Monsters { get; set; }
    public DbSet<MonsterAbility> MonsterAbilities { get; set; }
    public DbSet<MonsterAction> MonsterActions { get; set; }
    public DbSet<LegendaryAction> LegendaryActions { get; set; }
    public DbSet<ChallengeRating> ChallengeRatings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
    }
}
