using _5eTools.Data;
using _5eTools.Services;
using Microsoft.EntityFrameworkCore;

namespace _5eTools.API.Configurations;

public static class DependencyConfigurations
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("5eTools")!;

        return services
            .AddDbContextPool<ToolsDbContext>(options => options.UseMySQL(connectionString))
            .AddScoped<ICampaignService, CampaignService>()
            .AddSingleton<ICryptographyService, CryptographyService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IMonsterService, MonsterService>()
            .AddScoped<IPlayerCharacterService, PlayerCharacterService>();
    }
}
