using Csnp.Credential.Api.Middlewares;
using Csnp.Credential.Application;
using Csnp.Credential.Infrastructure;
using Csnp.EventBus.RabbitMQ;
using Csnp.Security.Infrastructure;
using Csnp.SharedKernel.Application.Abstractions.Events.Security;
using Csnp.SharedKernel.Configuration.DependencyInjection;
using Csnp.SharedKernel.Configuration.Extensions;
using Csnp.SharedKernel.Configuration.Settings.Persistence;
using Csnp.SharedKernel.Configuration.Settings.Security;

namespace Csnp.Credential.Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services
            .Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"))
            .OverrideWithEnv<JwtSettings>("Jwt", "LOC_CREDENTIAL", (original, env) => original.MergeNonDefaultValues(env));
        builder.Services.AddSingleton<IJwtService, JwtService>();

        builder.Services
            .Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"))
            .OverrideWithEnv<RabbitMqSettings>("RabbitMq", "LOC_CREDENTIAL", (original, env) => original.MergeNonDefaultValues(env));

        builder.Services
            .Configure<SqlServerSettings>(builder.Configuration.GetSection("Database"))
            .OverrideWithEnv<SqlServerSettings>("Database", "LOC_CREDENTIAL", (original, env) => original.MergeNonDefaultValues(env));

        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCustomExceptionHandler();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
