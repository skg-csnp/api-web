using Csnp.EventBus.Abstractions;
using Csnp.Notification.Application.Events;
using Csnp.Notification.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Csnp.Notification.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationHandler<UserSignedUpIntegrationEvent>, UserSignedUpIntegrationHandler>();

        return services;
    }
}
