using System.Reflection;
using Csnp.SharedKernel.Application.Abstractions.Events;
using Csnp.SharedKernel.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Csnp.Credential.Infrastructure.Events;

/// <summary>
/// Dispatches domain events to their corresponding handlers.
/// </summary>
public class DomainEventDispatcher : IDomainEventDispatcher
{
    #region -- Fields --

    private readonly IServiceProvider _serviceProvider;

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEventDispatcher"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider used for resolving event handlers.</param>
    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Dispatches the given collection of domain events to their respective handlers.
    /// </summary>
    /// <param name="domainEvents">The domain events to dispatch.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
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
}
