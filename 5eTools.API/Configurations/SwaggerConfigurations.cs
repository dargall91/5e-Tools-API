using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace _5eTools.API.Configurations;

public static class SwaggerConfigurations
{
    public static IServiceCollection AddSwaggerConfigurations(this IServiceCollection services)
    {
        return services
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
            .AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(Program).Assembly.GetName().Name}.xml"));
            });
    }

    public static WebApplication ConfigureSwagger(this WebApplication app)
    {
        if (!app.Environment.IsProduction())
        {
            app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    IApiVersionDescriptionProvider provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

                    foreach (var group in provider.ApiVersionDescriptions.Select(x => x.GroupName))
                    {
                        var path = $"swagger/{group}/swagger.json";
                        var name = group.ToUpperInvariant();

                        options.SwaggerEndpoint(path, name);
                    }
                });
        }

        return app;
    }

    public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        private static OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var deprecatedText = description.IsDeprecated
                ? " - This API version is deprecated"
                : string.Empty;

            var info = new OpenApiInfo
            {
                Title = "5e Tools API",
                Version = description.ApiVersion.ToString(),
                Description = "An API for managing 5th Edition D&D Campaigns" + deprecatedText
            };

            return info;
        }
    }
}
