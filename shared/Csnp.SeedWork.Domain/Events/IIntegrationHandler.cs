namespace Csnp.SeedWork.Domain.Events;

public interface IIntegrationHandler<in TEvent>
{
    Task HandleAsync(TEvent integrationEvent, CancellationToken cancellationToken = default);
}
