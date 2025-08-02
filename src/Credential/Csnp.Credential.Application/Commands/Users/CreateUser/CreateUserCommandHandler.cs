using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Domain.Entities;
using Csnp.SeedWork.Domain.ValueObjects;
using Csnp.SharedKernel.Application.Abstractions.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Csnp.Credential.Application.Commands.Users.CreateUser;

/// <summary>
/// Handles the <see cref="CreateUserCommand"/> to create a new user.
/// </summary>
public sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, long>
{
    #region -- Implements --

    /// <inheritdoc />
    public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User user = User.CreateByAdmin(
            EmailAddress.Create(request.UserName),
            request.Password,
            request.DisplayName
        );

        await _userRepo.AddAsync(user, cancellationToken);

        _logger.LogInformation("New user created by admin with ID {UserId}", user.Id);

        await _domainEventDispatcher.DispatchAsync(user.DomainEvents, cancellationToken);

        return user.Id;
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserHandler"/> class.
    /// </summary>
    /// <param name="userRepo">The user repository for write operations.</param>
    /// <param name="domainEventDispatcher">The domain event dispatcher.</param>
    /// <param name="logger">The logger for structured logging.</param>
    public CreateUserHandler(
        IUserWriteRepository userRepo,
        ICompositeDomainEventDispatcher domainEventDispatcher,
        ILogger<CreateUserHandler> logger)
    {
        _userRepo = userRepo;
        _domainEventDispatcher = domainEventDispatcher;
        _logger = logger;
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// Repository for writing user data.
    /// </summary>
    private readonly IUserWriteRepository _userRepo;

    /// <summary>
    /// Dispatcher responsible for handling domain events.
    /// </summary>
    private readonly ICompositeDomainEventDispatcher _domainEventDispatcher;

    /// <summary>
    /// Logger for writing structured logs during user creation.
    /// </summary>
    private readonly ILogger<CreateUserHandler> _logger;

    #endregion
}
