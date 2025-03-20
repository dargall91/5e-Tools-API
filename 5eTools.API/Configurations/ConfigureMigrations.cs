using _5eTools.Data;
using Microsoft.EntityFrameworkCore;

namespace _5eTools.API.Configurations;

public static class ConfigureMigrations
{
    public static WebApplication MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ToolsDbContext>();

        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }

        return app;
    }
}
