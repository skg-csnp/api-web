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

        const string expectedTemplate = "common-email-otp.html";
        const string expectedEmail = "test@example.com";

        mockEmailService
            .Setup(es => es.SendEmailAsync(
                expectedTemplate,
                expectedEmail,
                It.IsAny<object>()))
            .Returns(Task.CompletedTask);

        var handler = new UserSignedUpIntegrationHandler(mockLogger.Object, mockEmailService.Object);

        var integrationEvent = new UserSignedUpIntegrationEvent
        {
            UserId = 1,
            Email = expectedEmail
        };

        // Act
        await handler.HandleAsync(integrationEvent);

        // Assert
        mockEmailService.Verify(es =>
            es.SendEmailAsync(expectedTemplate, expectedEmail, It.IsAny<object>()),
            Times.Once);

        mockLogger.Verify(logger =>
            logger.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) =>
                    v != null && v.ToString()!.Contains("Send welcome email")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
