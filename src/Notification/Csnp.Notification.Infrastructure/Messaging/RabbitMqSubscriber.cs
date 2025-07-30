using System.Text;
using System.Text.Json;
using Csnp.EventBus.Abstractions;
using Csnp.EventBus.RabbitMQ;
using Csnp.Notification.Application.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Csnp.Notification.Infrastructure.Messaging;

/// <summary>
/// Background service to consume RabbitMQ messages and dispatch them to the appropriate integration handler.
/// </summary>
public sealed class RabbitMqSubscriber : BackgroundService
{
    #region -- Overrides --

    /// <inheritdoc />
    protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;

    /// <inheritdoc />
    public override async Task<Task> StartAsync(CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;

        var factory = new ConnectionFactory
        {
            HostName = _settings.Host,
            Port = _settings.Port,
            UserName = _settings.Username,
            Password = _settings.Password,
            VirtualHost = _settings.VirtualHost
        };

        _connection = await factory.CreateConnectionAsync(cancellationToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

        const string exchangeName = "user.signup";
        const string queueName = "user-signed-up";

        await _channel.ExchangeDeclareAsync(
            exchange: exchangeName,
            type: ExchangeType.Fanout,
            durable: true,
            cancellationToken: cancellationToken);

        await _channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: cancellationToken);

        await _channel.QueueBindAsync(
            queue: queueName,
            exchange: exchangeName,
            routingKey: string.Empty,
            cancellationToken: cancellationToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += OnMessageReceivedAsync;

        await _channel.BasicConsumeAsync(
            queue: queueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: cancellationToken);

        return base.StartAsync(cancellationToken);
    }

    /// <inheritdoc />
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel is not null)
        {
            await _channel.CloseAsync(cancellationToken: cancellationToken);
        }

        if (_connection is not null)
        {
            await _connection.CloseAsync(cancellationToken: cancellationToken);
        }

        await base.StopAsync(cancellationToken);
    }

    /// <inheritdoc />
    public override async void Dispose()
    {
        if (_channel is not null)
        {
            await _channel.DisposeAsync();
        }

        if (_connection is not null)
        {
            await _connection.DisposeAsync();
        }

        base.Dispose();
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMqSubscriber"/> class.
    /// </summary>
    /// <param name="serviceProvider">Service provider to resolve scoped services.</param>
    /// <param name="options">RabbitMQ settings injected via <see cref="IOptions{TOptions}"/>.</param>
    /// <param name="logger">Logger instance for diagnostic messages.</param>
    public RabbitMqSubscriber(
        IServiceProvider serviceProvider,
        IOptions<RabbitMqSettings> options,
        ILogger<RabbitMqSubscriber> logger)
    {
        _serviceProvider = serviceProvider;
        _settings = options.Value;
        _logger = logger;
    }

    /// <summary>
    /// Handles the received RabbitMQ message.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="ea">The event arguments containing message data.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task OnMessageReceivedAsync(object sender, BasicDeliverEventArgs ea)
    {
        try
        {
            byte[] body = ea.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);

            UserSignedUpIntegrationEvent? integrationEvent =
                JsonSerializer.Deserialize<UserSignedUpIntegrationEvent>(message);

            if (integrationEvent is not null)
            {
                using IServiceScope scope = _serviceProvider.CreateScope();

                IIntegrationHandler<UserSignedUpIntegrationEvent> handler =
                    scope.ServiceProvider.GetRequiredService<IIntegrationHandler<UserSignedUpIntegrationEvent>>();

                await handler.HandleAsync(integrationEvent, _cancellationToken);
            }

            if (_channel is not null)
            {
                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing integration event.");
        }
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// Provides access to scoped services.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Settings required to connect to RabbitMQ.
    /// </summary>
    private readonly RabbitMqSettings _settings;

    /// <summary>
    /// Logger instance for logging messages and errors.
    /// </summary>
    private readonly ILogger<RabbitMqSubscriber> _logger;

    /// <summary>
    /// Represents the RabbitMQ connection.
    /// </summary>
    private IConnection? _connection;

    /// <summary>
    /// Represents the RabbitMQ channel.
    /// </summary>
    private IChannel? _channel;

    /// <summary>
    /// Cancellation token used for stopping the service.
    /// </summary>
    private CancellationToken _cancellationToken;

    #endregion
}
