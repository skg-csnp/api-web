using System.Globalization;
using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Domain.Entities;
using Csnp.Credential.Infrastructure.Mappers;
using Csnp.SharedKernel.Application.Abstractions.Events;
using Csnp.SharedKernel.Application.Events;
using IdGen;
using Microsoft.AspNetCore.Identity;

namespace Csnp.Credential.Infrastructure.Persistence.Repositories;

public class UserWriteRepository : IUserWriteRepository
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly IdGenerator _idGen;
    private readonly IDomainEventDispatcher _dispatcher;

    public UserWriteRepository(UserManager<UserEntity> userManager, IdGenerator idGen, IDomainEventDispatcher dispatcher)
    {
        _userManager = userManager;
        _idGen = idGen;
        _dispatcher = dispatcher;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        UserEntity entity = user.ToEntity();
        entity.Id = _idGen.CreateId();

        IdentityResult result = await _userManager.CreateAsync(entity, user.Password);
        if (!result.Succeeded)
        {
            string errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Create user failed: {errors}");
        }

        user.SetId(entity.Id);

        await DomainEventHelper.DispatchAndClearAsync(user, _dispatcher, cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        UserEntity? entity = await _userManager.FindByIdAsync(user.Id.ToString(CultureInfo.InvariantCulture));
        if (entity is null)
        {
            throw new KeyNotFoundException("User not found");
        }

        entity.DisplayName = user.DisplayName;
        entity.Email = user.Email.Value;

        IdentityResult result = await _userManager.UpdateAsync(entity);
        if (!result.Succeeded)
        {
            string errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Update user failed: {errors}");
        }
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken)
    {
        UserEntity? entity = await _userManager.FindByIdAsync(user.Id.ToString(CultureInfo.InvariantCulture));
        if (entity is null)
        {
            return;
        }

        await _userManager.DeleteAsync(entity);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _userManager.FindByEmailAsync(email) is not null;
    }
}
