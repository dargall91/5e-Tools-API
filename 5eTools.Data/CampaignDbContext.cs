using _5eTools.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace _5eTools.Data;

public class CampaignDbContext(DbContextOptions<CampaignDbContext> options) : DbContext(options)
{
    public DbSet<Campaign> Campaigns { get; set; }
}
