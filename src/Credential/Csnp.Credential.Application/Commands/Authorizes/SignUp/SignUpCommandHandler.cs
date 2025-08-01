using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Domain.Entities;
using Csnp.SeedWork.Domain.ValueObjects;
using Csnp.SharedKernel.Application.Abstractions.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Csnp.Credential.Application.Commands.Authorizes.SignUp;

/// <summary>
/// Handles the <see cref="SignUpCommand"/> to register a new user.
/// </summary>
public sealed class SignUpCommandHandler : IRequestHandler<SignUpCommand, long>
{
    #region -- Implements --

    /// <summary>
    /// Handles the sign-up command.
    /// </summary>
    /// <param name="request">The sign-up request data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The ID of the newly registered user.</returns>
    /// <exception cref="InvalidOperationException">Thrown when a user with the same email already exists.</exception>
    public async Task<long> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        User? existingUser = await _userReadRepository.GetByUserNameAsync(request.Email, cancellationToken);
        if (existingUser is not null)
        {
            throw new InvalidOperationException("User already exists with the given email.");
        }

        EmailAddress email = EmailAddress.Create(request.Email);
        User user = User.Create(email, request.Password, request.DisplayName);

        await _userWriteRepository.AddAsync(user, cancellationToken);

        _logger.LogInformation("New user signed up with ID {UserId}", user.Id);

        await _domainEventDispatcher.DispatchAsync(user.DomainEvents, cancellationToken);

        return user.Id;
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="SignUpCommandHandler"/> class.
    /// </summary>
    /// <param name="userReadRepository">The user read repository.</param>
    /// <param name="userWriteRepository">The user write repository.</param>
    /// <param name="domainEventDispatcher">The domain event dispatcher.</param>
    /// <param name="logger">The logger instance.</param>
    public SignUpCommandHandler(
        IUserReadRepository userReadRepository,
        IUserWriteRepository userWriteRepository,
        IDomainEventDispatcher domainEventDispatcher,
        ILogger<SignUpCommandHandler> logger)
    {
        _userReadRepository = userReadRepository;
        _userWriteRepository = userWriteRepository;
        _domainEventDispatcher = domainEventDispatcher;
        _logger = logger;
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// Provides access to user read operations.
    /// </summary>
    private readonly IUserReadRepository _userReadRepository;

    /// <summary>
    /// Provides access to user write operations.
    /// </summary>
    private readonly IUserWriteRepository _userWriteRepository;

    /// <summary>
    /// Dispatches domain events raised during user creation.
    /// </summary>
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    /// <summary>
    /// Logger for writing structured logs during sign-up process.
    /// </summary>
    private readonly ILogger<SignUpCommandHandler> _logger;

    #endregion
}
