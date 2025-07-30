namespace Csnp.SharedKernel.Domain;

public abstract class ValueObject
{
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (hash, component) =>
            {
                return hash * 23 + (component?.GetHashCode() ?? 0);
            });
    }

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

    public static bool operator !=(ValueObject? a, ValueObject? b)
    {
        return !(a == b);
    }
}
