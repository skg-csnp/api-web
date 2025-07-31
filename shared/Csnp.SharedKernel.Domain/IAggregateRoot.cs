namespace Csnp.SharedKernel.Domain;

/// <summary>
/// Represents a marker interface to indicate that an entity is an aggregate root in the domain model.
/// </summary>
/// <remarks>
/// In Domain-Driven Design (DDD), an aggregate root is the entry point to an aggregate, 
/// enforcing business invariants and controlling access to related entities.
/// </remarks>
public interface IAggregateRoot
{
}
