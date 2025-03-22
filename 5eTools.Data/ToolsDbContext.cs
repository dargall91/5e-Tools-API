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
}
