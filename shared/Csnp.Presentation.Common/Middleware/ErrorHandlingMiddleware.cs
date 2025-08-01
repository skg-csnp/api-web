using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Csnp.Presentation.Common.Middlewares;

/// <summary>
/// Provides a centralized exception handling middleware for ASP.NET Core applications.
/// </summary>
public static class ExceptionHandlingMiddleware
{
    #region -- Methods --

    /// <summary>
    /// Adds a custom exception handler to the application pipeline.  
    /// Automatically formats <see cref="ValidationException"/> as a list of field-level errors,
    /// or returns a general error message for unexpected exceptions.
    /// </summary>
    /// <param name="app">The application builder to configure.</param>
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
                    Dictionary<string, string[]> errors = validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            group => group.Key,
                            group => group.Select(e => e.ErrorMessage).ToArray()
                        );

                    await context.Response.WriteAsJsonAsync(new { errors });
                }
                else
                {
                    object result = new
                    {
                        error = exception?.Message ?? "Unexpected error occurred"
                    };

                    await context.Response.WriteAsJsonAsync(result);
                }
            });
        });
    }

    #endregion
}
