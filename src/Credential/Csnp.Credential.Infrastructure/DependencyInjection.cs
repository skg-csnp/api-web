using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Infrastructure.Persistence.Repositories;
using Csnp.Credential.Infrastructure.Persistence.Shared;
using IdGen;
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

        // Add ASP.NET Identity services
        services
            .AddIdentityCore<UserEntity>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
            })
            .AddRoles<RoleEntity>()
            .AddEntityFrameworkStores<CredentialDbContext>();

        // Register repositories
        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();

        // Register ID generator
        services.AddSingleton<IdGenerator>(_ =>
        {
            var epoch = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var structure = new IdStructure(45, 2, 16);
            var options = new IdGeneratorOptions(structure, new DefaultTimeSource(epoch));

            var workerId = int.TryParse(Environment.GetEnvironmentVariable("WORKER_ID"), out var id) ? id : 0;
            return new IdGenerator(workerId, options);
        });

        return services;
    }
}
