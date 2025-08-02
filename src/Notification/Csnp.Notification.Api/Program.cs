using Csnp.Notification.Infrastructure;
using Csnp.Presentation.Common.Middlewares;
using Csnp.SharedKernel.Configuration.DependencyInjection;
using Csnp.SharedKernel.Configuration.Extensions;
using Csnp.SharedKernel.Configuration.Settings.Email;
using Csnp.SharedKernel.Configuration.Settings.Messaging;
using Csnp.SharedKernel.Configuration.Settings.Persistence;
using Csnp.SharedKernel.Configuration.Settings.Storage;

namespace Csnp.Notification.Api;

/// <summary>
/// The main entry point of the API application.
/// </summary>
public class Program
{
    /// <summary>
    /// Main method for starting the web application.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add controllers
        builder.Services.AddControllers();

        // Add OpenAPI/Swagger services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add Swagger + OpenAPI extensions
        builder.Services.AddOpenApi();

        // Configuration: Bind and override from environment
        builder.Services
            .Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"))
            .OverrideWithEnv<RabbitMqSettings>("RabbitMq", "LOC_NOTIFICATION", (original, env) => original.MergeNonDefaultValues(env));

        builder.Services
            .Configure<PostgreSqlSettings>(builder.Configuration.GetSection("Database"))
            .OverrideWithEnv<PostgreSqlSettings>("Database", "LOC_NOTIFICATION", (original, env) => original.MergeNonDefaultValues(env));

        builder.Services
            .Configure<MinioSettings>(builder.Configuration.GetSection("Minio"))
            .OverrideWithEnv<MinioSettings>("Minio", "LOC_NOTIFICATION", (original, env) => original.MergeNonDefaultValues(env));

        builder.Services
            .Configure<EmailSettings>(builder.Configuration.GetSection("Email"))
            .OverrideWithEnv<EmailSettings>("Email", "LOC_NOTIFICATION", (original, env) => original.MergeNonDefaultValues(env));

        // Register application and infrastructure layers
        builder.Services
            .AddApplication()
            .AddInfrastructure();

        WebApplication app = builder.Build();

        // Enable Swagger in development
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Add custom exception handler middleware
        app.UseCustomExceptionHandler();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
