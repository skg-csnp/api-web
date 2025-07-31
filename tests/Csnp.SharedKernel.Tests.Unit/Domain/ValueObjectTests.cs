using Csnp.SeedWork.Domain;

namespace Csnp.SeedWork.Tests.Unit.Domain;

public class ValueObjectTests
{
    private class TestValueObject : ValueObject
    {
        public string PropertyA { get; }
        public int PropertyB { get; }

        public TestValueObject(string propertyA, int propertyB)
        {
            PropertyA = propertyA;
            PropertyB = propertyB;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return PropertyA;
            yield return PropertyB;
        }
    }

    [Fact]
    public void Equals_ShouldReturnTrue_ForSameValues()
    {
        var obj1 = new TestValueObject("ABC", 123);
        var obj2 = new TestValueObject("ABC", 123);

        Assert.Equal(obj1, obj2);
        Assert.True(obj1 == obj2);
        Assert.False(obj1 != obj2);
    }

    [Fact]
    public void Equals_ShouldReturnFalse_ForDifferentValues()
    {
        var obj1 = new TestValueObject("ABC", 123);
        var obj2 = new TestValueObject("XYZ", 123);

        Assert.NotEqual(obj1, obj2);
        Assert.False(obj1 == obj2);
        Assert.True(obj1 != obj2);
    }

    [Fact]
    public void GetHashCode_ShouldBeEqual_ForEqualObjects()
    {
        var obj1 = new TestValueObject("ABC", 123);
        var obj2 = new TestValueObject("ABC", 123);

        Assert.Equal(obj1.GetHashCode(), obj2.GetHashCode());
    }

    [Fact]
    public void Equals_ShouldReturnFalse_IfOtherIsNull()
    {
        var obj1 = new TestValueObject("ABC", 123);

        Assert.False(obj1.Equals(null));
        Assert.False(obj1 == null);
        Assert.True(obj1 != null);
    }

    [Fact]
    public void Equals_ShouldReturnTrue_ForReferenceEqual()
    {
        var obj1 = new TestValueObject("ABC", 123);
        TestValueObject obj2 = obj1;

        Assert.True(obj1 == obj2);
        Assert.True(obj1.Equals(obj2));
        Assert.Equal(obj1.GetHashCode(), obj2.GetHashCode());
    }
}
