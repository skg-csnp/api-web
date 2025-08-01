using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Csnp.Presentation.Common.Filters;

/// <summary>
/// Validates the incoming model state before executing the action.
/// Returns 400 Bad Request with detailed error messages if validation fails.
/// </summary>
public class ValidationFilter : IActionFilter
{
    #region -- Methods --

    /// <inheritdoc />
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value?.Errors.Any() == true)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            context.Result = new BadRequestObjectResult(new ValidationProblemDetails(errors));
        }
    }

    /// <inheritdoc />
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // No action needed after execution.
    }

    #endregion
}
