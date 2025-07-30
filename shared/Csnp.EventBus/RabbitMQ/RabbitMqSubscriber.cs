using System.Text;
using System.Text.Json;
using Csnp.EventBus.Abstractions;
using Csnp.EventBus.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Csnp.EventBus.RabbitMQ;

/// <summary>
/// Generic RabbitMQ subscriber that listens to a specific integration event and dispatches it to the corresponding handler.
/// </summary>
/// <typeparam name="TEvent">Type of the integration event to handle.</typeparam>
public sealed class RabbitMqSubscriber<TEvent> : BackgroundService
    where TEvent : IntegrationEvent
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

        await _channel.ExchangeDeclareAsync(
            exchange: _metadata.ExchangeName,
            type: ExchangeType.Fanout,
            durable: true,
            cancellationToken: cancellationToken);

        await _channel.QueueDeclareAsync(
            queue: _metadata.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: cancellationToken);

        await _channel.QueueBindAsync(
            queue: _metadata.QueueName,
            exchange: _metadata.ExchangeName,
            routingKey: string.Empty,
            cancellationToken: cancellationToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += _handleReceivedMessageAsync;

        await _channel.BasicConsumeAsync(
            queue: _metadata.QueueName,
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
    /// Initializes a new instance of the <see cref="RabbitMqSubscriber{TEvent}"/> class.
    /// </summary>
    /// <param name="serviceProvider">Service provider to resolve scoped services.</param>
    /// <param name="options">RabbitMQ settings injected via <see cref="IOptions{TOptions}"/>.</param>
    /// <param name="metadata">Queue and exchange metadata for the integration event.</param>
    /// <param name="logger">Logger instance for diagnostic messages.</param>
    public RabbitMqSubscriber(
        IServiceProvider serviceProvider,
        IOptions<RabbitMqSettings> options,
        IIntegrationEventMetadata<TEvent> metadata,
        ILogger<RabbitMqSubscriber<TEvent>> logger)
    {
        _serviceProvider = serviceProvider;
        _settings = options.Value;
        _metadata = metadata;
        _logger = logger;
    }

    /// <summary>
    /// Handles the received RabbitMQ message for the subscribed integration event.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="ea">The event arguments containing message data.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task _handleReceivedMessageAsync(object sender, BasicDeliverEventArgs ea)
    {
        try
        {
            byte[] body = ea.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);

            TEvent? integrationEvent = JsonSerializer.Deserialize<TEvent>(message);

            if (integrationEvent is not null)
            {
                using IServiceScope scope = _serviceProvider.CreateScope();

                IIntegrationHandler<TEvent> handler =
                    scope.ServiceProvider.GetRequiredService<IIntegrationHandler<TEvent>>();

                await handler.HandleAsync(integrationEvent, _cancellationToken);
            }

            if (_channel is not null)
            {
                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing integration event of type {EventType}", typeof(TEvent).Name);
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
    /// Metadata that defines the queue and exchange names for the event.
    /// </summary>
    private readonly IIntegrationEventMetadata<TEvent> _metadata;

    /// <summary>
    /// Logger instance for logging messages and errors.
    /// </summary>
    private readonly ILogger<RabbitMqSubscriber<TEvent>> _logger;

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
