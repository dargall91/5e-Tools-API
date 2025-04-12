using _5eTools.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

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
    .MigrateDatabase()
    .UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

await app.RunAsync();
