using Microsoft.Extensions.DependencyInjection;

namespace Csnp.Credential.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));
        return services;
    }
}
