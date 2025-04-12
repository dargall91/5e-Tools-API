using System.Text.Json;
using Asp.Versioning;

namespace _5eTools.API.Configurations;

public static class ControllerConfiguration
{
    public static IServiceCollection AddControllerConfigurations(this IServiceCollection services)
    {
        services
            .AddCors()
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.IncludeFields = true;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

        services
            .AddEndpointsApiExplorer()
            .AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }

    public static WebApplication ConfigureControllers(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.MapControllers();

        return app;
    }
}
