using Csnp.Credential.Domain.Entities;
using Csnp.Credential.Domain.Interfaces;
using MediatR;

namespace Csnp.Credential.Application.Commands.Users;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, long>
{
    private readonly IUserRepository _userRepo;

    public CreateUserHandler(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User { Id = 1, UserName = request.UserName, DisplayName = request.DisplayName };
        await _userRepo.AddAsync(user);
        return user.Id;
    }
}
