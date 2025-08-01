using Csnp.Credential.Infrastructure.Events;
using Csnp.Credential.Infrastructure.Persistence.Constants;
using Csnp.SharedKernel.Application.Abstractions.Events;
using Csnp.SharedKernel.Domain.Events;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Csnp.Credential.Infrastructure.Persistence;

/// <summary>
/// Represents the EF Core DbContext for the Credential module, including Identity and domain event dispatching.
/// </summary>
public class CredentialDbContext : IdentityDbContext<UserEntity, RoleEntity, long>
{
    #region -- Overrides --

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(SchemaNames.Default);
        builder.ApplyConfigurationsFromAssembly(typeof(UserEntity).Assembly);
    }

    /// <inheritdoc />
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken);

        List<IEntityWithEvents> domainEntities = ChangeTracker
            .Entries<IEntityWithEvents>()
            .Where(entry => entry.Entity.DomainEvents.Any())
            .Select(entry => entry.Entity)
            .ToList();

        List<IDomainEvent> domainEvents = domainEntities
            .SelectMany(entity => entity.DomainEvents)
            .ToList();

        await _dispatcher.DispatchAsync(domainEvents, cancellationToken);

        foreach (IEntityWithEvents entity in domainEntities)
        {
            entity.ClearDomainEvents();
        }

        return result;
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="CredentialDbContext"/> class.
    /// </summary>
    /// <param name="options">The DB context options.</param>
    public CredentialDbContext(DbContextOptions<CredentialDbContext> options) : base(options) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CredentialDbContext"/> class with event dispatcher.
    /// </summary>
    /// <param name="options">The DB context options.</param>
    /// <param name="dispatcher">The domain event dispatcher.</param>
    public CredentialDbContext(DbContextOptions<CredentialDbContext> options, IDomainEventDispatcher dispatcher) : base(options)
    {
        _dispatcher = dispatcher;
    }

    #endregion

    #region -- Fields --

    private readonly IDomainEventDispatcher _dispatcher = new NoOpDomainEventDispatcher();

    #endregion
}
