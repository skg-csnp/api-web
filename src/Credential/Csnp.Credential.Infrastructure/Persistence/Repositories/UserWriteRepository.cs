using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Domain.Entities;
using Csnp.Credential.Infrastructure.Mappers;
using Csnp.Credential.Infrastructure.Persistence.Shared;
using IdGen;
using Microsoft.AspNetCore.Identity;

namespace Csnp.Credential.Infrastructure.Persistence.Repositories;

public class UserWriteRepository : IUserWriteRepository
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly IdGenerator _idGen;

    public UserWriteRepository(UserManager<UserEntity> userManager, IdGenerator idGen)
    {
        _userManager = userManager;
        _idGen = idGen;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        var entity = user.ToEntity();
        entity.Id = _idGen.CreateId();

        var result = await _userManager.CreateAsync(entity, user.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Create user failed: {errors}");
        }

        user.SetId(entity.Id);
    }
}
