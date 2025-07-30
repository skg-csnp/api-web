using Csnp.EventBus.Abstractions;
using Csnp.EventBus.RabbitMQ;
using Csnp.Notification.Application.Abstractions.Services;
using Csnp.Notification.Application.Events;
using Csnp.Notification.Application.Handlers;
using Csnp.Notification.Infrastructure.Messaging;
using Csnp.Notification.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Csnp.Notification.Infrastructure;

/// <summary>
/// Provides extension methods to register Notification module services into the DI container.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Registers application-level services for the Notification module.
    /// Includes event handlers that contain business logic responding to domain or integration events.
    /// </summary>
    /// <param name="services">The DI service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationHandler<UserSignedUpIntegrationEvent>, UserSignedUpIntegrationHandler>();
        return services;
    }

    /// <summary>
    /// Registers infrastructure-level services for the Notification module.
    /// Includes implementations for external dependencies such as email sending and RabbitMQ subscriptions.
    /// </summary>
    /// <param name="services">The DI service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>(); // sends actual email
        services.AddSingleton<IIntegrationEventMetadata<UserSignedUpIntegrationEvent>, UserSignedUpMetadata>(); // defines RabbitMQ metadata like queue name
        services.AddHostedService<RabbitMqSubscriber<UserSignedUpIntegrationEvent>>(); // background service to consume event from RabbitMQ
        return services;
    }
}
