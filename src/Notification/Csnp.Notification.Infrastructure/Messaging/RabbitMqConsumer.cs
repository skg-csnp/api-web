using System.Text;
using System.Text.Json;
using Csnp.EventBus.Abstractions;
using Csnp.Notification.Application.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Csnp.Notification.Infrastructure.Messaging;

public class RabbitMqConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;
    private IConnection? _connection;
    private IChannel? _channel;

    public RabbitMqConsumer(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    public override async Task<Task> StartAsync(CancellationToken cancellationToken)
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

        _connection = await factory.CreateConnectionAsync(cancellationToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

        string exchangeName = "user.signup";
        string queueName = "user-signed-up";

        await _channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Fanout, durable: true, cancellationToken: cancellationToken);
        await _channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: cancellationToken);
        await _channel.QueueBindAsync(queue: queueName, exchange: exchangeName, routingKey: "", cancellationToken: cancellationToken);


        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);
            UserSignedUpIntegrationEvent? integrationEvent = JsonSerializer.Deserialize<UserSignedUpIntegrationEvent>(message);

            if (integrationEvent != null)
            {
                using IServiceScope scope = _serviceProvider.CreateScope();
                IIntegrationHandler<UserSignedUpIntegrationEvent> handler = scope.ServiceProvider.GetRequiredService<IIntegrationHandler<UserSignedUpIntegrationEvent>>();
                await handler.HandleAsync(integrationEvent, cancellationToken);
            }

            await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
        };

        await _channel.BasicConsumeAsync(queue: "user-signed-up", autoAck: false, consumer: consumer, cancellationToken: cancellationToken);

        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel != null)
        {
            await _channel.CloseAsync(cancellationToken: cancellationToken);
        }

        if (_connection != null)
        {
            await _connection.CloseAsync(cancellationToken: cancellationToken);
        }

        await base.StopAsync(cancellationToken);
    }

    public override async void Dispose()
    {
        if (_channel != null)
        {
            await _channel.DisposeAsync();
        }

        if (_connection != null)
        {
            await _connection.DisposeAsync();
        }

        base.Dispose();
    }
}
