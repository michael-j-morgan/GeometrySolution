using Xunit;
using GeometryLib;

namespace GeometryTests;

public class GeometryEdgeTests
{
    [Fact]
    public void Circle_WithZeroRadius_HasZeroArea()
    {
        var circle = new Circle(0);
        Assert.Equal(0, circle.Area());
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

