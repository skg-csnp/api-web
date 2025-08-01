using Csnp.EventBus.RabbitMQ;
using Csnp.Notification.Infrastructure;
using Csnp.SharedKernel.Configuration.DependencyInjection;
using Csnp.SharedKernel.Configuration.Extensions;
using Csnp.SharedKernel.Configuration.Settings.Email;
using Csnp.SharedKernel.Configuration.Settings.Persistence;
using Csnp.SharedKernel.Configuration.Settings.Storage;

namespace Csnp.Notification.Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services
            .Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"))
            .OverrideWithEnv<RabbitMqSettings>("RabbitMq", "LOC_NOTIFICATION", (original, env) => original.MergeNonDefaultValues(env));

        builder.Services
            .Configure<MinioSettings>(builder.Configuration.GetSection("Minio"))
            .OverrideWithEnv<MinioSettings>("Minio", "LOC_NOTIFICATION", (original, env) => original.MergeNonDefaultValues(env));

        builder.Services
            .Configure<EmailSettings>(builder.Configuration.GetSection("Email"))
            .OverrideWithEnv<EmailSettings>("Email", "LOC_NOTIFICATION", (original, env) => original.MergeNonDefaultValues(env));

        builder.Services
            .Configure<SqlServerSettings>(builder.Configuration.GetSection("Database"))
            .OverrideWithEnv<SqlServerSettings>("Database", "LOC_NOTIFICATION", (original, env) => original.MergeNonDefaultValues(env));

        builder.Services
            .AddApplication()
            .AddInfrastructure();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
