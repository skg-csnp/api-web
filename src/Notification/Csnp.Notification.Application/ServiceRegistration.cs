using Csnp.Notification.Application.Events;
using Csnp.Notification.Application.Handlers;
using Csnp.SeedWork.Domain.Events;
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
