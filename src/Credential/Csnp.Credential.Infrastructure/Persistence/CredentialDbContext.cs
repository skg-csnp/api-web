using Csnp.Credential.Infrastructure.Events;
using Csnp.SharedKernel.Application.Abstractions.Events;
using Csnp.SharedKernel.Domain.Events;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Csnp.Credential.Infrastructure.Persistence;

public class CredentialDbContext : IdentityDbContext<UserEntity, RoleEntity, long>
{
    #region -- Overrides --

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(UserEntity).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken);

        var domainEntities = ChangeTracker
            .Entries<IEntityWithEvents>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x => x.Entity)
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.DomainEvents)
            .ToList();

        await _dispatcher.DispatchAsync(domainEvents, cancellationToken);

        foreach (IEntityWithEvents? entity in domainEntities)
        {
            entity.ClearDomainEvents();
        }

        return result;
    }

    #endregion

    #region -- Methods --

    public CredentialDbContext(DbContextOptions<CredentialDbContext> options) : base(options) { }

    public CredentialDbContext(DbContextOptions<CredentialDbContext> options, IDomainEventDispatcher dispatcher) : base(options)
    {
        _dispatcher = dispatcher;
    }

    #endregion

    #region -- Fields --

    private readonly IDomainEventDispatcher _dispatcher = new NoOpDomainEventDispatcher();

    #endregion
}
