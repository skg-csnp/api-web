using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Domain.Entities;
using Csnp.SharedKernel.Domain.ValueObjects;
using MediatR;

namespace Csnp.Credential.Application.Commands.Users.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, long>
{
    private readonly IUserWriteRepository _userRepo;

    public CreateUserHandler(IUserWriteRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
            EmailAddress.Create(request.UserName),
            request.Password,
            request.DisplayName
        );
        await _userRepo.AddAsync(user, cancellationToken);
        return user.Id;
    }
}
