using _5eTools.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls($"http://*:{(builder.Environment.IsProduction() ? "5000" : "7275")}");
builder.Services
    .AddControllerConfigurations()
    .AddSwaggerConfigurations()
    .AddDependencies(builder.Configuration)
    .ConfigureInvalidModelStateResponse();

var app = builder.Build();

app
    .ConfigureControllers()
    .ConfigureSwagger()
    .ConfigureExceptions()
    .MigrateDatabase();

await app.RunAsync();
