using FluentValidation;
using MediatR;

namespace Csnp.SeedWork.Application.Behaviors;

/// <summary>
/// Represents a pipeline behavior that uses FluentValidation to validate requests
/// before they are handled by MediatR handlers. If any validation errors are found,
/// a <see cref="ValidationException"/> will be thrown and the pipeline will be short-circuited.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
{
    #region -- Implements --

    /// <summary>
    /// Handles the request by validating it before passing it to the next handler.
    /// </summary>
    /// <param name="request">The incoming request.</param>
    /// <param name="next">The delegate representing the next action in the pipeline.</param>
    /// <param name="cancellationToken">A token for cancelling the operation.</param>
    /// <returns>The response from the next handler in the pipeline.</returns>
    /// <exception cref="ValidationException">Thrown when validation failures are found.</exception>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            FluentValidation.Results.ValidationResult[] validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))
            );

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
        }

        return await next(cancellationToken);
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="validators">A collection of validators to apply to the request.</param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// The collection of validators used to validate the request.
    /// </summary>
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    #endregion
}
