using Csnp.EventBus.Abstractions;
using Csnp.EventBus.RabbitMQ;
using Csnp.Notification.Application.Abstractions.Services;
using Csnp.Notification.Application.Events;
using Csnp.Notification.Application.Handlers;
using Csnp.Notification.Infrastructure.Messaging;
using Csnp.Notification.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Csnp.Notification.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddSingleton<IIntegrationEventMetadata<UserSignedUpIntegrationEvent>, UserSignedUpMetadata>();
        services.AddScoped<IIntegrationHandler<UserSignedUpIntegrationEvent>, UserSignedUpIntegrationHandler>();
        services.AddHostedService<RabbitMqSubscriber<UserSignedUpIntegrationEvent>>();
        return services;
    }
}
