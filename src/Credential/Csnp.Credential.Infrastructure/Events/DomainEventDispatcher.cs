using Csnp.SeedWork.Application.Abstractions.Events;
using Csnp.SeedWork.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Csnp.Credential.Infrastructure.Events;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            Type handlerType = typeof(IDomainHandler<>).MakeGenericType(domainEvent.GetType());
            IEnumerable<object?> handlers = _serviceProvider.GetServices(handlerType);

            foreach (object? handler in handlers)
            {
                System.Reflection.MethodInfo? handleMethod = handlerType.GetMethod("HandleAsync");
                if (handleMethod != null)
                {
                    await (Task)handleMethod.Invoke(handler, new object[] { domainEvent, cancellationToken })!;
                }
            }
        }
    }
}
