using Csnp.SharedKernel.Application.Abstractions.Events;
using Csnp.SharedKernel.Domain.Events;

namespace Csnp.Credential.Infrastructure.Events;

/// <summary>
/// A no-op implementation of <see cref="IDomainEventDispatcher"/> that does nothing.
/// </summary>
public class NoOpDomainEventDispatcher : IDomainEventDispatcher
{
    #region -- Implements --

    /// <inheritdoc />
    public Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    #endregion
}
