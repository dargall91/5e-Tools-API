using System.Text;
using System.Text.Json;
using _5eTools.API.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;

namespace _5eTools.API.Configurations;

public static class ExceptionConfigurations
{
    public static WebApplication ConfigureExceptions(this WebApplication app)
    {
        if (!app.Environment.IsProduction())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            Stream? originalBodyStream = null;

            app
                .Use((context, next) =>
                {
                    //keep reference to original stream as other middleware could modify it
                    originalBodyStream = context.Response.Body;

                    return next();
                })
                .UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context =>
                    {
                        var exception = context.Features.GetRequiredFeature<IExceptionHandlerPathFeature>().Error;

                        WriteResponse(context, originalBodyStream!, exception);

                        return Task.CompletedTask;
                    }
                });
        }

        return app;
    }

    private static void WriteResponse(HttpContext context, Stream stream, Exception exception)
    {
        using var responseBody = new MemoryStream();

        context.Response.Body = responseBody;
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        responseBody.CopyToAsync(stream);

        var response = new ResponseWrapper<object>(exception.CompleteMessage());

        var bodyContent = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response));

        stream.Write(bodyContent, 0, bodyContent.Length);
    }

    private static string CompleteMessage(this Exception ex) => $"{ex.Message}\r\n{ex.InnerException?.CompleteMessage()}";
}
