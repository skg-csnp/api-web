namespace Csnp.SeedWork.Domain;

/// <summary>
/// Represents a base class for value objects in the domain-driven design pattern.
/// A value object is defined by its components rather than a unique identity.
/// </summary>
public abstract class ValueObject
{
    #region -- Overrides --

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (hash, component) =>
            {
                return hash * 23 + (component?.GetHashCode() ?? 0);
            });
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Gets the components used to determine equality for the value object.
    /// Derived classes must implement this to specify which properties form equality.
    /// </summary>
    /// <returns>A sequence of objects to compare.</returns>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <summary>
    /// Determines whether two value objects are equal.
    /// </summary>
    /// <param name="a">The first value object.</param>
    /// <param name="b">The second value object.</param>
    /// <returns><c>true</c> if the value objects are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ValueObject? a, ValueObject? b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    /// <summary>
    /// Determines whether two value objects are not equal.
    /// </summary>
    /// <param name="a">The first value object.</param>
    /// <param name="b">The second value object.</param>
    /// <returns><c>true</c> if the value objects are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ValueObject? a, ValueObject? b)
    {
        return !(a == b);
    }

    #endregion
}
