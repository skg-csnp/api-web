using Csnp.Credential.Application.Commands.Users.CreateUser;
using Csnp.Credential.Application.Dispatcher;
using Csnp.SeedWork.Application.Abstractions.Events;
using Csnp.SeedWork.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Csnp.Credential.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register MediatR handlers from this assembly
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));

        // Register all validators from this assembly (only need to specify one type from the assembly)
        services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

        // Register pipeline validation behavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        return services;
    }
}
