using Csnp.SharedKernel.Application.Abstractions.Events;
using Csnp.SharedKernel.Domain.Events;

namespace Csnp.Credential.Application.Dispatcher;

/// <summary>
/// Coordinates the dispatching of domain events to both in-process handlers and external integration publishers.
/// </summary>
public sealed class CompositeDomainEventDispatcher : ICompositeDomainEventDispatcher
{
    #region -- Implements --

    /// <inheritdoc />
    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        await _handlerDispatcher.DispatchAsync(domainEvents, cancellationToken);
        await _integrationDispatcher.DispatchAsync(domainEvents, cancellationToken);
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositeDomainEventDispatcher"/> class.
    /// </summary>
    /// <param name="handlerDispatcher">Dispatcher that invokes domain event handlers.</param>
    /// <param name="integrationDispatcher">Dispatcher that maps and publishes integration events.</param>
    public CompositeDomainEventDispatcher(
        IDomainEventHandlerDispatcher handlerDispatcher,
        IDomainToIntegrationDispatcher integrationDispatcher)
    {
        _handlerDispatcher = handlerDispatcher;
        _integrationDispatcher = integrationDispatcher;
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// Dispatches domain events to their corresponding domain event handlers.
    /// </summary>
    private readonly IDomainEventHandlerDispatcher _handlerDispatcher;

    /// <summary>
    /// Converts domain events to integration events and dispatches them to external systems.
    /// </summary>
    private readonly IDomainToIntegrationDispatcher _integrationDispatcher;


    #endregion
}
