using Xunit;
using GeometryLib;

namespace GeometryTests;

public class TrapezoidTests
{
    [Fact]
    public void Trapezoid_Area_ComputesCorrectly()
    {
        // Given bases 8 and 4, height 3
        var trap = new Trapezoid(8, 4, 3);
        // Area = ((base1 + base2) / 2) * height = ((8 + 4) / 2) * 3 = 18
        Assert.Equal(18, trap.Area());
    }

    [Fact]
    public void Trapezoid_Perimeter_ComputesCorrectly_WhenSidesProvided()
    {
        // Given bases 8 and 4, height 3, sides 5 and 5
        var trap = new Trapezoid(8, 4, 3, 5, 5);
        // Perimeter = base1 + base2 + side1 + side2 = 8 + 4 + 5 + 5 = 22
        Assert.Equal(22, trap.Perimeter());
    }

    [Fact]
    public void Trapezoid_InvalidInput_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Trapezoid(-8, 4, 3));
    }
}
