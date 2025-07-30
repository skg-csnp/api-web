using Csnp.Common.Security;
using Csnp.Credential.Api.Middlewares;
using Csnp.Credential.Application;
using Csnp.Credential.Infrastructure;
using Csnp.EventBus.RabbitMQ;

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

        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        builder.Services.AddSingleton<IJwtService, JwtService>();

        builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"));
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
