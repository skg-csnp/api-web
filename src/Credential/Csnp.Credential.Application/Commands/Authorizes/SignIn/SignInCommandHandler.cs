using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.SharedKernel.Application.Abstractions.Events;
using Csnp.SharedKernel.Application.Abstractions.Events.Security;
using Csnp.SharedKernel.Application.Events;
using MediatR;

namespace Csnp.Credential.Application.Commands.Authorizes.SignIn;

/// <summary>
/// Handles the sign-in process by validating user credentials and generating JWT tokens.
/// </summary>
public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInResponse>
{
    #region -- Properties --

    private readonly IUserReadRepository _userReadRepository;
    private readonly IJwtService _jwtService;
    private readonly IDomainEventDispatcher _dispatcher;

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="SignInCommandHandler"/> class.
    /// </summary>
    /// <param name="userReadRepository">The user read repository.</param>
    /// <param name="jwtService">The JWT service for token generation.</param>
    /// <param name="dispatcher">The domain event dispatcher.</param>
    public SignInCommandHandler(
        IUserReadRepository userReadRepository,
        IJwtService jwtService,
        IDomainEventDispatcher dispatcher)
    {
        _userReadRepository = userReadRepository;
        _jwtService = jwtService;
        _dispatcher = dispatcher;
    }

    /// <inheritdoc />
    public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.User? user = await _userReadRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        bool isPasswordValid = await _userReadRepository.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        user.SignIn();
        await DomainEventHelper.DispatchAndClearAsync(user, _dispatcher, cancellationToken);

        string accessToken = _jwtService.GenerateToken(user.Id, user.DisplayName);
        string refreshToken = _jwtService.GenerateRefreshToken();
        DateTime expiresAt = DateTime.UtcNow.AddMinutes(30);

        return new SignInResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = expiresAt
        };
    }

    #endregion
}
