using _5eTools.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace _5eTools.API.Configurations;

public static class ApiBehaviorConfigurations
{
    public static IServiceCollection ConfigureInvalidModelStateResponse(this IServiceCollection services)
    {
        return services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage));

                return new BadRequestObjectResult(new ResponseWrapper<object>(errors));
            };
        });
    }
}
