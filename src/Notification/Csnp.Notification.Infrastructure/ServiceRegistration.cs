using Csnp.EventBus.Abstractions;
using Csnp.EventBus.RabbitMQ;
using Csnp.Notification.Application.Abstractions.Persistence;
using Csnp.Notification.Application.Abstractions.Services;
using Csnp.Notification.Application.Commands.EmailLogs.CreateEmailLog;
using Csnp.Notification.Application.Events;
using Csnp.Notification.Application.Handlers;
using Csnp.Notification.Infrastructure.Messaging;
using Csnp.Notification.Infrastructure.Persistence;
using Csnp.Notification.Infrastructure.Persistence.Constants;
using Csnp.Notification.Infrastructure.Persistence.Repositories;
using Csnp.Notification.Infrastructure.Services;
using Csnp.SharedKernel.Application.Behaviors;
using Csnp.SharedKernel.Configuration.Settings.Persistence;
using Csnp.SharedKernel.Configuration.Settings.Storage;
using FluentValidation;
using IdGen;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;

namespace Csnp.Notification.Infrastructure;

/// <summary>
/// Provides extension methods to register Notification module services into the DI container.
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
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateEmailLogCommand).Assembly));
        services.AddValidatorsFromAssemblyContaining<CreateEmailLogCommandValidator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<IIntegrationHandler<UserSignedUpIntegrationEvent>, UserSignedUpIntegrationHandler>();
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
        services.AddDbContext<NotificationDbContext>((sp, options) =>
        {
            PostgreSqlSettings settings = sp.GetRequiredService<IOptions<PostgreSqlSettings>>().Value;
            string connStr = settings.ToConnectionString();
            options.UseNpgsql(connStr, connOptions =>
            {
                connOptions.MigrationsHistoryTable("__ef_migrations_history", SchemaNames.Default);
            });
        });

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEmailTemplateRenderer, EmailTemplateRenderer>();
        services.AddScoped<IMinioTemplateLoader, MinioTemplateLoader>();

        // Register repositories
        services.AddScoped<IEmailLogWriteRepository, EmailLogWriteRepository>();

        // Register ID generator
        services.AddSingleton(_ =>
        {
            DateTime epoch = new(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            IdStructure structure = new(45, 2, 16);
            IdGeneratorOptions options = new(structure, new DefaultTimeSource(epoch));
            int workerId = int.TryParse(Environment.GetEnvironmentVariable("WORKER_ID"), out int id) ? id : 0;
            return new IdGenerator(workerId, options);
        });

        services.AddSingleton<IIntegrationEventMetadata<UserSignedUpIntegrationEvent>, UserSignedUpMetadata>();
        services.AddHostedService<RabbitMqSubscriber<UserSignedUpIntegrationEvent>>();

        services.AddSingleton(sp =>
        {
            MinioSettings settings = sp.GetRequiredService<IOptions<MinioSettings>>().Value;
            return new MinioClient()
                .WithEndpoint(settings.Endpoint)
                .WithCredentials(settings.AccessKey, settings.SecretKey)
                .WithSSL(settings.Secure)
                .Build();
        });

        return services;
    }

    #endregion
}
