using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Application.Commands.Users.CreateUser;
using Csnp.Credential.Application.Dispatcher;
using Csnp.Credential.Application.Events.Users;
using Csnp.Credential.Domain.Events.Users;
using Csnp.Credential.Infrastructure.Events;
using Csnp.Credential.Infrastructure.Persistence;
using Csnp.Credential.Infrastructure.Persistence.Constants;
using Csnp.Credential.Infrastructure.Persistence.Repositories;
using Csnp.EventBus.Abstractions;
using Csnp.EventBus.RabbitMQ;
using Csnp.SharedKernel.Application.Abstractions.Events;
using Csnp.SharedKernel.Application.Behaviors;
using Csnp.SharedKernel.Configuration.Settings.Persistence;
using Csnp.SharedKernel.Domain.Events;
using FluentValidation;
using IdGen;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Csnp.Credential.Infrastructure;

/// <summary>
/// Provides extension methods to register Credential module services into the DI container.
/// </summary>
public static class ServiceRegistration
{
    #region -- Methods --

    /// <summary>
    /// Adds application-layer services such as MediatR handlers, validators, and pipeline behaviors.
    /// </summary>
    /// <param name="services">The service collection to register dependencies into.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));
        services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<ICompositeDomainEventDispatcher, CompositeDomainEventDispatcher>();

        return services;
    }

    /// <summary>
    /// Registers infrastructure-level services such as database context, identity, repositories, 
    /// ID generation, domain event handlers, and integration event publisher.
    /// </summary>
    /// <param name="services">The DI service collection.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<CredentialDbContext>((sp, options) =>
        {
            PostgreSqlSettings settings = sp.GetRequiredService<IOptions<PostgreSqlSettings>>().Value;
            string connStr = settings.ToConnectionString();
            options.UseNpgsql(connStr, connOptions =>
            {
                connOptions.MigrationsHistoryTable("__ef_migrations_history", SchemaNames.Default);
            });
        });

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
        services.AddSingleton(_ =>
        {
            DateTime epoch = new(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            IdStructure structure = new(45, 2, 16);
            IdGeneratorOptions options = new(structure, new DefaultTimeSource(epoch));
            int workerId = int.TryParse(Environment.GetEnvironmentVariable("WORKER_ID"), out int id) ? id : 0;
            return new IdGenerator(workerId, options);
        });

        services.AddScoped<IDomainHandler<UserCreatedDomainEvent>, UserCreatedHandler>();
        services.AddScoped<IDomainHandler<UserSignedInDomainEvent>, UserSignedInHandler>();

        services.AddSingleton<IIntegrationEventPublisher, RabbitMqPublisher>();

        services.AddScoped<IDomainEventHandlerDispatcher, DomainEventHandlerDispatcher>();
        services.AddScoped<IDomainToIntegrationDispatcher, DomainToIntegrationDispatcher>();

        return services;
    }

    #endregion
}
