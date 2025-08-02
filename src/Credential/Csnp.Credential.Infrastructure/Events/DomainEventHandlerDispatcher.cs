using System.Reflection;
using Csnp.SharedKernel.Application.Abstractions.Events;
using Csnp.SharedKernel.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Csnp.Credential.Infrastructure.Events;

/// <summary>
/// Dispatches domain events to their corresponding in-process handlers.
/// </summary>
public sealed class DomainEventHandlerDispatcher : IDomainEventHandlerDispatcher
{
    #region -- Implements --

    /// <inheritdoc />
    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            Type handlerType = typeof(IDomainHandler<>).MakeGenericType(domainEvent.GetType());
            IEnumerable<object> handlers = _serviceProvider.GetServices(handlerType)
                                                           .Where(h => h is not null)!;

            foreach (object handler in handlers)
            {
                MethodInfo? handleMethod = handlerType.GetMethod(nameof(IDomainHandler<IDomainEvent>.HandleAsync));
                if (handleMethod != null)
                {
                    await (Task)handleMethod.Invoke(handler, new object[] { domainEvent, cancellationToken })!;
                }
            }
        }
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEventHandlerDispatcher"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider used to resolve event handlers.</param>
    public DomainEventHandlerDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// Provides access to scoped services.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    #endregion
}
