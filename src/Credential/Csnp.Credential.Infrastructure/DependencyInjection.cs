using Csnp.Credential.Domain.Interfaces;
using Csnp.Credential.Infrastructure.Persistence.Shared;
using Csnp.Credential.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Csnp.Credential.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Default");

        services.AddDbContext<CredentialDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
