using Csnp.Notification.Application.Abstractions.Services;
using Csnp.Notification.Infrastructure.Messaging;
using Csnp.Notification.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Csnp.Notification.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddHostedService<RabbitMqConsumer>();
        return services;
    }
}
