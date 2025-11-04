using Xunit;
using GeometryLib;

namespace GeometryTests;

public class TrapezoidTests
{
    [Fact]
    public void Trapezoid_Perimeter_ComputesCorrectly()
    {
        // Arrange
        var t = new Trapezoid(8, 4, 3, 5, 5);

        // Act
        var perimeter = t.Perimeter();

        // Assert
        Assert.Equal(8 + 4 + 5 + 5, perimeter);
    }

    [Fact]
    public void Trapezoid_InvalidSides_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Trapezoid(-8, 4, 3, 5, 5));
        Assert.Throws<ArgumentException>(() => new Trapezoid(8, 4, 3, -5, 5));
    }
}

