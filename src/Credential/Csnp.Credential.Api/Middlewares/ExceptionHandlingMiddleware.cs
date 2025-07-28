using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Csnp.Credential.Api.Middlewares;

public static class ExceptionHandlingMiddleware
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(config =>
        {
            config.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                Exception? exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                if (exception is ValidationException validationException)
                {
                    var errors = validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        );

                    await context.Response.WriteAsJsonAsync(new { errors });
                }
                else
                {
                    var result = new
                    {
                        error = exception?.Message ?? "Unexpected error occurred"
                    };

                    await context.Response.WriteAsJsonAsync(result);
                }
            });
        });
    }
}
