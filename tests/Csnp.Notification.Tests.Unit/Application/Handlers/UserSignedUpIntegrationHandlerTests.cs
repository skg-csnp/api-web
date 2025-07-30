using Csnp.Notification.Application.Abstractions.Services;
using Csnp.Notification.Application.Events;
using Csnp.Notification.Application.Handlers;
using Microsoft.Extensions.Logging;
using Moq;

namespace Csnp.Notification.Tests.Unit.Application.Handlers;

public class UserSignedUpIntegrationHandlerTests
{
    [Fact]
    public async Task HandleAsync_Should_Send_WelcomeEmail_And_Log_Info()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<UserSignedUpIntegrationHandler>>();
        var mockEmailService = new Mock<IEmailService>();
        var handler = new UserSignedUpIntegrationHandler(mockLogger.Object, mockEmailService.Object);

        var @event = new UserSignedUpIntegrationEvent
        {
            UserId = 1,
            Email = "test@example.com"
        };

        // Act
        await handler.HandleAsync(@event);

        // Assert
        mockEmailService.Verify(es =>
            es.SendWelcomeEmail("test@example.com"), Times.Once);

        mockLogger.Verify(l =>
            l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) =>
                    v != null && v.ToString()!.Contains("Send welcome email")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
