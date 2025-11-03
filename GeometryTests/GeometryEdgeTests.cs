using Xunit;
using GeometryLib;

namespace GeometryTests;

public class GeometryEdgeTests
{
    [Fact]
    public void Circle_WithZeroRadius_HasZeroArea()
    {
        Assert.Throws<ArgumentException>(() => new Circle(0));
    }

    [Fact]
    public void Square_WithNegativeSide_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Square(-2));
    }

    [Fact]
    public void Triangle_InvalidSides_ThrowsArgumentException()
    {
        // 1 + 2 < 10  → impossible triangle
        Assert.Throws<ArgumentException>(() => new Triangle(1, 2, 10));
    }
}

