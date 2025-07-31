using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Application.Events.Users;
using Csnp.Credential.Domain.Events.Users;
using Csnp.Credential.Infrastructure.Events;
using Csnp.Credential.Infrastructure.Persistence;
using Csnp.Credential.Infrastructure.Persistence.Repositories;
using Csnp.Credential.Infrastructure.UnitOfWork;
using Csnp.EventBus.Abstractions;
using Csnp.EventBus.RabbitMQ;
using Csnp.SeedWork.Application.UnitOfWork;
using Csnp.SharedKernel.Application.Abstractions.Events;
using Csnp.SharedKernel.Domain.Events;
using IdGen;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Csnp.Credential.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        string? connectionString = config.GetConnectionString("Default");

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

        // Register UnitOfWork and Repositories
        services.AddScoped<IUnitOfWork, CredentialUnitOfWork>();
        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();

        // Register ID generator
        services.AddSingleton<IdGenerator>(_ =>
        {
            var epoch = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var structure = new IdStructure(45, 2, 16);
            var options = new IdGeneratorOptions(structure, new DefaultTimeSource(epoch));

            int workerId = int.TryParse(Environment.GetEnvironmentVariable("WORKER_ID"), out int id) ? id : 0;
            return new IdGenerator(workerId, options);
        });

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<IDomainHandler<UserCreatedDomainEvent>, UserCreatedHandler>();
        services.AddScoped<IDomainHandler<UserSignedInDomainEvent>, UserSignedInHandler>();

        services.AddSingleton<IIntegrationEventPublisher, RabbitMqPublisher>();

        return services;
    }
}
