using Csnp.Common.Security;
using Csnp.Credential.Application.Abstractions.Persistence;
using MediatR;

namespace Csnp.Credential.Application.Commands.Authorizes.SignIn;

public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInResponse>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IJwtService _jwtService;

    public SignInCommandHandler(IUserReadRepository userReadRepository, IJwtService jwtService)
    {
        _userReadRepository = userReadRepository;
        _jwtService = jwtService;
    }

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
}
