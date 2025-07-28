using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Domain.Entities;
using Csnp.SharedKernel.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Csnp.Credential.Application.Commands.Authorizes.SignUp;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, long>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly ILogger<SignUpCommandHandler> _logger;

    public SignUpCommandHandler(
        IUserReadRepository userReadRepository,
        IUserWriteRepository userWriteRepository,
        ILogger<SignUpCommandHandler> logger)
    {
        _userReadRepository = userReadRepository;
        _userWriteRepository = userWriteRepository;
        _logger = logger;
    }

    public async Task<long> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userReadRepository.GetByUserNameAsync(request.Email, cancellationToken);
        if (existingUser is not null)
        {
            throw new ApplicationException("User already exists with the given email.");
        }

        var email = EmailAddress.Create(request.Email);
        var user = User.Create(email, request.Password, request.DisplayName);

        await _userWriteRepository.AddAsync(user, cancellationToken);

        _logger.LogInformation("New user signed up with ID {UserId}", user.Id);

        return user.Id;
    }
}
