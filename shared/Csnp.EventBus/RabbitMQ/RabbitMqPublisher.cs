using System.Text;
using System.Text.Json;
using Csnp.EventBus.Abstractions;
using Csnp.EventBus.Events;
using Csnp.SharedKernel.Configuration.Settings.Messaging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Csnp.EventBus.RabbitMQ;

/// <summary>
/// Publishes integration events to RabbitMQ using the fanout exchange type.
/// </summary>
public class RabbitMqPublisher : IIntegrationEventPublisher
{
    private readonly RabbitMqSettings _settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMqPublisher"/> class.
    /// </summary>
    /// <param name="options">The RabbitMQ settings provided via IOptions.</param>
    public RabbitMqPublisher(IOptions<RabbitMqSettings> options)
    {
        _settings = options.Value;
    }

    /// <summary>
    /// Publishes the specified integration event to the configured RabbitMQ exchange.
    /// </summary>
    /// <param name="integrationEvent">The integration event to be published.</param>
    public async Task PublishAsync(IntegrationEvent integrationEvent)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.Host,
            Port = _settings.Port,
            UserName = _settings.Username,
            Password = _settings.Password,
            VirtualHost = _settings.VirtualHost
        };

        await using IConnection connection = await factory.CreateConnectionAsync();
        await using IChannel channel = await connection.CreateChannelAsync();

        // TODO: In production, consider mapping event type to exchange dynamically
        const string exchangeName = "user.signup";

        await channel.ExchangeDeclareAsync(
            exchange: exchangeName,
            type: ExchangeType.Fanout,
            durable: true
        );

        string message = JsonSerializer.Serialize(integrationEvent, integrationEvent.GetType());
        byte[] body = Encoding.UTF8.GetBytes(message);

        var props = new BasicProperties();

        await channel.BasicPublishAsync(
            exchange: exchangeName,
            routingKey: "",
            mandatory: false,
            basicProperties: props,
            body: body
        );
    }
}
