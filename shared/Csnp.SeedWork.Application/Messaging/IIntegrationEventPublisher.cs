namespace Csnp.SeedWork.Application.Messaging;

public interface IIntegrationEventPublisher
{
    Task PublishAsync(IntegrationEvent integrationEvent);
}
