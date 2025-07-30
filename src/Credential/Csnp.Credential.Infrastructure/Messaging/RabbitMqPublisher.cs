using System.Text;
using Csnp.SeedWork.Application.Messaging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Csnp.Credential.Infrastructure.Messaging;

public class RabbitMqPublisher : IIntegrationEventPublisher
{
    private readonly IConfiguration _configuration;

    public RabbitMqPublisher(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task PublishAsync(IntegrationEvent integrationEvent)
    {
        var settings = new RabbitMqSettings();
        _configuration.GetSection("RabbitMq").Bind(settings);

        var factory = new ConnectionFactory
        {
            HostName = settings.Host,
            Port = settings.Port,
            UserName = settings.Username,
            Password = settings.Password,
            VirtualHost = settings.VirtualHost
        };

        await using IConnection connection = await factory.CreateConnectionAsync();
        await using IChannel channel = await connection.CreateChannelAsync();

        string exchangeName = "user.signup";
        await channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Fanout, durable: true);

        byte[] body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(integrationEvent));

        var props = new BasicProperties();
        await channel.BasicPublishAsync(exchange: exchangeName,
              routingKey: "",
              mandatory: false,
              basicProperties: props,
              body: body);
    }
}
